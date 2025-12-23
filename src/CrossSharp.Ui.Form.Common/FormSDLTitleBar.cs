using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using SkiaSharp;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar : StackedLayout, IMouseTargetable
{
    const Icon CLOSE_ICON = Icon.Close;
    const Icon MAXIMIZE_ICON = Icon.Maximize;
    const Icon RESTORE_ICON = Icon.Restore;
    const Icon MINIMIZE_ICON = Icon.Minimize;
    static readonly SizeF _actionButtonIconScale = new SizeF(0.7f, 0.7f);

    internal FormSDLTitleBar(FormSDL form)
    {
        Parent = form;
        Form.StateChanged += FormStateChanged;
        Orientation = Orientation.Horizontal;
        Height = 35;
        Width = Form.Width;
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;

        var buttonWidth = 50;

        _closeButton = new Button();
        _closeButton.Image = EfficientImage.GetIcon(CLOSE_ICON, SKColors.White);
        _closeButton.ImageScale = _actionButtonIconScale;
        _closeButton.Width = buttonWidth;
        _closeButton.Height = Height;
        _closeButton.Dock = DockStyle.Right;
        _closeButton.Click += (_, _) =>
        {
            Form.Close();
        };
        _closeButton.ThemePerformed += TitleBarButtonThemePerformed;
        Add(_closeButton);

        _maximizeRestoreButton = new Button();
        _maximizeRestoreButton.DockIndex = 1;
        _maximizeRestoreButton.Image = EfficientImage.GetIcon(MAXIMIZE_ICON, SKColors.White);
        _maximizeRestoreButton.ImageScale = _actionButtonIconScale;
        _maximizeRestoreButton.Width = buttonWidth;
        _maximizeRestoreButton.Height = Height;
        _maximizeRestoreButton.Dock = DockStyle.Right;
        _maximizeRestoreButton.Click += (_, _) =>
        {
            if (Form.State == WindowState.Maximized)
                Form.Restore();
            else
                Form.Maximize();
        };
        _maximizeRestoreButton.ThemePerformed += TitleBarButtonThemePerformed;
        Add(_maximizeRestoreButton);

        _minimizeButton = new Button();
        _minimizeButton.DockIndex = 2;
        _minimizeButton.Image = EfficientImage.GetIcon(MINIMIZE_ICON, SKColors.White);
        _minimizeButton.ImageScale = _actionButtonIconScale;
        _minimizeButton.Width = buttonWidth;
        _minimizeButton.Height = Height;
        _minimizeButton.Dock = DockStyle.Right;
        _minimizeButton.Click += (_, _) =>
        {
            Form.Minimize();
        };
        _minimizeButton.ThemePerformed += TitleBarButtonThemePerformed;
        Add(_minimizeButton);
    }

    void TitleBarButtonThemePerformed(object? sender, EventArgs e)
    {
        if (sender is IButton button)
            button.CornerRadius = 0;
    }

    public override void PerformTheme()
    {
        base.PerformTheme();
        CornerRadius = 0;
        BackgroundColor = Services.GetSingleton<ITheme>().PrimaryColor;
        _closeButton.BackgroundColor = BackgroundColor;
        _maximizeRestoreButton.BackgroundColor = BackgroundColor;
        _minimizeButton.BackgroundColor = BackgroundColor;
    }

    void FormStateChanged(object? sender, EventArgs e)
    {
        _maximizeRestoreButton.Image =
            Form.State == WindowState.Maximized
                ? EfficientImage.GetIcon(RESTORE_ICON, SKColors.White)
                : EfficientImage.GetIcon(MAXIMIZE_ICON, SKColors.White);
    }

    public override void Invalidate()
    {
        CornerRadius = 0;
        Width = Form.Width;
        base.Invalidate();
    }

    public override void Dispose()
    {
        base.Dispose();
        InputHandler.MouseMoved -= OnMouseMoved;
        InputHandler.MousePressed -= OnMousePressed;
    }
}
