using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Explicit)]
struct SDL_Event
{
    [FieldOffset(0)]
    public uint type; // common event type field

    // SDL3 window event union member
    [FieldOffset(0)]
    public SDL_WindowEvent window;

    // In the future, additional SDL3 event structs (keyboard, mouse, etc.)
    // can be added here at FieldOffset(0) to mirror the full SDL_Event union.
}
