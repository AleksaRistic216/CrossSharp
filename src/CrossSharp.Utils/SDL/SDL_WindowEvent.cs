using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
struct SDL_WindowEvent
{
    public uint type; // SDL_WINDOWEVENT
    public uint timestamp;
    public uint windowID; // ID of the window that triggered the event

    [MarshalAs(UnmanagedType.U1)]
    public byte eventType; // Use 'eventType' instead of 'event'      // SDL_WINDOWEVENT_CLOSE, etc.
    public byte padding1;
    public byte padding2;
    public byte padding3;
    public int data1;
    public int data2;
}
