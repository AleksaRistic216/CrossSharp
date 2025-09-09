using CrossSharp.Application.Constants;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Application;

static class GtkApplicationRunner {

    static Form _mainForm;
    internal static void Run<T>() where T : Form, new()
    {
        IntPtr app = GtkHelpers.gtk_application_new("org.example.GtkApp", 0); // TODO: App ID
        GtkHelpers.g_signal_connect_data(app, "activate", (GtkHelpers.ActivateCallback) OnActivate, IntPtr.Zero, IntPtr.Zero, 0);
        GtkHelpers.g_application_run(app, 0, IntPtr.Zero);
        return;

        void OnActivate(IntPtr appPtr, IntPtr _) {
            IApplication appInstance = ServicesPool.GetSingleton<IApplication>();
            appInstance.MainWindowHandle = appPtr;
            _mainForm = new T();
            _mainForm.OnClose += (_, _) => {
                GeneralConstants.AppLoop.Dispose();
            };
            _mainForm.Show();
        }
    }
}