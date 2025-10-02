using CrossSharp.Utils.Interfaces;
using DesktopNotifications;
using DesktopNotifications.FreeDesktop;

namespace CrossSharp.Desktop;

class NotificationsLinux : IDesktopNotification
{
    static FreeDesktopNotificationManager manager;

    internal NotificationsLinux()
    {
        manager = new FreeDesktopNotificationManager();
        manager.Initialize().Wait();
    }

    public void Show(string title, string message)
    {
        manager.Initialize();
        manager.ShowNotification(new Notification { Title = title, Body = message });
    }
}
