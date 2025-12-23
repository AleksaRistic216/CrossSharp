using CrossSharp.Utils.DI;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

class Application : IApplication
{
    bool _developersMode;
    IForm? _mainForm;
    public IForm MainForm => _mainForm ?? throw new InvalidOperationException("Application not started");
    public Type? MainFormType { get; set; }
    public IntPtr MainWindowHandle { get; set; }
    public bool DevelopersMode
    {
        get => _developersMode;
        set
        {
            if (_developersMode == value)
                return;
            _developersMode = value;
            Debug.Log(LogCategory.App, $"Developers mode: {(_developersMode ? "enabled" : "disabled")}");
            RaiseDevelopersModeChanged();
        }
    }
    public EventHandler? DevelopersModeChanged { get; set; }

    public HashSet<IForm> Forms { get; } = [];

    public void SetTheme(ITheme theme)
    {
        Debug.Log(LogCategory.Theme, $"Setting application theme: {theme.GetType().Name}");
        Services.AddSingleton(theme, true);
    }

    public void Start()
    {
        if (MainFormType is null)
        {
            Debug.LogError("MainFormType is null");
            throw new NullReferenceException(nameof(MainFormType));
        }
        Debug.Log(LogCategory.App, $"Starting application with form: {MainFormType.Name}");
        _mainForm = (IForm)Activator.CreateInstance(MainFormType)!;
        _mainForm.OnClose += OnMainFormClose;
        _mainForm.Show();
        _mainForm.Invalidate();
        Debug.Log(LogCategory.App, "Application started");
    }

    public EventHandler? Tick { get; set; }

    void OnMainFormClose(object? sender, System.EventArgs e)
    {
        Debug.Log(LogCategory.App, "Main form closed, disposing application loop");
        Services.GetSingleton<IApplicationLoop>().Dispose();
    }

    void RaiseDevelopersModeChanged()
    {
        DevelopersModeChanged?.Invoke(this, System.EventArgs.Empty);
    }
}
