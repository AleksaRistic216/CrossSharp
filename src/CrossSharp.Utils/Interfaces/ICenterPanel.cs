namespace CrossSharp.Utils.Interfaces;

public interface ICenterPanel : IGtkWidget, IBackgroundColorProvider, ISizeProvider
{
    IGtkWidget? Child { get; set; }
}
