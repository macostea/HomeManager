FROM mcr.microsoft.com/dotnet/sdk:5.0

ENV SONAR_TOKEN ""

RUN dotnet tool install --global dotnet-sonarscanner
# Hack for installing OpenJDK
RUN mkdir -p /usr/share/man/man1
RUN apt-get update && \
    apt-get install -y openjdk-11-jre-headless && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY . .

RUN chmod 744 /src/scripts/sonarcloud/docker-entrypoint.sh
ENTRYPOINT ["sh", "-c", "/src/scripts/sonarcloud/docker-entrypoint.sh"]
