using System.Runtime.InteropServices;

namespace CrossSharp.Utils.Gtk;

class GtkGrid : GtkWidget
{
    internal GtkGrid()
        : base(GtkHelpers.gtk_grid_new())
    {
        SetColumnSpacing(0);
        SetRowSpacing(0);
    }

    [DllImport(GtkHelpers.GTK)]
    static extern void gtk_grid_attach(
        IntPtr grid,
        IntPtr child,
        int column,
        int row,
        int width,
        int height
    );

    internal void Attach(IntPtr child, int column, int row, int width, int height)
    {
        gtk_grid_attach(Handle, child, column, row, width, height);
    }

    [DllImport(GtkHelpers.GTK)]
    static extern void gtk_grid_set_row_homogeneous(IntPtr grid, bool homogeneous);

    [DllImport(GtkHelpers.GTK)]
    static extern void gtk_grid_set_column_spacing(IntPtr grid, uint spacing);

    internal void SetColumnSpacing(uint spacing)
    {
        gtk_grid_set_column_spacing(Handle, spacing);
    }

    [DllImport(GtkHelpers.GTK)]
    static extern void gtk_grid_set_row_spacing(IntPtr grid, uint spacing);

    internal void SetRowSpacing(uint spacing)
    {
        gtk_grid_set_row_spacing(Handle, spacing);
    }
}
