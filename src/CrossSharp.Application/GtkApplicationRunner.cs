using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

static class GtkApplicationRunner
{
    internal static void Run<T>()
        where T : IForm
    {
        var app = GtkHelpers.gtk_application_new("org.example.GtkApp", 0); // TODO: App ID
        GtkHelpers.g_signal_connect_data(
            app,
            "activate",
            (GtkHelpers.ActivateCallback)OnActivate,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
        _ = GtkHelpers.g_application_run(app, 0, IntPtr.Zero);
        return;

        void OnActivate(IntPtr appPtr, IntPtr _)
        {
            var appInstance = Services.GetSingleton<IApplication>();
            appInstance.MainFormType = typeof(T);
            appInstance.MainWindowHandle = appPtr;
            appInstance.Start();
        }
    }
}
