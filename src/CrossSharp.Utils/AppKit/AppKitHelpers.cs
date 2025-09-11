using System.Runtime.InteropServices;

namespace CrossSharp.Utils.AppKit;

public static class AppKitHelpers
{
    const string objcLib = "/usr/lib/libobjc.A.dylib";

    [DllImport(objcLib)]
    public static extern IntPtr sel_registerName(string name);

    [DllImport(objcLib)]
    public static extern IntPtr objc_getClass(string name);

    [DllImport(objcLib)]
    public static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector);

    [DllImport(objcLib)]
    public static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);
    
}