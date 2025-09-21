namespace CrossSharp.Utils.Interfaces;

public interface IControlsContainerFactory
{
    IControlsContainer Create(IntPtr parentHandle, ISizeProvider sizeProvider);
}
