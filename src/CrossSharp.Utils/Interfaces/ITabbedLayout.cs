namespace CrossSharp.Utils.Interfaces;

public interface ITabbedLayout : IControlsContainer
{
    void AddTab(string title, Type content);
    void RemoveTab(string title);
    void SelectTab(string title);
}
