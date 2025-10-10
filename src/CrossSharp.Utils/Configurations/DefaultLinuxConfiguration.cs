using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Configurations;

public class DefaultLinuxConfiguration : ILinuxConfiguration
{
    public LinuxWidgetLibrary WidgetLibrary { get; } = LinuxWidgetLibrary.CrossSharpSDK;
}
