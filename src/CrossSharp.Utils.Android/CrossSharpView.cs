using Android.Content;
using Android.Graphics;
using Android.Views;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Android;

/// <summary>
/// A custom Android View that renders CrossSharp controls using the IGraphics interface.
/// </summary>
public class CrossSharpView : View
{
    private IControl? _rootControl;

    public CrossSharpView(Context context)
        : base(context)
    {
        SetWillNotDraw(false);
    }

    /// <summary>
    /// Sets the root control to be rendered by this view.
    /// </summary>
    public IControl? RootControl
    {
        get => _rootControl;
        set
        {
            _rootControl = value;
            if (_rootControl != null)
            {
                _rootControl.Invalidated += (_, _) => RequestRedraw();
            }
            RequestRedraw();
        }
    }

    /// <summary>
    /// Requests a redraw of the view on the next frame.
    /// </summary>
    public void RequestRedraw()
    {
        PostInvalidate();
    }

    protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
    {
        base.OnSizeChanged(w, h, oldw, oldh);

        if (_rootControl != null)
        {
            _rootControl.Width = w;
            _rootControl.Height = h;
            _rootControl.Invalidate();
        }
    }

    protected override void OnDraw(Canvas? canvas)
    {
        base.OnDraw(canvas);

        if (canvas == null || _rootControl == null)
            return;

        // Create graphics context for this frame
        using var graphics = new AndroidGraphics(canvas);
        IGraphics graphicsInterface = graphics;

        // Perform theming if needed
        _rootControl.PerformTheme();

        // Draw the control hierarchy
        _rootControl.Draw(ref graphicsInterface);
    }

    protected override void OnDetachedFromWindow()
    {
        base.OnDetachedFromWindow();
        _rootControl?.Dispose();
    }
}
