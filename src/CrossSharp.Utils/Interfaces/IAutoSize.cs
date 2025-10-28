namespace CrossSharp.Utils.Interfaces;

public interface IAutoSize : IMaxSize
{
    bool AutoSize { get; set; }
    // Rectangle GetAutoSizeBounds();
}
