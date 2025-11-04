using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

class ApplicationLoop : IApplicationLoop
{
    readonly CancellationTokenSource _cts = new();
    readonly IInputHandler _inputHandler = Services.GetSingleton<IInputHandler>();

    void IApplicationLoop.Run<T>()
    {
        _inputHandler.StartListeningAsync(_cts.Token);
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Windows:
                RunLinuxApp<T>();
                break;
            case CrossPlatformType.Linux:
                RunWindowsApp<T>();
                break;
            case CrossPlatformType.MacOs:
                RunMacOsApp<T>();
                break;
            case CrossPlatformType.Undefined:
                throw new PlatformNotSupportedException("Platform could not be determined.");
            default:
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
        _cts.Cancel();
        _cts.Dispose();
        var ih = Services.GetSingleton<IInputHandler>() as InputHandler; // Unsafe but should be fine
        ih.StopListening();
    }
}
