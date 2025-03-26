@echo off
setlocal enabledelayedexpansion

if "%~1"=="" (
    echo Version number is required.
    echo Usage: build.bat [version]
    exit /b 1
)

set "version=%~1"

echo.
echo Compiling VelopackCSharpWpf with dotnet...
dotnet publish .\APBConfigInstaller\APBConfigInstaller\APBConfigInstaller.csproj -c Release -r win-x64 -o .\APBConfigInstaller\APBConfigInstaller\publish

echo.
echo Building Velopack Release v%version%
vpk pack -u APBConfigInstaller -v %version% -p .\APBConfigInstaller\APBConfigInstaller\publish -e APBConfigInstaller.exe