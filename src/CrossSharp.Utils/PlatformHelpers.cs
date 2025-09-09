using CrossSharp.Utils.Enums;
namespace CrossSharp.Utils;

public static class PlatformHelpers {
    static CrossPlatformType? _currentPlatform;
    public static CrossPlatformType GetCurrentPlatform() {
        if (_currentPlatform.HasValue)
            return _currentPlatform.Value;
        if (OperatingSystem.IsWindows())
            _currentPlatform = CrossPlatformType.Windows;
        if (OperatingSystem.IsLinux())
            _currentPlatform = CrossPlatformType.Linux;
        if (OperatingSystem.IsMacOS())
            _currentPlatform = CrossPlatformType.MacOs;
        if(!_currentPlatform.HasValue)
            _currentPlatform = CrossPlatformType.Undefined;
        return _currentPlatform.Value;
    }
}