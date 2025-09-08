using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Ui;

public partial class Form : ILocationProvider, IRenderable, ISizeProvider {
    protected Form() {
        _applicationConfiguration = ServicesPool.GetSingleton<IApplicationConfiguration>();
    }
    public void Show() {
        CrossPlatformType platform = PlatformHelpers.GetCurrentPlatform();
        switch(platform) {
            case CrossPlatformType.Windows:
                throw new NotImplementedException();
            case CrossPlatformType.Linux:
                ShowLinux();
                break;
            case CrossPlatformType.MacOs:
                throw new NotImplementedException();
            case CrossPlatformType.Undefined:
            default:
                throw new PlatformNotSupportedException("The current platform is not supported.");
        }
    }
}
