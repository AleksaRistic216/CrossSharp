namespace CrossSharp.Utils.Gtk;

class GtkWidget
{
    internal IntPtr Handle { get; }

    internal GtkWidget(IntPtr handle)
    {
        Handle = handle;
        if (Handle == IntPtr.Zero)
            throw new Exception("Invalid GTK widget handle");
    }
}
