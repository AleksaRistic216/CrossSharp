namespace CrossSharp.Utils.Interfaces;

public interface IApplication {
    public IntPtr MainWindowHandle { get; internal set; }
}