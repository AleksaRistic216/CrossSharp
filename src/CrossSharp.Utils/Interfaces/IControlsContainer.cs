namespace CrossSharp.Utils.Interfaces;

public interface IControlsContainer : IControl, IEnumerable<IControl>
{
    void Add(params IControl[] controls);
    void Remove(params IControl[] controls);
};
