using System.Diagnostics;
using CrossSharp.Themes;
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
        RegisterCommonServices(applicationConfiguration);
        RegisterPlatformSpecificServices();
    }

    void RegisterCommonServices(IApplicationConfiguration applicationConfiguration)
    {
        AddSingleton(applicationConfiguration);
        AddSingleton<IInputHandler, InputHandler>();
        AddSingleton<IApplication, Utils.Application>();
        AddSingleton<IApplicationLoop, ApplicationLoop>();
    }

    void RegisterPlatformSpecificServices()
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Windows:
                RegisterWindowsServices();
                break;
            case CrossPlatformType.Linux:
                RegisterLinuxServices();
                break;
            case CrossPlatformType.MacOs:
                RegisterMacOsServices();
                break;
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Current platform is not supported");
        }
    }

    void RegisterLinuxServices()
    {
        AddSingleton<IFormFactory, Ui.Linux.FormFactory>();
        AddSingleton<IPanelFactory, Ui.Linux.PanelFactory>();
        AddSingleton<IFormTitleBarFactory, Ui.Linux.FormTitleBarFactory>();
        AddSingleton<IControlsContainerFactory, Ui.Linux.ControlsContainerFactory>();
        AddSingleton<IButtonFactory, Ui.Linux.ButtonFactory>();
        AddSingleton<ILabelFactory, Ui.Linux.LabelFactory>();
        AddSingleton<ICenterPanelFactory, Ui.Linux.CenterPanelFactory>();
    }

    void RegisterWindowsServices()
    {
        throw new NotImplementedException();
    }

    void RegisterMacOsServices()
    {
        throw new NotImplementedException();
    }

    public void Run<T>()
        where T : Form
    {
        // Catch all exceptions
        AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
        {
            Console.WriteLine(e.Exception);
            Debugger.Break();
        };
        // ===

        ConfirmTheme();
        ServicesPool.GetSingleton<IApplicationLoop>().Run<T>();
    }

    void ConfirmTheme()
    {
        if (ServicesPool.IsRegistered<ITheme>())
            return;

        ServicesPool.AddSingleton<ITheme, DefaultTheme>();
    }

    public void SetTheme(ITheme theme)
    {
        ServicesPool.AddSingleton(theme, true);
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
