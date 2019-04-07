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
WORKDIR /app
ENTRYPOINT ["dotnet", "kestrel-whoami.dll"]
COPY --from=publish /app .
