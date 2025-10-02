using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Linux;

public class Control : Utils.Control
{
    Graphics? _g;
    bool _initialized;
    readonly IApplication _application = Services.GetSingleton<IApplication>();
    public override IntPtr Handle { get; set; } = GtkHelpers.gtk_drawing_area_new();

    public override void Initialize()
    {
        if (_initialized)
            return;
        GtkHelpers.DrawFunc drawFunc = OnDraw;
        GtkHelpers.gtk_drawing_area_set_draw_func(Handle, drawFunc, IntPtr.Zero, IntPtr.Zero);
        _initialized = true;
    }

    public override void Invalidate()
    {
        InvalidateVisibility();
    }

    void InvalidateVisibility()
    {
        if (Handle == IntPtr.Zero)
            return;
        var form = GetForm();
        if (form is null)
            return;
        // TODO: Should be client bounds but for now we are ok with this
        Rectangle formBounds = new(0, 0, form.Width, form.Height);
        var controlBounds = GetFormRelativeBounds();
        Visible = formBounds.Contains(controlBounds);
    }

    public override void Show()
    {
        if (!GetIsAlreadyBoundToParent())
            GtkHelpers.gtk_fixed_put(ParentHandle, Handle, 0, 0);
        InvalidateVisibility();
    }

    bool GetIsAlreadyBoundToParent()
    {
        IntPtr parent = GtkHelpers.gtk_widget_get_parent(Handle);
        return parent == ParentHandle;
    }

    void OnDraw(IntPtr sender, IntPtr cr, int width, int height, IntPtr data)
    {
        if (cr == IntPtr.Zero)
            return;
        _g = new Graphics(cr, this, this);
        ParentalLimitClip(ref _g);
        DrawShadows(_g!);
        DrawBackground(_g!);
        DrawBorders(_g!);
        DrawContent(_g!);
        if (_application.DevelopersMode)
            DrawDevelopersBorders(_g!);
        _g!.Dispose();
    }

    void ParentalLimitClip(ref Graphics graphics)
    {
        var clipLimiters = GetClipLimiters();
        while (clipLimiters.Count > 0)
        {
            var cl = clipLimiters.Pop();
            cl.LimitClip(ref graphics);
        }
    }

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
}
