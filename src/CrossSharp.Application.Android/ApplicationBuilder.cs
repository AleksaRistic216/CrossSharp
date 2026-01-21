using CrossSharp.Application.Android;
using CrossSharp.Themes;
using CrossSharp.Ui.Android;
using CrossSharp.Utils;
using CrossSharp.Utils.Android;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

/// <summary>
/// Application builder for Android platform.
/// This class has the same namespace and API as the desktop ApplicationBuilder,
/// allowing the same Program.cs code to work on both platforms.
/// </summary>
public class ApplicationBuilder
{
    public ApplicationBuilder(IApplicationConfiguration applicationConfiguration)
    {
        Debug.Log(
            LogCategory.App,
            $"Initializing ApplicationBuilder (Android) for '{applicationConfiguration.ApplicationName}'"
        );
        RegisterCommonServices(applicationConfiguration);
        RegisterAndroidServices();
        Debug.Log(LogCategory.App, "ApplicationBuilder (Android) initialized successfully");
    }

    void RegisterCommonServices(IApplicationConfiguration applicationConfiguration)
    {
        Services.AddSingleton(applicationConfiguration);
        Services.AddSingleton<IEfficientImagesCache, EfficientImageCache>();
    }

    void RegisterAndroidServices()
    {
        Debug.Log(LogCategory.App, "Registering Android services");
        Services.AddSingleton<IFontFamilyMap, FontFamilyMap>();
        Services.AddSingleton<IInputHandler, AndroidInputHandler>();

        // Register Form factory for Android
        Services.AddSingleton<IFormFactory, CrossSharp.Application.Android.FormFactory>();

        // Register implemented Android control factories
        Services.AddSingleton<IStaticLayoutFactory, StaticLayoutFactory>();
        Services.AddSingleton<IStackedLayoutFactory, StackedLayoutFactory>();
        Services.AddSingleton<IPanelFactory, PanelFactory>();
        Services.AddSingleton<ILabelFactory, LabelFactory>();
        Services.AddSingleton<IButtonFactory, ButtonFactory>();
        Services.AddSingleton<IAccordionFactory, AccordionFactory>();

        Services.AddSingleton<IChartFactory, ChartFactory>();
        Services.AddSingleton<IDataGridFactory, DataGridFactory>();
        Services.AddSingleton<IDropdownFactory, DropdownFactory>();
        Services.AddSingleton<IFilesPickerFactory, FilesPickerFactory>();
        Services.AddSingleton<IFlowLayoutFactory, FlowLayoutFactory>();
        Services.AddSingleton<IImagePreviewFactory, ImagePreviewFactory>();
        Services.AddSingleton<IInputFactory, InputFactory>();
        Services.AddSingleton<IModularFormFactory, ModularFormFactory>();
        Services.AddSingleton<ITabbedLayoutFactory, TabbedLayoutFactory>();
        Services.AddSingleton<IThemePickerFactory, ThemePickerFactory>();
    }

    /// <summary>
    /// Registers the main form type for Android.
    /// On Android, this stores the form type in the registry and returns immediately.
    /// The framework's MainActivity will create the form when the Activity starts.
    /// </summary>
    public void Run<T>()
        where T : IForm
    {
        Debug.Log(LogCategory.App, $"Registering main form for Android: {typeof(T).Name}");

        ConfirmTheme();
        ConfirmIconProvider();

        // Store the form type for the Activity to use
        AndroidFormRegistry.MainFormType = typeof(T);

        Debug.Log(LogCategory.App, "Android form registered. Activity will create UI when launched.");
        // On Android, we don't run a loop - the Activity lifecycle takes over
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
}
