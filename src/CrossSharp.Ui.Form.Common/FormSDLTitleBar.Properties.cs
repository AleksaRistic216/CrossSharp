using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
partial class FormSDLTitleBar
{
    IInputHandler InputHandler => Services.GetSingleton<IInputHandler>();
    IForm Form => (IForm)Parent!;

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
}
