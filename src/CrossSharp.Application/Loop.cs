using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Application;

class Loop : IDisposable
{
    readonly CancellationTokenSource _cts = new();

    internal void Run<T>()
        where T : Form, new()
    {
        var ih = new InputHandler();
        ServicesPool.AddSingleton(ih);
        _ = ih.StartListeningAsync(_cts.Token);
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
        where T : Form
    {
        throw new NotImplementedException();
    }

    static void RunLinuxApp<T>()
        where T : Form, new()
    {
        GtkApplicationRunner.Run<T>();
    }

    static void RunWindowsApp<T>()
        where T : Form
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
        var ih = ServicesPool.GetSingleton<InputHandler>();
        ih.StopListening();
    }
}
