#!/usr/bin/env bash
cd test/UtilitiesTest &&
dotnet test -c release &&
cd ./ &&
dotnet restore &&
dotnet build -c release
