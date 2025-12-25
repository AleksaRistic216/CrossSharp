using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar
{
    #region Constants

    const int BUTTON_WIDTH = 50;
    const int TITLE_BAR_HEIGHT = 35;
    const int DOUBLE_CLICK_THRESHOLD_MS = 500;
    const int DOUBLE_CLICK_DISTANCE_THRESHOLD = 5;
    const Icon CLOSE_ICON = Icon.Close;
    const Icon MAXIMIZE_ICON = Icon.Maximize;
    const Icon RESTORE_ICON = Icon.Restore;
    const Icon MINIMIZE_ICON = Icon.Minimize;
    static readonly SizeF ActionButtonIconScale = new(0.7f, 0.7f);

    #endregion

    #region Services

    IInputHandler InputHandler => Services.GetSingleton<IInputHandler>();
    IForm Form => (IForm)Parent!;

    #endregion

    #region Controls

    IButton _closeButton = null!;
    IButton _minimizeButton = null!;
    IButton _maximizeRestoreButton = null!;

    #endregion

    #region Double-Click Tracking

    DateTime? _lastClickTime;
    Point? _lastClickPosition;

    #endregion
}
