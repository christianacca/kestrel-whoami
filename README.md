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

## Build / Publish

* Build only: `./build.ps1`
* Build + publish: `./build.ps1 -Tag n.n.n -Publish -Credential christianacca`
    * replace 'n.n.n' with the sematic version that describes the change EG: 1.0.0