namespace CrossSharp.Utils.Interfaces;

public interface IClickable : IIsMouseOverProvider
{
    EventHandler? OnClick { get; set; }
}
