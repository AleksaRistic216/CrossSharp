using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Explicit, Size = 128)]
struct SDL_Event
{
    [FieldOffset(0)]
    public uint type;

    [FieldOffset(0)]
    public SDL_WindowEvent window;
    // You can expand this struct to include other event data (e.g., keyboard, mouse, etc.)
}
