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
    CancellationTokenSource? _formDragCancellationTokenSource;

    // ReSharper disable once NotAccessedField.Local
    Task? _formDragTask;
    Point? _formDragDestination;
    int CoreFps => Services.GetSingleton<IApplicationConfiguration>().CoreFps;
    DateTime? _lastFormDragTime;
    const int MOVEMENT_THRESHOLD = 1;
    Point? _mouseDownMousePosition;
    Point? _mouseDownFormPosition;
    int _deltaX;
    int _deltaY;
    DateTime? _lastClickTime;
    Point? _lastClickPosition;
    const int DOUBLE_CLICK_THRESHOLD_MS = 500;
    const int DOUBLE_CLICK_DISTANCE_THRESHOLD = 5;
}
