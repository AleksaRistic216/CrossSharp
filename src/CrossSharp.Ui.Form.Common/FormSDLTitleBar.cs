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
    const string MaximizeIconKey = nameof(Icon.Maximize);
    const string RestoreIconKey = nameof(Icon.Collapse);
    const string MinimizeIconKey = nameof(Icon.Minimize);

    internal FormSDLTitleBar(FormSDL form)
    {
        Parent = form;
        Form.StateChanged += FormStateChanged;
        Orientation = Orientation.Horizontal;
        Height = 35;
        Width = Form.Width;
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;
        InputHandler.MouseDragged += OnMouseDragged;
        InputHandler.MouseReleased += OnMouseReleased;

        LoadTitleBarIcons();

        var buttonWidth = 50;

        _closeButton = new Button();
        _closeButton.Text = "X";
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
        _maximizeRestoreButton.Image = EfficientImage.Get(MaximizeIconKey);
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
        _minimizeButton.Image = EfficientImage.Get(MinimizeIconKey);
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

    void LoadTitleBarIcons()
    {
        var iconProvider = Services.GetSingleton<IIconProvider>();
        var imagesCache = Services.GetSingleton<IEfficientImagesCache>();
        const int iconSize = 64;
        var iconColor = SKColors.White;

        if (!imagesCache.HasImage(MaximizeIconKey))
        {
            var svg = iconProvider.GetSvg(Icon.Maximize);
            imagesCache.AddImage(MaximizeIconKey, ImageHelpers.FromSvg(svg, iconSize, iconSize, iconColor));
        }

        if (!imagesCache.HasImage(RestoreIconKey))
        {
            var svg = iconProvider.GetSvg(Icon.Collapse);
            imagesCache.AddImage(RestoreIconKey, ImageHelpers.FromSvg(svg, iconSize, iconSize, iconColor));
        }

        if (!imagesCache.HasImage(MinimizeIconKey))
        {
            var svg = iconProvider.GetSvg(Icon.Minimize);
            imagesCache.AddImage(MinimizeIconKey, ImageHelpers.FromSvg(svg, iconSize, iconSize, iconColor));
        }
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
                ? EfficientImage.Get(RestoreIconKey)
                : EfficientImage.Get(MaximizeIconKey);
    }

    public override void Invalidate()
    {
        CornerRadius = 0;
        Width = Form.Width;
        base.Invalidate();
    }

    bool IsWithinDraggableBounds(Point screenPoint)
    {
        foreach (var button in new[] { _closeButton, _maximizeRestoreButton, _minimizeButton })
            if (button.GetScreenBounds().Contains(screenPoint))
                return false;
        return true;
    }

    void StartMovingForm()
    {
        _formDragCancellationTokenSource = new CancellationTokenSource();
        _formDragTask = Task.Run(
            () =>
            {
                while (!_formDragCancellationTokenSource.IsCancellationRequested)
                {
                    if (_formDragDestination is null)
                        continue;
                    int targetDelay = (int)(1000f / CoreFps);
                    int timeSinceLastDrag = _lastFormDragTime is null
                        ? int.MaxValue
                        : (int)(DateTime.Now - _lastFormDragTime.Value).TotalMilliseconds;
                    if (timeSinceLastDrag < targetDelay)
                        continue;
                    _lastFormDragTime = DateTime.Now;
                    Form.Move(_formDragDestination.Value);
                }
            },
            _formDragCancellationTokenSource.Token
        );
    }

    public override void Dispose()
    {
        base.Dispose();
        InputHandler.MouseMoved -= OnMouseMoved;
        InputHandler.MousePressed -= OnMousePressed;
        InputHandler.MouseDragged -= OnMouseDragged;
        InputHandler.MouseReleased -= OnMouseReleased;
        _formDragCancellationTokenSource?.Cancel();
        _formDragTask = null;
    }
}
