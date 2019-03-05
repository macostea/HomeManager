FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 44357


# Dependencies
FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SensorService/SensorService.csproj", "SensorService/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "SensorService/SensorService.csproj"
COPY . .
WORKDIR "/src/SensorService"

# Build and publish
FROM build AS publish
RUN dotnet publish "SensorService.csproj" -c Release -o /app

# Test
FROM build AS testrunner
WORKDIR /src/SensorServiceTests
RUN curl -s https://codecov.io/bash > codecov && \
	chmod +x codecov
ENTRYPOINT ["sh", "-c", "/src/SensorServiceTests/docker-entrypoint.sh"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SensorService.dll"]