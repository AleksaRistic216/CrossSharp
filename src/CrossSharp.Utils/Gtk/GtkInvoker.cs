using System.Runtime.InteropServices;
using CrossSharp.Utils.Gtk;

static class GtkInvoker
{
    const int SAFE_TIMEOUT = 50;

    // Delegate matching GSourceFunc signature
    private delegate bool GSourceFunc(IntPtr data);

    // P/Invoke declaration for g_idle_add
    [DllImport(GtkHelpers.GTK)]
    private static extern uint g_idle_add(GSourceFunc func, IntPtr data);

    /// <summary>
    /// Schedules the given action to run when GTK enters idle state.
    /// </summary>
    internal static void InvokeWhenIdle(Action action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        // Wrap the action in a GSourceFunc-compatible delegate
        GCHandle? handle = null;
        GSourceFunc wrapper = data =>
        {
            action();
            handle?.Free(); // Free the GCHandle
            return false; // Return false to run only once
        };

        // Pin the delegate to prevent GC
        handle = GCHandle.Alloc(wrapper);

        // Schedule the idle callback
        g_idle_add(wrapper, IntPtr.Zero);
    }

    [DllImport(GtkHelpers.GTK)]
    private static extern uint g_timeout_add(uint interval, GSourceFunc func, IntPtr data);

    /// <summary>
    /// Schedules the given action to run after a timeout (in milliseconds).
    /// </summary>
    internal static void InvokeAfterTimeout(uint milliseconds, Action action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        GCHandle? handle = null;
        GSourceFunc wrapper = data =>
        {
            action();
            handle?.Free(); // Free the GCHandle
            return false; // Run once
        };

        handle = GCHandle.Alloc(wrapper); // Prevent GC

        g_timeout_add(milliseconds, wrapper, IntPtr.Zero);
    }

    internal static void InvokeSafe(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);
        InvokeWhenIdle(() =>
        {
            InvokeAfterTimeout(SAFE_TIMEOUT, action);
        });
    }
}
