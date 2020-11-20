FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG TARGETPLATFORM
ARG BUILDPLATFORM
WORKDIR /src
COPY Sensors/WeatherSensor/WeatherSensor.csproj Sensors/WeatherSensor/
COPY Common/Common.csproj Common/
COPY Domain/Domain.csproj Domain/
RUN dotnet restore Sensors/WeatherSensor/WeatherSensor.csproj
COPY . .
WORKDIR /src/Sensors/WeatherSensor
RUN dotnet build WeatherSensor.csproj -c Release -o /app

FROM build AS publish
RUN [ "bash", "-c", "if [ $TARGETPLATFORM == \"linux/arm/v7\" ]; then export ARCH_ARG=\"-r linux-arm --self-contained\"; else export ARCH_ARG=\"\"; fi; echo \"BUILDING FOR $ARCH_ARG\"; dotnet publish WeatherSensor.csproj $ARCH_ARG -c Release -o /app" ]

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WeatherSensor.dll"]
