namespace CrossSharp.Utils.Interfaces;

public interface IRelativeHandle
{
    IntPtr Handle { get; internal set; }
    IntPtr ParentHandle { get; set; }
    object Parent { get; set; }
}
