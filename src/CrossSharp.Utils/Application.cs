using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

class Application : IApplication
{
    bool _developersMode;
    IForm? _mainForm;
    public IForm MainForm =>
        _mainForm ?? throw new InvalidOperationException("Application not started");
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
            RaiseDevelopersModeChanged();
        }
    }
    public EventHandler? DevelopersModeChanged { get; set; }

    public HashSet<IForm> Forms { get; } = [];

    public void SetTheme(ITheme theme)
    {
        Services.AddSingleton(theme, true);
    }

    public void Start()
    {
        if (MainFormType is null)
            throw new NullReferenceException(nameof(MainFormType));
        _mainForm = (IForm)Activator.CreateInstance(MainFormType)!;
        _mainForm.OnClose += OnMainFormClose;
        _mainForm.Show();
    }

    public EventHandler? Tick { get; set; }

    void OnMainFormClose(object? sender, System.EventArgs e)
    {
        Services.GetSingleton<IApplicationLoop>().Dispose();
    }

    void RaiseDevelopersModeChanged()
    {
        DevelopersModeChanged?.Invoke(this, System.EventArgs.Empty);
    }
}
