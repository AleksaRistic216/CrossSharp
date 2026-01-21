#!/bin/bash

# CSBuild.sh - Build a CrossSharp application for Windows and Linux
# Usage: ./CSBuild.sh <path-to-csproj>

set -e

if [ -z "$1" ]; then
    echo "Usage: $0 <path-to-csproj>"
    echo "Example: $0 ../demos/Demo.Buttons/Demo.Buttons.csproj"
    exit 1
fi

CSPROJ="$1"
CONFIG="${CONFIG:-Release}"

if [ ! -f "$CSPROJ" ]; then
    echo "Error: Project file not found: $CSPROJ"
    exit 1
fi

PROJECT_DIR=$(dirname "$CSPROJ")
PROJECT_NAME=$(basename "$CSPROJ" .csproj)

echo "Building $PROJECT_NAME..."

# Build for both platforms
for os in Windows_NT Linux; do
    echo "Building for $os..."
    dotnet build "$CSPROJ" -c "$CONFIG" -p:OS=$os
done

OUTPUT_DIR="$PROJECT_DIR/bin/$CONFIG/net10.0"

# Create run.sh for Linux
cat > "$OUTPUT_DIR/run.sh" << 'RUNSH'
#!/bin/bash
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REQUIRED_VERSION="10.0"

install_dotnet() {
    echo "Installing .NET $REQUIRED_VERSION..."
    curl -sSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel $REQUIRED_VERSION --runtime dotnet
    export DOTNET_ROOT="$HOME/.dotnet"
    export PATH="$DOTNET_ROOT:$PATH"
    echo ""
    echo "Installation complete. You may need to add the following to your shell profile:"
    echo "  export DOTNET_ROOT=\"\$HOME/.dotnet\""
    echo "  export PATH=\"\$DOTNET_ROOT:\$PATH\""
    echo ""
}

check_dotnet() {
    if command -v dotnet &> /dev/null; then
        if dotnet --list-runtimes 2>/dev/null | grep -q "Microsoft.NETCore.App $REQUIRED_VERSION"; then
            return 0
        fi
    fi
    if [ -x "$HOME/.dotnet/dotnet" ]; then
        export DOTNET_ROOT="$HOME/.dotnet"
        export PATH="$DOTNET_ROOT:$PATH"
        if dotnet --list-runtimes 2>/dev/null | grep -q "Microsoft.NETCore.App $REQUIRED_VERSION"; then
            return 0
        fi
    fi
    return 1
}

if check_dotnet; then
    echo "dotnet version $REQUIRED_VERSION exists"
else
    echo "dotnet version $REQUIRED_VERSION doesn't exist"
    read -p "Would you like to download and install .NET $REQUIRED_VERSION? [Y/n] " response
    response=${response:-Y}
    if [[ "$response" =~ ^[Yy]$ ]]; then
        install_dotnet
        if ! check_dotnet; then
            echo "Installation failed. Please install .NET $REQUIRED_VERSION manually."
            exit 1
        fi
        echo "dotnet version $REQUIRED_VERSION exists"
    else
        echo "Please install .NET $REQUIRED_VERSION manually from https://dotnet.microsoft.com/download"
        exit 1
    fi
fi

RUNSH
# Append the dotnet run command with the actual project name
echo "dotnet \"\$SCRIPT_DIR/$PROJECT_NAME.dll\" \"\$@\"" >> "$OUTPUT_DIR/run.sh"
chmod +x "$OUTPUT_DIR/run.sh"

# Create run.bat for Windows
cat > "$OUTPUT_DIR/run.bat" << 'RUNBAT'
@echo off
setlocal enabledelayedexpansion
set REQUIRED_VERSION=10.0

:check_dotnet
set DOTNET_FOUND=0
where dotnet >nul 2>nul
if %ERRORLEVEL% equ 0 (
    dotnet --list-runtimes 2>nul | findstr /C:"Microsoft.NETCore.App %REQUIRED_VERSION%" >nul
    if !ERRORLEVEL! equ 0 set DOTNET_FOUND=1
)
if %DOTNET_FOUND% equ 0 (
    if exist "%USERPROFILE%\.dotnet\dotnet.exe" (
        set "PATH=%USERPROFILE%\.dotnet;%PATH%"
        dotnet --list-runtimes 2>nul | findstr /C:"Microsoft.NETCore.App %REQUIRED_VERSION%" >nul
        if !ERRORLEVEL! equ 0 set DOTNET_FOUND=1
    )
)

if %DOTNET_FOUND% equ 1 (
    echo dotnet version %REQUIRED_VERSION% exists
    goto run_app
)

echo dotnet version %REQUIRED_VERSION% doesn't exist
set /p INSTALL="Would you like to download and install .NET %REQUIRED_VERSION%? [Y/n] "
if /i "%INSTALL%"=="" set INSTALL=Y
if /i "%INSTALL%"=="Y" goto install_dotnet
if /i "%INSTALL%"=="y" goto install_dotnet
echo Please install .NET %REQUIRED_VERSION% manually from https://dotnet.microsoft.com/download
exit /b 1

:install_dotnet
echo Installing .NET %REQUIRED_VERSION%...
powershell -NoProfile -ExecutionPolicy Bypass -Command "& { Invoke-WebRequest -Uri 'https://dot.net/v1/dotnet-install.ps1' -OutFile '%TEMP%\dotnet-install.ps1'; & '%TEMP%\dotnet-install.ps1' -Channel %REQUIRED_VERSION% -Runtime dotnet }"
set "PATH=%USERPROFILE%\.dotnet;%PATH%"
echo.
echo Installation complete.
echo.

dotnet --list-runtimes 2>nul | findstr /C:"Microsoft.NETCore.App %REQUIRED_VERSION%" >nul
if %ERRORLEVEL% neq 0 (
    echo Installation failed. Please install .NET %REQUIRED_VERSION% manually.
    exit /b 1
)
echo dotnet version %REQUIRED_VERSION% exists

:run_app
RUNBAT
# Append the dotnet run command with the actual project name
echo "dotnet \"%~dp0$PROJECT_NAME.dll\" %*" >> "$OUTPUT_DIR/run.bat"

echo "Build complete. Output in: $OUTPUT_DIR"
echo "Run scripts created: run.sh, run.bat"
