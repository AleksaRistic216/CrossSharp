namespace CrossSharp.Utils.Interfaces;

public interface IForm : IControl, ISizeProvider, ILocationProvider
{
    string Title { get; set; }
    IApplication AppInstance { get; }
    IControlsContainer Controls { get; }
    EventHandler? OnShow { get; set; }
    EventHandler? OnClose { get; set; }
}
