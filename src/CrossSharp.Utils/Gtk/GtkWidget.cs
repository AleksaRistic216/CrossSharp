using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Gtk;

public abstract class GtkWidget : Control
{
    public override IntPtr Handle { get; set; } = GtkHelpers.gtk_drawing_area_new();
    Graphics _g;

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
        GtkHelpers.gtk_fixed_put(ParentHandle, Handle, Location.X, Location.Y);
        Visible = true;
    }

    void OnDraw(IntPtr sender, IntPtr cr, int width, int height, IntPtr data)
    {
        _g = new Graphics(cr);
        DrawShadows(_g);
        DrawBackground(_g);
        DrawBorders(_g);
        DrawContent(_g);
        _g.Dispose();
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
        IntPtr cr = g.ContextHandle; // cairo_t*
        Color developerBorderColor = Color.Green;
        const float strokeThickness = 2.0f;
        GtkHelpers.cairo_set_source_rgb(
            cr,
            developerBorderColor.R,
            developerBorderColor.G,
            developerBorderColor.B
        ); // Blue
        GtkHelpers.cairo_set_line_width(cr, strokeThickness); // Border thickness
        GtkHelpers.cairo_rectangle(
            cr,
            Location.X,
            Location.Y,
            Width - Location.X - strokeThickness,
            Height - Location.X - strokeThickness
        );
        GtkHelpers.cairo_stroke(cr); // Draw border only
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

    public override void Dispose()
    {
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }
}
