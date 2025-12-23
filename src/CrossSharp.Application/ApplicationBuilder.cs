using CrossSharp.Themes;
using CrossSharp.Ui.Linux;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Linux;

namespace CrossSharp.Application;

public class ApplicationBuilder
{
    public ApplicationBuilder(IApplicationConfiguration applicationConfiguration)
    {
        Debug.Log(LogCategory.App, $"Initializing ApplicationBuilder for '{applicationConfiguration.ApplicationName}'");
        Debug.Log(LogCategory.App, $"Log file: {Debug.GetLogFilePath()}");
        RegisterCommonServices(applicationConfiguration);
        RegisterPlatformSpecificServices();
        Debug.Log(LogCategory.App, "ApplicationBuilder initialized successfully");
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
        var platform = PlatformHelpers.GetCurrentPlatform();
        Debug.Log(LogCategory.App, $"Detected platform: {platform}");
        switch (platform)
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
                Debug.LogError($"Unsupported platform: {platform}");
                throw new NotSupportedException("Current platform is not supported");
        }
    }

    void RegisterLinuxServices()
    {
        AddSingleton<IFormFactory, FormFactory>();
        AddSingleton<IModularFormFactory, ModularFormFactory>();
        AddSingleton<IStaticLayoutFactory, StaticLayoutFactory>();
        AddSingleton<IStackedLayoutFactory, StackedLayoutFactory>();
        AddSingleton<ITabbedLayoutFactory, TabbedLayoutFactory>();
        AddSingleton<IFlowLayoutFactory, FlowLayoutFactory>();
        AddSingleton<IFilesPickerFactory, FilesPickerFactory>();
        AddSingleton<IPanelFactory, PanelFactory>();
        AddSingleton<ILabelFactory, LabelFactory>();
        AddSingleton<IFontFamilyMap, FontFamilyMap>();
        AddSingleton<IButtonFactory, ButtonFactory>();
        AddSingleton<IInputFactory, InputFactory>();
        AddSingleton<IAccordionFactory, AccordionFactory>();
        AddSingleton<IDropdownFactory, DropdownFactory>();
        AddSingleton<IThemePickerFactory, ThemePickerFactory>();
        AddSingleton<IDataGridFactory, DataGridFactory>();
        AddSingleton<IImagePreviewFactory, ImagePreviewFactory>();
    }

    void RegisterWindowsServices()
    {
        AddSingleton<IFormFactory, Ui.Windows.FormFactory>();
        AddSingleton<IModularFormFactory, Ui.Windows.ModularFormFactory>();
        AddSingleton<IStaticLayoutFactory, Ui.Windows.StaticLayoutFactory>();
        AddSingleton<IStackedLayoutFactory, Ui.Windows.StackedLayoutFactory>();
        AddSingleton<ITabbedLayoutFactory, Ui.Windows.TabbedLayoutFactory>();
        AddSingleton<IFlowLayoutFactory, Ui.Windows.FlowLayoutFactory>();
        AddSingleton<IFilesPickerFactory, Ui.Windows.FilesPickerFactory>();
        AddSingleton<IPanelFactory, Ui.Windows.PanelFactory>();
        AddSingleton<ILabelFactory, Ui.Windows.LabelFactory>();
        AddSingleton<IFontFamilyMap, Utils.Windows.FontFamilyMap>();
        AddSingleton<IButtonFactory, Ui.Windows.ButtonFactory>();
        AddSingleton<IInputFactory, Ui.Windows.InputFactory>();
        AddSingleton<IAccordionFactory, Ui.Windows.AccordionFactory>();
        AddSingleton<IDropdownFactory, Ui.Windows.DropdownFactory>();
        AddSingleton<IThemePickerFactory, Ui.Windows.ThemePickerFactory>();
        AddSingleton<IDataGridFactory, Ui.Windows.DataGridFactory>();
        AddSingleton<IImagePreviewFactory, Ui.Windows.ImagePreviewFactory>();
    }

    void RegisterMacOsServices()
    {
        throw new NotImplementedException();
    }

    public void Run<T>()
        where T : IForm
    {
        Debug.Log(LogCategory.App, $"Starting application with main form: {typeof(T).Name}");

        // Catch all exceptions
        // Commented for not because it catches them good
        // AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
        // {
        //     Console.WriteLine(e.Exception);
        //     DebugLog.Break();
        // };
        // ===

        ConfirmTheme();
        ConfirmIconProvider();
        Services.GetSingleton<IApplicationLoop>().Run<T>();
    }

    static void ConfirmTheme()
    {
        if (Services.IsRegistered<ITheme>())
        {
            Debug.Log(LogCategory.Theme, $"Using registered theme: {Services.GetSingleton<ITheme>().GetType().Name}");
            return;
        }

        Debug.Log(LogCategory.Theme, "No theme registered, using DefaultTheme");
        Services.AddSingleton<ITheme, DefaultTheme>();
    }

    public void SetTheme(ITheme theme)
    {
        Debug.Log(LogCategory.Theme, $"Setting theme: {theme.GetType().Name}");
        Services.AddSingleton(theme, true);
    }

    static void ConfirmIconProvider()
    {
        if (Services.IsRegistered<IIconProvider>())
            return;

        const string message =
            "No icon provider registered. "
            + "Register your own implementation of IIconProvider "
            + "or reference `CrossSharp.Icons` and register one of the built-in providers.";

        Debug.LogError(message);
        throw new InvalidOperationException(message);
    }

    public void SetIconProvider(IIconProvider iconProvider)
    {
        Debug.Log(LogCategory.App, $"Setting icon provider: {iconProvider.GetType().Name}");
        Services.AddSingleton(iconProvider, true);
    }

    public void AddSingleton<T>(T instance)
        where T : class => Services.AddSingleton(instance);

    public void AddSingleton<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface => Services.AddSingleton<TInterface, TImplementation>();

    public void EnableDevelopersMode() => Services.GetSingleton<IApplication>().DevelopersMode = true;
}
