using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Ui;

public partial class Label : Control {
    public Label() {
        Initialize();
    }
    public override void Initialize() {
        switch(PlatformHelpers.GetCurrentPlatform()) {
            case CrossPlatformType.Windows:
                throw new NotImplementedException();
            case CrossPlatformType.Linux:
                InitializeLinux();
                break;
            case CrossPlatformType.MacOs:
                throw new NotImplementedException();
            case CrossPlatformType.Undefined:
            default:
                throw new PlatformNotSupportedException("The current platform is not supported.");
        }
    }
    public override void Invalidate() { }
    public override void Show() {
        switch(PlatformHelpers.GetCurrentPlatform()) {
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