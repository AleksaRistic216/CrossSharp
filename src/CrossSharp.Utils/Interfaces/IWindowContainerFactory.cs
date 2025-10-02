namespace CrossSharp.Utils.Interfaces;

public interface IWindowContainerFactory
{
    IWindowContainer Create(
        IntPtr parentHandle,
        ISizeProvider sizeProvider,
        IBackgroundColorProvider backgroundColorProvider
    );
}
