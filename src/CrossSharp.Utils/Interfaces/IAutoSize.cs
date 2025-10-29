namespace CrossSharp.Utils.Interfaces;

public interface IAutoSize : IMaxSize, IMinSize
{
    bool AutoSize { get; set; }
    // Rectangle GetAutoSizeBounds();
}
