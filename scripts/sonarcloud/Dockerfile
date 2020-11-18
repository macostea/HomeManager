FROM mcr.microsoft.com/dotnet/sdk:5.0

ENV SONAR_TOKEN ""

# Install dotnet core runtime 2.1 for tests
# Install .NET Core
ENV DOTNET_VERSION 2.1.23

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Runtime/$DOTNET_VERSION/dotnet-runtime-$DOTNET_VERSION-linux-x64.tar.gz \
    && dotnet_sha512='9663a204abb74016113ae0db6c184598a6e5efc6126e35e275d81594432f75f186af781b3b352c8fe8527c690711820bfdd6271424b56e272f73ebe2d666bab5' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -zxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz
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
