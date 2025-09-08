using CrossSharp.Utils.Enums;
namespace CrossSharp.Utils;

public static class PlatformHelpers {
    public static CrossPlatformType GetCurrentPlatform() {
        if (OperatingSystem.IsWindows()) {
            return CrossPlatformType.Windows;
        }
        else if (OperatingSystem.IsLinux()) {
            return CrossPlatformType.Linux;
        }
        else if (OperatingSystem.IsMacOS()) {
            return CrossPlatformType.MacOs;
        }
        return CrossPlatformType.Undefined;
    }
}