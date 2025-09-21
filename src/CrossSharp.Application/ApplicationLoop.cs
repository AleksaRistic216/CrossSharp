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
        _ = _inputHandler.StartListeningAsync(_cts.Token);
        switch (PlatformHelpers.GetCurrentPlatform())
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
                throw new PlatformNotSupportedException("Platform could not be determined.");
            default:
                throw new PlatformNotSupportedException("The current platform is not supported.");
        }
    }

    static void RunMacOsApp<T>()
        where T : IForm
    {
        throw new NotImplementedException();
    }

    static void RunLinuxApp<T>()
        where T : IForm
    {
        GtkApplicationRunner.Run<T>();
    }

    static void RunWindowsApp<T>()
        where T : IForm
    {
        throw new NotImplementedException();
        // Bellow is a placeholder for actual Windows Forms application loop
        Thread thread = new(() =>
        {
            throw new NotImplementedException();
        });
#pragma warning disable CA1416
        thread.SetApartmentState(ApartmentState.STA);
#pragma warning restore CA1416
        thread.Start();
        thread.Join();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        var ih = Services.GetSingleton<IInputHandler>() as InputHandler; // Unsafe but should be fine
        ih.StopListening();
    }
}
