namespace CrossSharp.Utils.Interfaces;

public interface IClickable : IIsMouseOverProvider
{
    EventHandler? Click { get; set; }
}
