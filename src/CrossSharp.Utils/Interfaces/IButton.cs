namespace CrossSharp.Utils.Interfaces;

public interface IButton : IControl, IClickable, IBackgroundColorProvider, ISizeProvider
{
    string Text { get; set; }
}
