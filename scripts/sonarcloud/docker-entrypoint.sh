#!/bin/bash

set -e

export PATH="$PATH:/root/.dotnet/tools"

dotnet sonarscanner begin /o:macostea /k:macostea_HomeManager /d:sonar.host.url=https://sonarcloud.io
dotnet build
dotnet sonarscanner end

