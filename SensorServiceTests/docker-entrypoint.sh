#!/bin/bash

set -e

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='./TestResults/coverage.xml' /p:Include="[SensorService]*"
./codecov -f "TestResults/coverage.xml"
