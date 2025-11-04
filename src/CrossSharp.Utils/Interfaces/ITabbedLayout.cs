namespace CrossSharp.Utils.Interfaces;

public interface ITabbedLayout : IControlsContainer
{
    EventHandler? CurrentTabChanged { get; set; }
    void AddTab(string title, Type content);

    /// <summary>
    /// Creates, adds and returns a new tab button instance of the tabbed layout.
    /// Button is already styled according to the tabbed layout style.
    /// </summary>
    /// <returns></returns>
    IButton CreateTabButton();
    void RemoveTab(string title);
    void SelectTab(string title);
}
