using System.Runtime.InteropServices;
using CrossSharp.Application.Constants;
using CrossSharp.Ui;
namespace CrossSharp.Application;

static class GtkApplicationRunner {
    [DllImport("libgtk-4.so.1")] static extern IntPtr gtk_application_new(string appId, int flags);
    [DllImport("libgtk-4.so.1")] static extern int g_application_run(IntPtr app, int argc, IntPtr argv);
    [DllImport("libgobject-2.0.so.0")] static extern ulong g_signal_connect_data(
        IntPtr instance, string detailed_signal, Delegate handler,
        IntPtr data, IntPtr destroy_data, uint connect_flags);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void ActivateCallback(IntPtr app, IntPtr user_data);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool CloseRequestCallback(IntPtr window, IntPtr user_data);

    internal static void Run<T>() where T : Form, new()
    {
        IntPtr app = gtk_application_new("org.example.GtkApp", 0); // TODO: App ID

        g_signal_connect_data(app, "activate", (ActivateCallback) OnActivate, IntPtr.Zero, IntPtr.Zero, 0);
        g_signal_connect_data(app, "close-request", (CloseRequestCallback) OnClose, IntPtr.Zero, IntPtr.Zero, 0);

        g_application_run(app, 0, IntPtr.Zero);
        return;

        void OnActivate(IntPtr appPtr, IntPtr _) {
            T form = new() {
                Handle = appPtr
            };
            form.Show();
        }
        
        bool OnClose(IntPtr windowPtr, IntPtr _) {
            // Handle any cleanup if necessary
            GeneralConstants.AppLoop.Dispose();
            return false; // Return true to prevent the window from closing
        }
    }
}