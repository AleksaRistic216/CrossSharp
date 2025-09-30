using System.Runtime.InteropServices;

public static class GtkTimeoutInvoker
{
    // Delegate matching GSourceFunc signature
    private delegate bool GSourceFunc(IntPtr data);

    // P/Invoke declaration for g_timeout_add
    [DllImport("libglib-2.0.so.0")]
    private static extern uint g_timeout_add(uint interval, GSourceFunc func, IntPtr data);

    /// <summary>
    /// Schedules the given action to run after a timeout (in milliseconds).
    /// </summary>
    public static void InvokeAfterTimeout(uint milliseconds, Action action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        GSourceFunc wrapper = data =>
        {
            action();
            return false; // Run once
        };

        GCHandle.Alloc(wrapper); // Prevent GC

        g_timeout_add(milliseconds, wrapper, IntPtr.Zero);
    }
}
