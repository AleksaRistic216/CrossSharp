namespace CrossSharp.Utils.Interfaces;

public interface IPanel
    : IGtkWidget,
        ISizeProvider,
        IBackgroundColorProvider,
        ICenterPanelChild,
        IForegroundColorProvider;
