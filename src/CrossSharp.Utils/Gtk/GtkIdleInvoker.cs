using System.Runtime.InteropServices;
using CrossSharp.Utils.Gtk;

public static class GtkIdleInvoker
{
    // Delegate matching GSourceFunc signature
    private delegate bool GSourceFunc(IntPtr data);

    // P/Invoke declaration for g_idle_add
    [DllImport(GtkHelpers.GTK)]
    private static extern uint g_idle_add(GSourceFunc func, IntPtr data);

    /// <summary>
    /// Schedules the given action to run when GTK enters idle state.
    /// </summary>
    public static void InvokeWhenIdle(Action action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        // Wrap the action in a GSourceFunc-compatible delegate
        GSourceFunc wrapper = data =>
        {
            action();
            return false; // Return false to run only once
        };

        // Pin the delegate to prevent GC
        GCHandle.Alloc(wrapper);

        // Schedule the idle callback
        g_idle_add(wrapper, IntPtr.Zero);
    }
}
