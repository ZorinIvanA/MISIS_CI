FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-buster AS build
WORKDIR /src
COPY ["MISIS.CI.Gateway/MISIS.CI.Gateway.csproj", "MISIS.CI.Gateway/"]
RUN dotnet restore "MISIS.CI.Gateway/MISIS.CI.Gateway.csproj"
COPY . .
WORKDIR "/src/MISIS.CI.Gateway"
RUN dotnet build "MISIS.CI.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISIS.CI.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISIS.CI.Gateway.dll"]