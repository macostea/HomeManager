FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy dependency layer
COPY Common/*.csproj ./Common
RUN cd Common && dotnet restore

# Copy csproj and restore as distinct layers
COPY Functions/SaveToDB/*.csproj ./Functions/SaveToDB
RUN cd Functions/SaveToDB && dotnet restore

# Copy everything else and build
COPY . .

WORKDIR /app/Functions/SaveToDB
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Functions/SaveToDB/out .
ENTRYPOINT ["dotnet", "SensorListener.dll"]
