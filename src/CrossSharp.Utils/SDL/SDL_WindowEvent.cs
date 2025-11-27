using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

// SDL3 SDL_WindowEvent mapping
[StructLayout(LayoutKind.Sequential)]
struct SDL_WindowEvent
{
    public uint type; // SDL_EVENT_WINDOW_* (SDL3)
    public uint reserved; // reserved field in SDL3 (instead of timestamp/windowID)
    public uint windowID; // ID of the window that triggered the event

    [MarshalAs(UnmanagedType.U1)]
    public byte @event; // SDL_EVENT_WINDOW_CLOSE, etc. (name is 'event' in SDL3)
    public byte padding1;
    public byte padding2;
    public byte padding3;
    public int data1;
    public int data2;
}
