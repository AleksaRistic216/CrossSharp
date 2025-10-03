using System.Runtime.InteropServices;

namespace CrossSharp.Utils.Helpers;

static class GCHelpers
{
    static List<GCHandle> _handles = [];

    public static void Alloc(object obj) => _handles.Add(GCHandle.Alloc(obj));

    public static void FreeAll()
    {
        foreach (var handle in _handles)
            handle.Free();
        _handles.Clear();
    }
}
