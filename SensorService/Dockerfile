FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 44357

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SensorService/SensorService.csproj", "SensorService/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "SensorService/SensorService.csproj"
COPY . .
WORKDIR "/src/SensorService"
RUN dotnet build "SensorService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SensorService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SensorService.dll"]