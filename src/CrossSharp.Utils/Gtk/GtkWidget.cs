using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Gtk;

public abstract class GtkWidget : Control
{
    public override IntPtr Handle { get; set; } = GtkHelpers.gtk_drawing_area_new();
    Graphics? _g;
    readonly IApplication _application;

    protected GtkWidget()
    {
        _application = ServicesPool.GetSingleton<IApplication>();
    }

    public override void Initialize()
    {
        GtkHelpers.DrawFunc drawFunc = OnDraw;
        GtkHelpers.gtk_drawing_area_set_draw_func(Handle, drawFunc, IntPtr.Zero, IntPtr.Zero);
    }

    public override void Show()
    {
        IntPtr parent = GtkHelpers.gtk_widget_get_parent(Handle);
        if (parent == ParentHandle)
            return;
        GtkHelpers.gtk_widget_show(Handle);
        GtkHelpers.gtk_fixed_put(ParentHandle, Handle, 0, 0);
        Visible = true;
    }

    void OnDraw(IntPtr sender, IntPtr cr, int width, int height, IntPtr data)
    {
        _g = new Graphics(cr, this, this);
        DrawShadows(_g!);
        DrawBackground(_g!);
        DrawBorders(_g!);
        DrawContent(_g!);
        _g!.Dispose();
    }

    void DrawShadows(Graphics g)
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Linux:
                DrawShadowsLinux(g);
                break;
            case CrossPlatformType.Windows:
                DrawShadowsWindows(g);
                break;
            case CrossPlatformType.MacOs:
                DrawShadowsMacOs(g);
                break;
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Platform not supported");
        }
    }

    protected abstract void DrawShadowsLinux(Graphics g);
    protected abstract void DrawShadowsWindows(Graphics g);
    protected abstract void DrawShadowsMacOs(Graphics g);

    void DrawBackground(Graphics g)
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Linux:
                DrawBackgroundLinux(g);
                break;
            case CrossPlatformType.Windows:
                DrawBackgroundWindows(g);
                break;
            case CrossPlatformType.MacOs:
                DrawBackgroundMacOs(g);
                break;
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Platform not supported");
        }
    }

    protected abstract void DrawBackgroundLinux(Graphics g);
    protected abstract void DrawBackgroundWindows(Graphics g);
    protected abstract void DrawBackgroundMacOs(Graphics g);

    void DrawBorders(Graphics g)
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Linux:
                DrawDevelopersBordersLinux(g);
                DrawBordersLinux(g);
                break;
            case CrossPlatformType.Windows:
                DrawBordersWindows(g);
                break;
            case CrossPlatformType.MacOs:
                DrawBordersMacOs(g);
                break;
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Platform not supported");
        }
    }

    void DrawDevelopersBordersLinux(Graphics g)
    {
        if (_application.DevelopersMode == false)
            return;
        ColorRgba developerBorderColor = ColorRgba.Green;
        const float strokeThickness = 2.0f;
        g.DrawRectangle(
            Location.X + 1,
            Location.Y + 1,
            Width - 1,
            Height - 1,
            developerBorderColor,
            strokeThickness
        );
    }

    protected abstract void DrawBordersLinux(Graphics g);
    protected abstract void DrawBordersWindows(Graphics g);
    protected abstract void DrawBordersMacOs(Graphics g);

    void DrawContent(Graphics g)
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Linux:
                DrawContentLinux(g);
                break;
            case CrossPlatformType.Windows:
                DrawContentWindows(g);
                break;
            case CrossPlatformType.MacOs:
                DrawContentMacOs(g);
                break;
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Platform not supported");
        }
    }

    protected abstract void DrawContentLinux(Graphics g);
    protected abstract void DrawContentWindows(Graphics g);
    protected abstract void DrawContentMacOs(Graphics g);

    public override void Dispose() { }
}
