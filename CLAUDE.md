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

## Adding New Icon Sets

To create a complete new icon set (e.g., `CrossSharp2026_Winter`), follow these steps:

### Step 1: Create the Icon Set Directory

Create a new directory under `src/CrossSharp.Icons/IconSets/` with your icon set name:
```bash
mkdir -p src/CrossSharp.Icons/IconSets/{IconSetName}
```

### Step 2: Define Your Design Language

Before creating icons, establish a consistent design language for the set. Consider:
- **Visual theme**: What makes this set unique (e.g., winter theme with snow accents)
- **Stroke style**: stroke-width, linecap, linejoin settings
- **Decorative elements**: Consistent accents across all icons (e.g., wavy snow lines)

Example design pattern (Winter theme with snow accumulation):
```xml
<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
  <!-- Base icon shape -->
  <path d="..."/>
  <!-- Snow accent using quadratic bezier curves -->
  <path d="M4 4 Q7 2 10 4 Q13 6 16 4 Q19 2 20 4"/>
</svg>
```

### Step 3: Create SVG Files for All Icons

Create one SVG file for each icon in the `Icon` enum (`src/CrossSharp.Utils/Interfaces/Icon.cs`):

| Icon | File Name | Design Notes |
|------|-----------|--------------|
| Collapse | Collapse.svg | Arrow with snow on peak |
| Maximize | Maximize.svg | Rectangle with snow on top edge |
| Restore | Restore.svg | Overlapping rectangles with snow |
| Minimize | Minimize.svg | Line with snow wave |
| Close | Close.svg | X with snow on top corners |
| Home | Home.svg | House with snow on roof |
| Palette | Palette.svg | Palette with snow on curve |
| Dropdown | Dropdown.svg | Arrow with snow on arms |
| DataGrid | DataGrid.svg | Grid with snow on top |
| HamburgerMenu | HamburgerMenu.svg | Lines with snow on top |
| Settings | Settings.svg | Gear with snow on top |
| Grabber | Grabber.svg | Dots with snow caps |
| Show | Show.svg | Eye with snow above |
| Hide | Hide.svg | Crossed eye with snow |
| Bookmark | Bookmark.svg | Bookmark with snow on top |

SVG requirements:
- `viewBox="0 0 24 24"` - Standard 24x24 grid
- `fill="none"` - No fill (stroke-based icons)
- `stroke="currentColor"` - Allows theming via CSS/code
- `stroke-width="2"` - Consistent stroke weight
- File name must exactly match enum value

### Step 4: Create the Icon Provider Class

Create a provider class in `src/CrossSharp.Icons/Providers/`:

**{IconSetName}IconProvider.cs**:
```csharp
namespace CrossSharp.Icons.Providers;

public class {IconSetName}IconProvider() : IconProviderBase("{IconSetName}");
```

Example for `CrossSharp2026_Winter`:
```csharp
namespace CrossSharp.Icons.Providers;

public class CrossSharp2026WinterIconProvider() : IconProviderBase("CrossSharp2026_Winter");
```

### Step 5: Verify Build

Build the Icons project to ensure SVGs are embedded correctly:
```bash
dotnet build src/CrossSharp.Icons/CrossSharp.Icons.csproj
```

### Step 6: Usage

Use the new icon provider in your application:
```csharp
var iconProvider = new CrossSharp2026WinterIconProvider();
var svg = iconProvider.GetSvg(Icon.Home);
```

### Design Tips for Themed Icon Sets

**Snow/Winter effects**:
- Use quadratic bezier curves (`Q`) for wavy snow lines: `M4 4 Q7 2 10 4 Q13 6 16 4`
- Place snow accents on horizontal surfaces and top edges
- Keep snow curves subtle (2-4 units variation)

**Avoid problematic designs**:
- Don't use small vertical lines hanging from edges (can look inappropriate)
- Keep decorative elements proportional to the icon
- Ensure the base icon remains recognizable

**Consistency checklist**:
- All icons use same stroke-width
- Decorative elements follow same pattern
- Snow/accent curves use similar amplitude
- Icons remain functional and recognizable

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
