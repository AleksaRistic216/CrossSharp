namespace CrossSharp.Utils.Interfaces;

public interface IFocusable
{
    bool IsFocused { get; set; }
    EventHandler? OnFocusChanged { get; set; }
}
