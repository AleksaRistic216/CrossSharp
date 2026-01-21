# Debugging Android Applications with JetBrains Rider

This guide explains how to debug CrossSharp Android applications using JetBrains Rider.

## Prerequisites

1. JetBrains Rider installed
2. Android SDK installed (typically at `~/Android/Sdk`)
3. Android emulator running or physical device connected

## Setup Steps

### 1. Configure Android SDK in Rider

**Option A: Set in Rider Settings (Recommended)**

1. Open **File → Settings** (or **Rider → Preferences** on macOS)
2. Navigate to **Build, Execution, Deployment → Android**
3. Set **Android SDK Location** to: `~/Android/Sdk`
4. Click **Apply** and **OK**

**Option B: Set Environment Variables**

Add to your shell profile (`~/.bashrc` or `~/.zshrc`):
```bash
export ANDROID_HOME=~/Android/Sdk
export ANDROID_SDK_ROOT=~/Android/Sdk
export PATH=$PATH:$ANDROID_HOME/platform-tools:$ANDROID_HOME/emulator
```

Then restart Rider or run `source ~/.bashrc`.

**Option C: Already configured via Directory.Build.props**

The project includes a `Directory.Build.props` file that automatically sets `AndroidSdkDirectory` if `~/Android/Sdk` exists.

### 2. Open the Solution

1. Open Rider
2. **File → Open** and select `cross-sharp.sln`

### 3. Create Run Configuration

1. Go to **Run → Edit Configurations** (or click the configuration dropdown in toolbar → **Edit Configurations**)
2. Click **+** (Add New Configuration)
3. Select **.NET Project**
4. Configure:
   - **Name**: `Demos.AllInOne Android`
   - **Project**: Select `Demos.AllInOne.csproj`
   - **Target framework**: `net10.0-android`
   - **Program arguments**: (leave empty)
   - **Working directory**: Should auto-fill
5. Click **Apply** and **OK**

### 4. Start Emulator (if not running)

Before debugging, ensure your emulator is running:

```bash
# List available emulators
~/Android/Sdk/emulator/emulator -list-avds

# Start emulator
~/Android/Sdk/emulator/emulator -avd NetAndroidEmulator &

# Verify it's running
~/Android/Sdk/platform-tools/adb devices
```

You should see something like:
```
List of devices attached
emulator-5554    device
```

### 5. Start Debugging

1. Set breakpoints by clicking in the gutter (left margin) of any `.cs` file
2. Select your `Demos.AllInOne Android` configuration from the dropdown in the toolbar
3. Click the **Debug** button (green bug icon) or press **Shift+F9**
4. Rider will automatically:
   - Build the Android project
   - Detect the running emulator
   - Deploy the APK
   - Attach the debugger
   - Launch the application

**Note**: If Rider prompts you to select a device, choose your running emulator from the list.

### Alternative: Run from Terminal, Attach in Rider

If the above doesn't work, you can run the app separately and attach:

1. Run the app from terminal:
   ```bash
   export ANDROID_HOME=~/Android/Sdk
   dotnet build demos/Demos.AllInOne/Demos.AllInOne.csproj -f net10.0-android -c Debug
   $ANDROID_HOME/platform-tools/adb install -r demos/Demos.AllInOne/bin/Debug/net10.0-android/com.crosssharp.demos.allinone-Signed.apk
   # Launch the app (uses monkey to start the main activity automatically)
   $ANDROID_HOME/platform-tools/adb shell monkey -p com.crosssharp.demos.allinone 1
   ```

   Alternatively, if you need to start a specific activity:
   ```bash
   # Find the main activity name
   $ANDROID_HOME/platform-tools/adb shell cmd package resolve-activity --brief com.crosssharp.demos.allinone
   # Then launch it
   $ANDROID_HOME/platform-tools/adb shell am start -n com.crosssharp.demos.allinone/<activity-name>
   ```

2. In Rider: **Run → Attach to Process**
3. Select **Android Debugger** from the dropdown
4. Choose your app process (`com.crosssharp.demos.allinone`)

## Debugging Techniques

### Breakpoints

- **Line breakpoint**: Click in the gutter
- **Conditional breakpoint**: Right-click breakpoint → Edit → Add condition
- **Exception breakpoint**: Run → View Breakpoints → Add → .NET Exception Breakpoint

### Inspecting Variables

When stopped at a breakpoint:
- **Locals window**: View local variables
- **Watch window**: Add expressions to monitor
- **Immediate window**: Execute code and evaluate expressions

### Logcat Integration

View Android logs directly in Rider:

1. **View → Tool Windows → Logcat**
2. Select your device/emulator
3. Filter by your app: `package:com.crosssharp.demos.allinone`

Or filter by log level:
- `level:error` - Errors only
- `level:warn` - Warnings and errors
- `level:info` - Info, warnings, and errors

### Using Debug.Log

The CrossSharp framework includes built-in logging. Add log statements:

```csharp
using CrossSharp.Utils.Helpers;

// In your code:
Debug.Log(LogCategory.App, "My debug message");
Debug.Log(LogCategory.Theme, $"Variable value: {myVar}");
Debug.LogError("Something went wrong");
```

View these logs in Logcat by filtering: `mono-stdout` or `CrossSharp`

## Troubleshooting

### Debugger Won't Attach

1. Ensure the app is built in `Debug` configuration (not `Release`)
2. Check that the emulator is running: `adb devices`
3. Restart ADB server: `adb kill-server && adb start-server`

### Breakpoints Not Hit

1. Verify you're running the `Debug` configuration
2. Clean and rebuild: **Build → Clean Solution**, then **Build → Rebuild Solution**
3. Ensure the source file is part of the project being debugged

### App Crashes on Start

Check Logcat for the crash reason:
```bash
adb logcat -b crash -d
```

Or in Rider's Logcat window, filter by `level:error`

### Slow Debugging

1. Use a physical device instead of emulator (faster)
2. Enable **Fast Deployment**: Project Properties → Android Options → Use Fast Deployment
3. Disable **Link Assemblies** for debug builds

## Command Line Alternative

If Rider debugging isn't working, use command line:

```bash
# Terminal 1: Start the app with debugger waiting
export ANDROID_HOME=~/Android/Sdk
dotnet run --project demos/Demos.AllInOne/Demos.AllInOne.csproj \
  -f net10.0-android -c Debug \
  -p:AndroidSdkDirectory=$ANDROID_HOME \
  -p:AndroidAttachDebugger=true

# Terminal 2: View logs
adb logcat | grep -E "CrossSharp|mono|Demos"
```

## Useful ADB Commands

```bash
# List connected devices
adb devices

# View all logs from your app
adb logcat --pid=$(adb shell pidof -s com.crosssharp.demos.allinone)

# Clear logcat buffer
adb logcat -c

# Take screenshot
adb exec-out screencap -p > screenshot.png

# Record screen
adb shell screenrecord /sdcard/demo.mp4

# Pull recorded video
adb pull /sdcard/demo.mp4

# Uninstall app
adb uninstall com.crosssharp.demos.allinone

# Force stop app
adb shell am force-stop com.crosssharp.demos.allinone
```
