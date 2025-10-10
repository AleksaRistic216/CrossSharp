using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FormFactory : IFormFactory
{
    public IForm Create()
    {
        var linuxConfig = Services.GetSingleton<ILinuxConfiguration>();
        return linuxConfig.WidgetLibrary switch
        {
            LinuxWidgetLibrary.CrossSharpSDK => new FormSDL(),
            _ => throw new NotSupportedException(
                $"Linux widget library {linuxConfig.WidgetLibrary} is not supported"
            ),
        };
    }
}
