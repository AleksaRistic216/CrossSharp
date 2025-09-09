using CrossSharp.Utils.Interfaces;
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Application")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Form")]
namespace CrossSharp.Utils;

internal class Application : IApplication {
    public IntPtr MainWindowHandle {
        get;
        set;
    }
}