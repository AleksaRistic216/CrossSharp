using CrossSharp.Application.Constants;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Application;

public class Builder {
    static readonly CrossPlatformType _currentPlatform = PlatformHelpers.GetCurrentPlatform();
    public Builder(IApplicationConfiguration applicationConfiguration) {
        AddSingleton(applicationConfiguration);
    }
    public void Run<T>() where T : Form, new() {
        GeneralConstants.AppLoop.Run<T>(_currentPlatform);
    }
    public void AddSingleton<T>(T instance) where T : class {
        ServicesPool.AddSingleton(instance);
    }
    public void AddSingleton<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface {
        ServicesPool.AddSingleton<TInterface, TImplementation>();
    }
}