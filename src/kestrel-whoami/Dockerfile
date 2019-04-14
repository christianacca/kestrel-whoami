FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY kestrel-whoami.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release --no-restore

FROM build AS publish
RUN dotnet publish -c Release -o /app --no-build

FROM base AS final

ARG COMMIT="local-build"
ARG DATE="1970-01-01T00:00:00Z"
ARG URL="https://github.com/jnovack/dockerhub-hooks"
ARG VERSION="dirty"

WORKDIR /app
HEALTHCHECK --interval=5s --timeout=5s \
    CMD curl --fail http://localhost/api/health || exit 1
ENTRYPOINT ["dotnet", "kestrel-whoami.dll"]

COPY --from=publish /app .

LABEL org.opencontainers.image.created=$DATE \
    org.opencontainers.image.vendor="christianacca" \
    org.opencontainers.image.title="christianacca/kestrel-whoami" \
    org.opencontainers.image.description="ASP.Net core web api used to echo http request headers and webserver host name" \
	org.opencontainers.image.licenses="MIT" \
    org.opencontainers.image.url="https://hub.docker.com/r/christianacca/kestrel-whoami" \
    org.opencontainers.image.version="$VERSION" \
    org.opencontainers.image.source=$URL \
    org.opencontainers.image.revision=$COMMIT