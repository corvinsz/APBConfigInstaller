@echo off
setlocal enabledelayedexpansion

if "%~1"=="" (
    echo Version number is required.
    echo Usage: build.bat [version]
    exit /b 1
)

set "version=%~1"

echo.
echo Installing vpk CLI if needed
dotnet tool update -g vpk

echo.
echo Compiling app with dotnet...
dotnet publish .\APBConfigInstaller\APBConfigInstaller\APBConfigInstaller.csproj -c Release -r win-x64 -o .\APBConfigInstaller\APBConfigInstaller\publish

echo.
echo Building Velopack Release v%version%
vpk pack -u APBConfigInstaller -v %version% -p .\APBConfigInstaller\APBConfigInstaller\publish -e APBConfigInstaller.exe

echo.
echo Upload the generated files to a new GitHub release
vpk upload github --repoUrl "https://github.com/corvinsz/APBConfigInstaller" --token "$env:VPK_TOKEN" --publish --releaseName "MyApp $VERSION" --tag "v$VERSION"
