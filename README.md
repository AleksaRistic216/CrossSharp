# Cross-Platform C# UI Deployment

## Issues tracking, reporting and idea suggestions
 - Overview: https://limitlesssoft.youtrack.cloud/dashboard?id=170-3
 - Report an issue / suggest idea: https://limitlesssoft.youtrack.cloud/issues

## Purpose
This project provides an easy way to develop C# UI applications across all major platforms — Linux, Windows, and macOS — using a single API.

## Features

- Unified deployment API for C# UI apps
- Supports Linux, Windows, and macOS (currently only Linux and Windows)
- Simplifies cross-platform development and distribution

## Try it out
 - Create Console Aplication project (Templates still to come)
 - `dotnet add package CrossSharp --version 9.0.3.2`
 - Create `MainForm.cs` and inherit `Form` (leave other empty)
 - Update `Program.cs` as bellow
 - Run application

```
using ConsoleApp1;
using CrossSharp.Application;

var configuration = new BaseConfiguration()
{
    ApplicationName = "ConsoleApp1",
    CompanyName = "Some Company",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
```

## Controls
Documentation is still to come, but for purpose of testing out you can either browse the source code `~/Demos` or try using any of bellow controls and figure out yourself:
 - Button
 - Label
 - Panel
 - Input
 - StackedLayout
 - TabbedLayout
 - FlowLayout
 - StaticLayout
 - Form
 - ModularForm
 - FilesPicker

## Build for multiplatform
 - Simply build with specifying OS=Window_NT or OS=Linux and you can run this app on both systems.
 - (plan is to simplify this and make this easier through single command and/or through UI)

## Current State
Project is currently maintained mostly by me in my free time. If anyone wants to join, feel free to contact me.
A lot of features are missing and are planned to come. If you want to see some feature before other, please open issue.

