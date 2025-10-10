namespace CrossSharp.Utils.Linux;

public abstract class ControlBase : Utils.ControlBase
{
    public abstract override void Initialize();

    public abstract override void Invalidate();

    public abstract override void Redraw();
}
