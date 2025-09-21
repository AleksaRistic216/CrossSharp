using CrossSharp.Ui;
using CrossSharp.Utils.AppKit;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

public class AppKitApplicationRunner
{
    internal static void Run<T>()
        where T : Form, new()
    {
        var configuration = Services.GetSingleton<IApplicationConfiguration>();
        var nsAppClass = AppKitHelpers.objc_getClass("NSApplication");
        var shredAppSel = AppKitHelpers.sel_registerName("sharedApplication");
        var app = AppKitHelpers.objc_msgSend(nsAppClass, shredAppSel);

        var nsWindowClass = AppKitHelpers.objc_getClass("NSWindow");
        var allocSel = AppKitHelpers.sel_registerName("alloc");
        var initSel = AppKitHelpers.sel_registerName("init");

        var window = AppKitHelpers.objc_msgSend(
            AppKitHelpers.objc_msgSend(nsWindowClass, allocSel),
            initSel
        );

        var makeKeyAndOrderFrontSel = AppKitHelpers.sel_registerName("makeKeyAndOrderFront:");
        AppKitHelpers.objc_msgSend(window, makeKeyAndOrderFrontSel, IntPtr.Zero);

        var runSel = AppKitHelpers.sel_registerName("run");
        AppKitHelpers.objc_msgSend(app, runSel);
    }
}
