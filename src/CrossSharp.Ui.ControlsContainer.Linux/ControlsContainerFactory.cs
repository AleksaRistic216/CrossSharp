using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class ControlsContainerFactory : IControlsContainerFactory
{
    public IControlsContainer Create(IntPtr parentHandle, ISizeProvider sizeProvider) =>
        new ControlsContainer(parentHandle, sizeProvider);
}
