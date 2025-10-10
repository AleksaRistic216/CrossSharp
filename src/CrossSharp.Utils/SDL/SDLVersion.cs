using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct SDL_version
{
    public byte major;
    public byte minor;
    public byte patch;
}
