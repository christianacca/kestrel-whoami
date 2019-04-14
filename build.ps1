param(
    [string] $Tag = 'latest',
    [switch] $Publish,
    [pscredential] $Credential
)

begin {
    $ErrorActionPreference = 'Stop'

    function exec {
        param([scriptblock] $Command)

        & $Command
        if ($LASTEXITCODE -ne 0) {
            throw "Command failed with exit code $LASTEXITCODE"
        }
    }

    function Get-GitBranchName {
        exec { git rev-parse --abbrev-ref HEAD }
    }

    function Get-GitShortCommitSha {
        $sha = Get-GitCommitSha
        $sha.substring(0, [System.Math]::Min(14, $sha.Length))
    }

    function Get-GitCommitSha {
        exec { git rev-parse HEAD }
    }

    function Get-Version {
        <#
        .SYNOPSIS
        # Returns the semantic version numbers from the $Value supplied

        .EXAMPLE
        Get-Version 1.0.1

        Output
        ---
        1
        1.0
        1.0.1
        
        #>
        param(
            [string] $Value
        )
        $Value -split '\.' | ForEach-Object -Begin { $str='' } -Process {
            if ($str -eq '') { 
                $str = $_
            } else {
                $str += ".$_"
            }
            $str
        }        
    }
}
process {

    if ($Credential) {
        $dockerUsername = $Credential.UserName
        $dockerPassword = $Credential.GetNetworkCredential().Password
        exec { docker login -u $dockerUsername -p $dockerPassword }
    }

    $tags = @(
        Get-Version $Tag
        if ($Tag -ne 'latest' -and $(Get-GitBranchName) -eq 'master') { 'latest' }
        if ($Tag -ne 'latest') { Get-GitShortCommitSha }
    )

    $env:REPO = 'christianacca/kestrel-whoami'

    $env:TAG = $Tag
    $buildArgs = @(
        '--build-arg', "VERSION=$Tag"
        '--build-arg', "COMMIT=$(Get-GitCommitSha)"
        '--build-arg', "DATE=$(Get-Date -UFormat '%Y-%m-%dT%H:%M:%SZ')"
    )
    exec { docker-compose build @buildArgs --pull }

    $builtImage = '{0}:{1}' -f $env:REPO, $Tag
    $tags | Where-Object { $_ -ne $Tag } | ForEach-Object {
        exec { docker tag $builtImage ('{0}:{1}' -f $env:REPO, $_) }
    }

    if ($Publish) {
        $tags | ForEach-Object {
            $env:TAG = $_
            exec { docker-compose push }
        }
    }
}