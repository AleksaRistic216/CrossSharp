namespace CrossSharp.Utils.Interfaces;

public interface IControlsContainer : ICollection<IControl>
{
    List<IControl> Items { get; }
};
