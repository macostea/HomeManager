FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy dependency layer
COPY Common/*.csproj ./Common/
RUN ls -l
RUN cd Common && dotnet restore

# Copy csproj and restore as distinct layers
COPY Functions/SaveToDB/*.csproj ./Functions/SaveToDB/
RUN cd Functions/SaveToDB && dotnet restore

# Copy everything else and build
COPY . .
RUN ls -l Common

WORKDIR /app/Functions/SaveToDB
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:runtime

RUN addgroup --system app \
    && adduser --system --ingroup app app \
    && apt-get update -qy \
    && apt-get install -qy \
       curl ca-certificates --no-install-recommends \
    && echo "Pulling watchdog binary from Github." \
    && curl -sSL https://github.com/openfaas/faas/releases/download/0.8.10/fwatchdog > /usr/bin/fwatchdog \
    && chmod +x /usr/bin/fwatchdog \
    && apt-get -qy remove curl \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build-env /app/Functions/SaveToDB/out .

RUN chown app:app -R /app

USER app

ENV fprocess="dotnet SaveToDB.dll"
EXPOSE 8080

HEALTHCHECK --interval=1s CMD [ -e /tmp/.lock ] || exit 1

CMD ["fwatchdog"]

