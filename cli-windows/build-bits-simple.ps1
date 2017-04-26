# This approach still has issues, but would be the simplest approach for this script

Param([string] $rootPath)
$scriptPath = Split-Path $script:MyInvocation.MyCommand.Path

Write-Host "Current script directory is $scriptPath" -ForegroundColor Yellow

if ([string]::IsNullOrEmpty($rootPath)) {
    $rootPath = "$scriptPath\.."
}
Write-Host "Root path used is $rootPath" -ForegroundColor Yellow


$SolutionFilePath = $rootPath + "beershop.sln"

dotnet restore $SolutionFilePath

dotnet publish $SolutionFilePath -c Release -o .\obj\Docker\publish

