namespace CrossSharp.Utils.Interfaces;

public interface ITabbedLayout : IControlsContainer
{
    void AddTab(string title, Type content);
    void AddTabButton(string text, Action onClick);
    void RemoveTab(string title);
    void SelectTab(string title);
}
