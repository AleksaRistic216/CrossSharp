namespace CrossSharp.Utils.Interfaces;

public interface ITitleBar : IMouseTargetable
{
    void Add(params IControl[] control);
}
