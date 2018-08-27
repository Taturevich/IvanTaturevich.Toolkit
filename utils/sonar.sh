#!/usr/bin/env bash
cd ./ &&
dotnet tool install --global dotnet-sonarscanner --version 4.3.1 &&
dotnet sonarscanner begin /k:"taturevich_toolkit" /d:sonar.organization="taturevich-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="28d273d7cb3d188f13ceab30ea3ede984161f09f" &&
dotnet build &&
dotnet-sonarscaner end /d:sonar.login="28d273d7cb3d188f13ceab30ea3ede984161f09f"
