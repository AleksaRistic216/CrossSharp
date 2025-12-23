using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar
{
    IInputHandler InputHandler => Services.GetSingleton<IInputHandler>();
    IForm Form => (IForm)Parent!;

    IButton _closeButton;
    IButton _minimizeButton;
    IButton _maximizeRestoreButton = null!;

    DateTime? _lastClickTime;
    Point? _lastClickPosition;
    const int DOUBLE_CLICK_THRESHOLD_MS = 500;
    const int DOUBLE_CLICK_DISTANCE_THRESHOLD = 5;
}
