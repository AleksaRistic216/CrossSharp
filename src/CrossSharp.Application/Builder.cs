using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Application;

public class Builder {
    static readonly CrossPlatformType _currentPlatform = PlatformHelpers.GetCurrentPlatform();
    static readonly Loop _appLoop = new ();
    
    public Builder(IApplicationConfiguration applicationConfiguration) {
        AddSingleton(applicationConfiguration);
    }
    
    // ==========================================================================
    // Run
    // ==========================================================================
    public void Run<T>() where T : Form, new() {
        switch (_currentPlatform) {
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
        Console.WriteLine("Running application...");
    }
    static void RunMacOsApp<T>() where T : Form {
        _appLoop.Run<T>();
    }
    static void RunLinuxApp<T>() where T : Form, new() {
        GtkApplicationRunner.Run<T>();
    }
    static void RunWindowsApp<T>() where T : Form {
        Thread thread = new (() => {
            _appLoop.Run<T>();
        });
#pragma warning disable CA1416
        thread.SetApartmentState(ApartmentState.STA);
#pragma warning restore CA1416
        thread.Start();
        thread.Join();
    }
    // ==========================================================================
    // ==========================================================================
    // ==========================================================================
    public void AddSingleton<T>(T instance) where T : class {
        ServicesPool.AddSingleton(instance);
    }
    public void AddSingleton<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface {
        ServicesPool.AddSingleton<TInterface, TImplementation>();
    }
}