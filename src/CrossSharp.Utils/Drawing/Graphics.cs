namespace CrossSharp.Utils.Drawing;

public class Graphics {
    internal IntPtr ContextHandle { get; set; }
    public Graphics(IntPtr contextHandle) {
        ContextHandle = contextHandle;
    }
}