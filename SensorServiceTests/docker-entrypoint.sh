#!/bin/bash


dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='./TestResults/coverage.xml'
./codecov -f "TestResults/coverage.xml"
