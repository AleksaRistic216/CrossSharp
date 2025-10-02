namespace CrossSharp.Utils.Interfaces;

public interface ICenterPanel : IControl, IBackgroundColorProvider, ISizeProvider
{
    IControl? Child { get; set; }
}
