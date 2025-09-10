namespace CrossSharp.Utils.Drawing;

public class Graphics(IntPtr contextHandle) : IDisposable
{
    internal IntPtr ContextHandle { get; set; } = contextHandle;

    public void Dispose()
    {
        ContextHandle = IntPtr.Zero;
        GC.SuppressFinalize(this);
    }
}
