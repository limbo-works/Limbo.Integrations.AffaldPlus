@echo off
dotnet build src/Limbo.Integrations.AffaldPlus --configuration Debug /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=c:/nuget