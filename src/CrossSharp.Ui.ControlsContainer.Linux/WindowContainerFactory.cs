using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class WindowContainerFactory : IWindowContainerFactory
{
    public IWindowContainer Create(
        IntPtr parentHandle,
        ISizeProvider sizeProvider,
        IBackgroundColorProvider backgroundColorProvider
    ) => new WindowContainer(parentHandle, sizeProvider, backgroundColorProvider);
}
