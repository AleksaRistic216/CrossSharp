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

## Adding New Icons

To add a new icon to the project:

1. **Add to Icon enum** - Add the icon name to `src/CrossSharp.Utils/Interfaces/Icon.cs`:
   ```csharp
   public enum Icon
   {
       // existing icons...
       MyNewIcon,
   }
   ```

2. **Create SVG files** - Create an SVG file for each icon set in `src/CrossSharp.Icons/IconSets/{IconSetName}/`:
   - File must be named exactly as the enum value: `MyNewIcon.svg`
   - SVG files are automatically embedded as resources via the csproj

3. **Icon set style (CrossSharp2026)** - Follow this format:
   ```xml
   <?xml version="1.0" encoding="UTF-8"?>
   <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
     <!-- icon paths/shapes here -->
   </svg>
   ```

### Icon Loading

Icons are loaded via `IIconProvider.GetSvg(Icon icon)` which reads from embedded resources using the pattern:
`CrossSharp.Icons.IconSets.{IconSetName}.{IconName}.svg`

## Adding New Controls

To add a new control (e.g., `Chart`), follow these steps:

### Step 1: Create Interfaces in CrossSharp.Utils

Create two interface files in `src/CrossSharp.Utils/Interfaces/`:

**IChart.cs** - Control interface inheriting from required base interfaces:
```csharp
namespace CrossSharp.Utils.Interfaces;

public interface IChart : IControl, IBackgroundColorProvider, IDockable { }
```

**IChartFactory.cs** - Factory interface for DI:
```csharp
namespace CrossSharp.Utils.Interfaces;

public interface IChartFactory
{
    IChart Create();
}
```

Common interfaces to inherit from:
- `IControl` - Base control interface (required)
- `IBackgroundColorProvider` - Adds `BackgroundColor` and `BackgroundColorChanged`
- `IDockable` - Adds `Dock` and `DockIndex` for docking support
- `IRoundedCorners` - Adds `CornerRadius` property
- `IClickable` - Adds `Click` event
- `IAutoSize` - Adds auto-sizing properties

### Step 2: Create the Four Projects

Create directories and projects under `src/`:

#### 2.1 Public API Project (`CrossSharp.Ui.Chart`)

**Chart.cs** - Wrapper class delegating to implementation:
```csharp
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Chart() : CrossControl<IChart>(Services.GetSingleton<IChartFactory>().Create()), IChart
{
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }
    public int DockIndex
    {
        get => Implementation.DockIndex;
        set => Implementation.DockIndex = value;
    }
    public DockStyle Dock
    {
        get => Implementation.Dock;
        set => Implementation.Dock = value;
    }
}
```

#### 2.2 Common Implementation Project (`CrossSharp.Ui.Chart.Common`)

Split into partial classes for organization:

**Chart.cs** - Main logic, constructor, PerformTheme, Invalidate:
```csharp
using CrossSharp.Utils;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Chart : ControlBase, IChart
{
    internal Chart() { }

    public override void PerformTheme() { }

    public override void Invalidate()
    {
        this.PerformDocking();  // Required for IDockable controls
    }
}
```

**Chart.Properties.cs** - Property definitions with backing fields:
```csharp
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Ui.Common;

partial class Chart
{
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            OnBackgroundColorChangedInternal();  // Trigger change handler
        }
    }

    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
}
```

**Chart.Handlers.cs** - Event handlers and change notifications:
```csharp
namespace CrossSharp.Ui.Common;

partial class Chart
{
    public EventHandler? BackgroundColorChanged { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
}
```

**AssemblyInfo.cs** - Grant access to platform projects:
```csharp
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrossSharp.Ui.Chart.Linux")]
[assembly: InternalsVisibleTo("CrossSharp.Ui.Chart.Windows")]
```

#### 2.3 Platform Projects (`CrossSharp.Ui.Chart.Linux` and `CrossSharp.Ui.Chart.Windows`)

Each platform project contains:

**Chart.cs** - Empty class inheriting from Common:
```csharp
namespace CrossSharp.Ui.Linux;  // or CrossSharp.Ui.Windows

class Chart : Common.Chart { }
```

**ChartFactory.cs** - Factory implementation:
```csharp
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;  // or CrossSharp.Ui.Windows

class ChartFactory : IChartFactory
{
    public IChart Create()
    {
        var chart = new Chart();
        return chart;
    }
}
```

**AssemblyInfo.cs** - Grant access to Application project:
```csharp
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrossSharp.Application")]
```

### Step 3: Add to Solution

```bash
dotnet sln cross-sharp.sln add src/CrossSharp.Ui.Chart/CrossSharp.Ui.Chart.csproj --solution-folder Controls/Chart
dotnet sln cross-sharp.sln add src/CrossSharp.Ui.Chart.Common/CrossSharp.Ui.Chart.Common.csproj --solution-folder Controls/Chart
dotnet sln cross-sharp.sln add src/CrossSharp.Ui.Chart.Linux/CrossSharp.Ui.Chart.Linux.csproj --solution-folder Controls/Chart
dotnet sln cross-sharp.sln add src/CrossSharp.Ui.Chart.Windows/CrossSharp.Ui.Chart.Windows.csproj --solution-folder Controls/Chart
```

### Step 4: Register in CrossSharp.Application

**CrossSharp.Application.csproj** - Add project references:
```xml
<ProjectReference Include="..\CrossSharp.Ui.Chart.Linux\CrossSharp.Ui.Chart.Linux.csproj" />
<ProjectReference Include="..\CrossSharp.Ui.Chart.Windows\CrossSharp.Ui.Chart.Windows.csproj" />
<ProjectReference Include="..\CrossSharp.Ui.Chart\CrossSharp.Ui.Chart.csproj" />
```

**ApplicationBuilder.cs** - Register factories:
```csharp
// In RegisterLinuxServices():
AddSingleton<IChartFactory, ChartFactory>();

// In RegisterWindowsServices():
AddSingleton<IChartFactory, Ui.Windows.ChartFactory>();
```

### Property Change Pattern

For properties that need change notification:

1. **Properties.cs**: Use backing field, call `On{Property}ChangedInternal()` in setter
2. **Handlers.cs**: Define event, internal handler (calls Invalidate + Raise), and raise method

```csharp
// In Properties.cs
ColorRgba _backgroundColor = ColorRgba.Transparent;
public ColorRgba BackgroundColor
{
    get => _backgroundColor;
    set
    {
        if (_backgroundColor == value) return;
        _backgroundColor = value;
        OnBackgroundColorChangedInternal();
    }
}

// In Handlers.cs
public EventHandler? BackgroundColorChanged { get; set; }

void OnBackgroundColorChangedInternal()
{
    Invalidate();
    RaiseBackgroundColorChanged();
}

void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
```

### Important Notes

- Controls implementing `IDockable` MUST call `this.PerformDocking()` in `Invalidate()`
- The `DrawBackground()` method in `ControlBase` automatically uses `IBackgroundColorProvider.BackgroundColor`
- Platform projects use `internal` classes - `InternalsVisibleTo` grants access to required assemblies
- All csproj files should use `<RootNamespace>` matching the project's namespace convention
