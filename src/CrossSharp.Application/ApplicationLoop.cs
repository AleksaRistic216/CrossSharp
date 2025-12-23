using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

class ApplicationLoop : IApplicationLoop
{
    readonly CancellationTokenSource _cts = new();
    readonly IInputHandler _inputHandler = Services.GetSingleton<IInputHandler>();

    void IApplicationLoop.Run<T>()
    {
        Debug.Log(LogCategory.App, "Starting application loop");
        _inputHandler.StartListeningAsync(_cts.Token);
        var platform = PlatformHelpers.GetCurrentPlatform();
        Debug.Log(LogCategory.App, $"Running on platform: {platform}");
        switch (platform)
        {
            case CrossPlatformType.Windows:
                RunWindowsApp<T>();
                break;
            case CrossPlatformType.Linux:
                RunLinuxApp<T>();
                break;
            case CrossPlatformType.MacOs:
                RunMacOsApp<T>();
                break;
            case CrossPlatformType.Undefined:
                Debug.LogError("Platform could not be determined");
                throw new PlatformNotSupportedException("Platform could not be determined.");
            default:
                Debug.LogError($"Platform not supported: {platform}");
                throw new PlatformNotSupportedException("The current platform is not supported.");
        }
    }

    void RunMacOsApp<T>()
        where T : IForm
    {
        throw new NotImplementedException();
    }

    void RunLinuxApp<T>()
        where T : IForm
    {
        CrossSharpApplicationRunner.Run<T>();
        Dispose();
    }

    void RunWindowsApp<T>()
        where T : IForm
    {
        CrossSharpApplicationRunner.Run<T>();
        Dispose();
    }

    public void Dispose()
    {
        Debug.Log(LogCategory.App, "Disposing application loop");
        _cts.Cancel();
        _cts.Dispose();
        var ih = Services.GetSingleton<IInputHandler>() as InputHandler; // Unsafe but should be fine
        ih?.StopListening();
        Debug.Log(LogCategory.App, "Application loop disposed");
    }
}
