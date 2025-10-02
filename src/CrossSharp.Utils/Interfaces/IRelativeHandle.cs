namespace CrossSharp.Utils.Interfaces;

public interface IRelativeHandle
{
    IntPtr Handle { get; }
    IntPtr ParentHandle { get; set; }
    object Parent { get; set; }
}
