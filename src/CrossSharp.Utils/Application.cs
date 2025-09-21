using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

class Application : IApplication
{
    bool _developersMode = false;
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

    public void SetTheme(ITheme theme)
    {
        ServicesPool.AddSingleton(theme, true);
    }

    void RaiseDevelopersModeChanged()
    {
        DevelopersModeChanged?.Invoke(this, EventArgs.Empty);
    }
}
