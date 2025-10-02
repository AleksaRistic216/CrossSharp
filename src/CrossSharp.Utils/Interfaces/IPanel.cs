namespace CrossSharp.Utils.Interfaces;

public interface IPanel
    : IControl,
        ISizeProvider,
        IBackgroundColorProvider,
        ICenterPanelChild,
        IForegroundColorProvider;
