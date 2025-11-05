using CrossSharp.Utils.Interfaces;
using DesktopNotifications;
using DesktopNotifications.FreeDesktop;

namespace CrossSharp.Desktop;

class NotificationsLinux : IDesktopNotification
{
    readonly FreeDesktopNotificationManager _manager;

    internal NotificationsLinux()
    {
        _manager = new FreeDesktopNotificationManager();
        _manager.Initialize().Wait();
    }

    public void Show(string title, string message)
    {
        _manager.Initialize();
        _manager.ShowNotification(new Notification { Title = title, Body = message });
    }
}
