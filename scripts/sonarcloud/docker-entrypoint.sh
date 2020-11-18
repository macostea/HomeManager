#!/bin/bash

set -e

export PATH="$PATH:/root/.dotnet/tools"

dotnet sonarscanner begin /o:macostea /k:macostea_HomeManager /d:sonar.host.url=https://sonarcloud.io /d:sonar.cs.opencover.reportsPaths="SensorServiceTests/TestResults/coverage.xml" /d:sonar.cs.vstest.reportsPaths="SensorServiceTests/TestResults/*.trx"
dotnet build
dotnet test --logger trx /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='./TestResults/coverage.xml' /p:Include="[SensorService]*"
dotnet sonarscanner end
