using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Gtk;

public abstract class GtkWidget : Control, IGtkWidget
{
    Graphics? _g;
    readonly IApplication _application = ServicesPool.GetSingleton<IApplication>();
    public override IntPtr Handle { get; set; } = GtkHelpers.gtk_drawing_area_new();

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
        if (_application.DevelopersMode)
            DrawDevelopersBorders(_g!);
        _g!.Dispose();
    }

    public abstract void DrawShadows(Graphics g);

    public abstract void DrawBackground(Graphics g);

    public abstract void DrawBorders(Graphics g);

    public abstract void DrawContent(Graphics g);

    void DrawDevelopersBorders(Graphics g)
    {
        if (_application.DevelopersMode == false)
            return;
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Linux:
                DrawDevelopersBordersLinux(g);
                break;
            case CrossPlatformType.Windows:
                throw new NotImplementedException();
            case CrossPlatformType.MacOs:
                throw new NotImplementedException();
            case CrossPlatformType.Undefined:
            default:
                throw new NotSupportedException("Platform not supported");
        }
    }

    void DrawDevelopersBordersLinux(Graphics g)
    {
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

    public override void Dispose() { }
}
