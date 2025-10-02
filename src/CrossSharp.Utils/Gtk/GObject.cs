namespace CrossSharp.Utils.Gtk;

class GObject
{
    protected readonly IntPtr Handle;

    internal GObject(IntPtr handle)
    {
        Handle = handle;
    }
}
