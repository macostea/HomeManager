FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 44357


# Dependencies
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG TARGETPLATFORM
ARG BUILDPLATFORM
WORKDIR /src
COPY ["SensorService/SensorService.csproj", "SensorService/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "SensorService/SensorService.csproj"
COPY . .
WORKDIR /src/SensorService

# Build and publish
FROM build AS publish
RUN [ "bash", "-c", "if [ $TARGETPLATFORM == \"linux/arm/v7\" ]; then export ARCH_ARG=\"-r linux-arm --self-contained\"; else export ARCH_ARG=\"\"; fi; echo \"BUILDING FOR $ARCH_ARG\"; dotnet publish \"SensorService.csproj\" $ARCH_ARG -c Release -o /app" ]

# Test
FROM build AS testrunner
WORKDIR /src/SensorServiceTests
RUN curl -s https://codecov.io/bash > codecov && \
	chmod +x codecov
RUN chmod 744 docker-entrypoint.sh
ENTRYPOINT ["sh", "-c", "/src/SensorServiceTests/docker-entrypoint.sh"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SensorService.dll"]