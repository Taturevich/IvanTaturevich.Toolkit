#!/usr/bin/env bash
dotnet tool install --global dotnet-sonarscanner &&
dotnet sonarscanner begin /k:"taturevich_toolkit" /d:sonar.organization="taturevich-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="28d273d7cb3d188f13ceab30ea3ede984161f09f" &&
cd test/UtilitiesTest &&
dotnet test -c release &&
cd ./ &&
dotnet restore &&
dotnet build -c release &&
dotnet sonarscaner end /d:sonar.login="28d273d7cb3d188f13ceab30ea3ede984161f09f"
