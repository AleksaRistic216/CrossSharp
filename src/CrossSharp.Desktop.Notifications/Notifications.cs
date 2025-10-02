using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Desktop;

public static class Notifications
{
    static IDesktopNotification _impl;

    static Notifications()
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Windows:
                throw new NotImplementedException();
            case CrossPlatformType.Linux:
                _impl = new NotificationsLinux();
                break;
            case CrossPlatformType.MacOs:
                throw new NotImplementedException();
            case CrossPlatformType.Undefined:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void Show(string title, string message) => _impl.Show(title, message);
}
