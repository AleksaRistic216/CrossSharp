using System.Runtime.InteropServices;
namespace CrossSharp.Ui;

public partial class Form {
    [DllImport("libgtk-4.so.1")] static extern IntPtr gtk_application_window_new(IntPtr app);
    [DllImport("libgtk-4.so.1")] static extern void gtk_window_set_title(IntPtr window, string title);
    [DllImport("libgtk-4.so.1")] static extern void gtk_window_set_default_size(IntPtr window, int width, int height);
    [DllImport("libgtk-4.so.1")] static extern void gtk_widget_show(IntPtr widget);
    void ShowLinux()
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("The form has not been initialized properly.");

        IntPtr window = gtk_application_window_new(Handle);
        gtk_window_set_title(window, Title);
        gtk_window_set_default_size(window, Width, Height);
        gtk_widget_show(window);
    }
}