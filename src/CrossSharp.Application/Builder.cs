using CrossSharp.Application.Constants;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

public class Builder
{
    public Builder(IApplicationConfiguration applicationConfiguration)
    {
        AddSingleton(applicationConfiguration);
        AddSingleton<IApplication, Utils.Application>();
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Windows:
                throw new NotImplementedException();
            case CrossPlatformType.Linux:
                AddSingleton<IFormFactory, FormLinuxFactory>();
                break;
            case CrossPlatformType.MacOs:
                throw new NotImplementedException();
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Current platform is not supported");
        }
    }

    public void Run<T>()
        where T : Form, new()
    {
        GeneralConstants.AppLoop.Run<T>();
    }

    public void AddSingleton<T>(T instance)
        where T : class
    {
        ServicesPool.AddSingleton(instance);
    }

    public void AddSingleton<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface
    {
        ServicesPool.AddSingleton<TInterface, TImplementation>();
    }

    public void EnableDevelopersMode()
    {
        IApplication app = ServicesPool.GetSingleton<IApplication>();
        app.DevelopersMode = true;
    }
}
