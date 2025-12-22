# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CrossSharp is a pre-alpha cross-platform C# UI library that provides a unified API for building desktop applications on Linux, Windows, and macOS (currently Linux and Windows). It uses SDL3 for rendering and follows a three-tier architecture pattern for platform abstraction.

## Build Commands

```bash
# Build a project (specify OS: Windows_NT or Linux)
dotnet build src/CrossSharp.Application/CrossSharp.Application.csproj -c Debug -p:OS=Linux

# Build all demos for Windows and Linux
./automation/buildDemos.sh

# Create NuGet packages
./automation/pack.sh

# Run tests
dotnet test tests/Tests.Demo/Tests.Demo.csproj
dotnet test tests/Tests.Demo.ScreenShots/Tests.Demo.ScreenShots.csproj

# Format code with csharpier
dotnet tool restore
dotnet csharpier .
```

## Architecture

### Three-Tier Control Pattern

Every UI control follows a three-tier structure:
1. **Common Interface** (`CrossSharp.Ui.{Control}`) - Public API and abstractions
2. **Common Implementation** (`CrossSharp.Ui.{Control}.Common`) - Shared cross-platform logic
3. **Platform-Specific** (`CrossSharp.Ui.{Control}.Linux`, `CrossSharp.Ui.{Control}.Windows`) - Platform implementations

Example: Button control is split across `CrossSharp.Ui.Button`, `CrossSharp.Ui.Button.Common`, `CrossSharp.Ui.Button.Linux`, and `CrossSharp.Ui.Button.Windows` projects.

### Core Components

- **CrossSharp.Utils** - Core utilities, SDL3 rendering, DI container, image caching, input handling
- **CrossSharp.Application** - Application framework with `ApplicationBuilder`, `ApplicationLoop`, platform runners
- **CrossSharp.Themes** - Theme management system
- **CrossSharp.DynamicControlsController** - Dynamic control management

### Application Entry Pattern

```csharp
var configuration = new BaseConfiguration()
{
    ApplicationName = "AppName",
    CompanyName = "CompanyName"
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
```

### Form/Control Pattern

```csharp
public class MainForm : Form
{
    public MainForm()
    {
        var button = new Button() { Text = "Click" };
        Controls.Add(button);
    }
}
```

## Key Directories

- `src/` - Core library source (all CrossSharp.* packages)
- `src/lib/` - Native libraries (SDL3, SDL3_ttf for Linux/Windows)
- `demos/` - Demo applications showing control usage
- `tests/` - xUnit test projects
- `automation/` - Build and deployment scripts
- `templates/` - Project templates

## Dependencies

- SDL3 and SDL3_ttf for rendering (native libraries in `src/lib/`)
- SharpHook for input handling
- SkiaSharp for vector graphics
- SixLabors.ImageSharp for image processing

## Platform Targeting

Projects use conditional compilation based on `$(OS)` property (Windows_NT or Linux). Platform-specific code is isolated in dedicated projects with matching OS prefixes.
