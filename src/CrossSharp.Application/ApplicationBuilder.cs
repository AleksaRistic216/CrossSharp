using System.Diagnostics;
using CrossSharp.Themes;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

public class ApplicationBuilder
{
    public ApplicationBuilder(IApplicationConfiguration applicationConfiguration)
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
        AddSingleton<IEfficientImagesCache, EfficientImageCache>();
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
        AddSingleton<IModularFormFactory, Ui.Linux.ModularFormFactory>();
        AddSingleton<IStaticLayoutFactory, Ui.Linux.StaticLayoutFactory>();
        AddSingleton<IStackedLayoutFactory, Ui.Linux.StackedLayoutFactory>();
        AddSingleton<ITabbedLayoutFactory, Ui.Linux.TabbedLayoutFactory>();
        AddSingleton<IFlowLayoutFactory, Ui.Linux.FlowLayoutFactory>();
        AddSingleton<IFilesPickerFactory, Ui.Linux.FilesPickerFactory>();
        AddSingleton<IPanelFactory, Ui.Linux.PanelFactory>();
        AddSingleton<ILabelFactory, Ui.Linux.LabelFactory>();
        AddSingleton<IFontFamilyMap, Utils.Linux.FontFamilyMap>();
        AddSingleton<IButtonFactory, Ui.Linux.ButtonFactory>();
        AddSingleton<IInputFactory, Ui.Linux.InputFactory>();
    }

    void RegisterWindowsServices()
    {
        AddSingleton<IFormFactory, Ui.Linux.FormFactory>();
        AddSingleton<IStaticLayoutFactory, Ui.Linux.StaticLayoutFactory>();
        AddSingleton<IFontFamilyMap, Utils.Linux.FontFamilyMap>();
    }

    void RegisterMacOsServices()
    {
        throw new NotImplementedException();
    }

    public void Run<T>()
        where T : IForm
    {
        // Catch all exceptions
        // Commented for not because it catches them good
        // AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
        // {
        //     Console.WriteLine(e.Exception);
        //     Debugger.Break();
        // };
        // ===

        ConfirmTheme();
        Services.GetSingleton<IApplicationLoop>().Run<T>();
    }

    static void ConfirmTheme()
    {
        if (Services.IsRegistered<ITheme>())
            return;

        Services.AddSingleton<ITheme, DefaultTheme>();
    }

    public void SetTheme(ITheme theme) => Services.AddSingleton(theme, true);

    public void AddSingleton<T>(T instance)
        where T : class => Services.AddSingleton(instance);

    public void AddSingleton<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface =>
        Services.AddSingleton<TInterface, TImplementation>();

    public void EnableDevelopersMode() =>
        Services.GetSingleton<IApplication>().DevelopersMode = true;
}
