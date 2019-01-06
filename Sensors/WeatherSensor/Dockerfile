FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Sensors/WeatherSensor/WeatherSensor.csproj Sensors/WeatherSensor/
COPY Common/Common.csproj Common/
COPY Domain/Domain.csproj Domain/
RUN dotnet restore Sensors/WeatherSensor/WeatherSensor.csproj
COPY . .
WORKDIR /src/Sensors/WeatherSensor
RUN dotnet build WeatherSensor.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WeatherSensor.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WeatherSensor.dll"]
