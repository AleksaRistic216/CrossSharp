namespace CrossSharp.Utils.Interfaces;

public interface IDesktopNotification
{
    void Show(string message);
    void Show(string title, string message);
}
