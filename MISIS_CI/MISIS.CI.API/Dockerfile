FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["MISIS.CI.API/MISIS.CI.API.csproj", "MISIS.CI.API/"]
RUN dotnet restore "MISIS.CI.API/MISIS.CI.API.csproj"
COPY . .
WORKDIR "/src/MISIS.CI.API"
RUN dotnet build "MISIS.CI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISIS.CI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISIS.CI.API.dll"]