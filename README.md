# kestrel-whoami

ASP.Net core web api running kestrel webserver used to echo http request headers and webserver host name

## Running

**Using docker run command**

* `docker run --rm -it -p 5000:80 -e Kestrel__Limits__KeepAliveTimeout=00:00:20 christianacca/kestrel-whoami`
* browse to http://localhost:5000

**Using docker-compose**

* download [docker-compose.yml](docker-compose.yml)
* run: `docker-compose up`
* browse to http://localhost:5000

## Supported settings

See [Kestrel options](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.2#kestrel-options) for an explanation of each setting that can be supplied

Settings can be supplied by:
* docker config (swarm mode only) to supply an appsettings.json file
* docker volume to supply an appsettings.json file
* docker environment variable. See [docker-compose.yml](docker-compose.yml) for an example

## Build / Publish Docker image

* Build only: `./build.ps1`
* Build + publish: `./build.ps1 -Tag n.n.n -Publish -Credential christianacca`
    * replace 'n.n.n' with the semantic version that describes the change EG: 1.0.0

## Deploy

1. Update the ingress settings in [values.yaml](tools/helm/values.yaml) to fit your requirement 
   * this sample uses [AKS HTTP application routing](https://docs.microsoft.com/en-gb/azure/aks/http-application-routing)
2. Deploy helm chart:

   ```bash
   cd ./tools/helm
   helm repo add bitnami https://charts.bitnami.com/bitnami
   helm dependency build .
   helm upgrade --install --atomic --cleanup-on-fail kestrel-whoami .
   ```