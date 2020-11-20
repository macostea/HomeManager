FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY SensorListener/SensorListener.csproj SensorListener/
COPY Common/Common.csproj Common/
COPY Domain/Domain.csproj Domain/
RUN dotnet restore SensorListener/SensorListener.csproj
COPY . .
WORKDIR /src/SensorListener
RUN dotnet build SensorListener.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SensorListener.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SensorListener.dll"]
