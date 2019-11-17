FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ./*.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build "MISIS.CI.Storage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISIS.CI.Storage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISIS.CI.Storage.dll"]
