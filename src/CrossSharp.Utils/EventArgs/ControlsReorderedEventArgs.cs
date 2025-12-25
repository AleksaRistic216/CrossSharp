using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.EventArgs;

public class ControlsReorderedEventArgs(IControl control, int oldIndex, int newIndex) : System.EventArgs
{
    public IControl Control { get; } = control;
    public int OldIndex { get; } = oldIndex;
    public int NewIndex { get; } = newIndex;
}
