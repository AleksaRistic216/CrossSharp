using CrossSharp.Utils.Interfaces;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Application")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Form")]

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

    void RaiseDevelopersModeChanged()
    {
        DevelopersModeChanged?.Invoke(this, EventArgs.Empty);
    }
}
