using CrossSharp.Application.Constants;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Application;

static class GtkApplicationRunner {

    internal static void Run<T>() where T : Form, new()
    {
        var app = GtkHelpers.gtk_application_new("org.example.GtkApp", 0); // TODO: App ID
        GtkHelpers.g_signal_connect_data(app, "activate", (GtkHelpers.ActivateCallback) OnActivate, IntPtr.Zero, IntPtr.Zero, 0);
        _ = GtkHelpers.g_application_run(app, 0, IntPtr.Zero);
        return;

        void OnActivate(IntPtr appPtr, IntPtr _) {
            var appInstance = ServicesPool.GetSingleton<IApplication>();
            appInstance.MainWindowHandle = appPtr;
            Form mainForm = new T();
            mainForm.OnClose += (_, _) => {
                GeneralConstants.AppLoop.Dispose();
            };
            mainForm.Show();
        }
    }
}