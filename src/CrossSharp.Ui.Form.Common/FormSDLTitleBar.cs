using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using SkiaSharp;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar : StackedLayout, IMouseTargetable
{
    #region Constructor

    internal FormSDLTitleBar(FormSDL form)
    {
        Parent = form;
        Orientation = Orientation.Horizontal;
        Height = TITLE_BAR_HEIGHT;
        Width = Form.Width;

        InitializeButtons();
        SubscribeToEvents();
    }

    #endregion

    #region Initialization

    void InitializeButtons()
    {
        _closeButton = CreateTitleBarButton(CLOSE_ICON, dockIndex: 0, onClick: Form.Close);
        _maximizeRestoreButton = CreateTitleBarButton(MAXIMIZE_ICON, dockIndex: 1, onClick: ToggleMaximize);
        _minimizeButton = CreateTitleBarButton(MINIMIZE_ICON, dockIndex: 2, onClick: Form.Minimize);

        Add(_closeButton);
        Add(_maximizeRestoreButton);
        Add(_minimizeButton);
    }

    IButton CreateTitleBarButton(Icon icon, int dockIndex, Action onClick)
    {
        var button = new Button
        {
            Image = EfficientImage.GetIcon(icon, SKColors.White),
            ImageScale = ActionButtonIconScale,
            Width = BUTTON_WIDTH,
            Height = Height,
            Dock = DockStyle.Right,
            DockIndex = dockIndex,
        };
        button.Click += (_, _) => onClick();
        button.ThemePerformed += OnTitleBarButtonThemePerformed;
        return button;
    }

    void SubscribeToEvents()
    {
        Form.StateChanged += OnFormStateChanged;
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;
    }

    void UnsubscribeFromEvents()
    {
        Form.StateChanged -= OnFormStateChanged;
        InputHandler.MouseMoved -= OnMouseMoved;
        InputHandler.MousePressed -= OnMousePressed;
    }

    #endregion

    #region Overrides

    public override void PerformTheme()
    {
        base.PerformTheme();
        CornerRadius = 0;

        var primaryColor = Services.GetSingleton<ITheme>().PrimaryColor;
        BackgroundColor = primaryColor;
        _closeButton.BackgroundColor = primaryColor;
        _maximizeRestoreButton.BackgroundColor = primaryColor;
        _minimizeButton.BackgroundColor = primaryColor;
    }

    public override void Invalidate()
    {
        CornerRadius = 0;
        Width = Form.Width;
        base.Invalidate();
    }

    public override void Dispose()
    {
        UnsubscribeFromEvents();
        base.Dispose();
    }

    #endregion

    #region Event Handlers

    void OnTitleBarButtonThemePerformed(object? sender, EventArgs e)
    {
        if (sender is IButton button)
            button.CornerRadius = 0;
    }

    void OnFormStateChanged(object? sender, EventArgs e)
    {
        var icon = Form.State == WindowState.Maximized ? RESTORE_ICON : MAXIMIZE_ICON;
        _maximizeRestoreButton.Image = EfficientImage.GetIcon(icon, SKColors.White);
    }

    #endregion
}
