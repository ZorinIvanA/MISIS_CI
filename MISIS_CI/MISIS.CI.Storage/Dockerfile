FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-buster AS build
WORKDIR /src
COPY ["MISIS.CI.Storage/MISIS.CI.Storage.csproj", "MISIS.CI.Storage/"]
RUN dotnet restore "MISIS.CI.Storage/MISIS.CI.Storage.csproj"
COPY . .
WORKDIR "/src/MISIS.CI.Storage"
RUN dotnet build "MISIS.CI.Storage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISIS.CI.Storage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISIS.CI.Storage.dll"]