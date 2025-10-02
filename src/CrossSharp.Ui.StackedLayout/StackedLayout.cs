using System.Collections;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class StackedLayout()
    : Control<IStackedLayout>(Services.GetSingleton<IStackedLayoutFactory>().Create()),
        IStackedLayout
{
    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set { _impl.BackgroundColor = value; }
    }

    IEnumerator<IControl> IEnumerable<IControl>.GetEnumerator() => _impl.GetEnumerator();

    public IEnumerator GetEnumerator() => _impl.GetEnumerator();

    public void Add(IControl item) => _impl.Add(item);

    public void Clear() => _impl.Clear();

    public bool Contains(IControl item) => _impl.Contains(item);

    public void CopyTo(IControl[] array, int arrayIndex) => _impl.CopyTo(array, arrayIndex);

    public bool Remove(IControl item) => _impl.Remove(item);

    public int Count => _impl.Count;
    public bool IsReadOnly => false;
    public List<IControl> Items => _impl.Items;
    public Direction ItemsDirection
    {
        get => _impl.ItemsDirection;
        set => _impl.ItemsDirection = value;
    }
}
