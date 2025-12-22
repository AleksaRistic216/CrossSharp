using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
struct SDL_WindowEvent
{
    public uint type; // SDL_EVENT_WINDOW_*
    public uint reserved;
    public ulong timestamp; // In nanoseconds
    public uint windowID; // ID of the window that triggered the event
    public int data1;
    public int data2;
}
