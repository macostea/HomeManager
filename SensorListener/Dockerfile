FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY SensorListener/SensorListener.csproj SensorListener/
COPY Common/Common.csproj Common/
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
