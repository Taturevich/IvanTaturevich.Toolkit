cd src/IvanT.Utilities && dotnet pack IvanT.Utilities.csproj -c release -o . && dotnet nuget push *.nupkg -s http://taturevichnugetserver.azurewebsites.net/