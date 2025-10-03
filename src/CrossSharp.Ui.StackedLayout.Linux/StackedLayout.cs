using System.Collections;
using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class StackedLayout : Panel, IStackedLayout
{
    public int Count => Items.Count;
    public bool IsReadOnly => false;
    Direction _itemsDirection = Direction.Vertical;
    public Direction ItemsDirection
    {
        get => _itemsDirection;
        set
        {
            if (_itemsDirection == value)
                return;
            _itemsDirection = value;
            Invalidate();
        }
    }

    Dictionary<IControl, ControlSizing> _sizing = new();

    public void SetItemSizing(IControl control, ControlSizing sizing)
    {
        ArgumentNullException.ThrowIfNull(control);
        if (!Items.Contains(control))
            throw new Exception("Control is not child of this layout");
        _sizing[control] = sizing;
        Invalidate();
    }

    public List<IControl> Items { get; } = [];

    public override void Invalidate()
    {
        base.Invalidate();
        Items.ForEach(x => x.Invalidate());
        if (ItemsDirection == Direction.Vertical)
        {
            foreach (IControl control in Items.Where(control => control.Width != Width))
                control.Width = Width;
            StackVertically();
        }
        else
        {
            foreach (IControl control in Items.Where(control => control.Height != Height))
                control.Height = Height;
            StackHorizontally();
        }
    }

    List<IControl> GetControlsWithoutSizing() => Items.Where(c => !_sizing.ContainsKey(c)).ToList();

    void StackHorizontally()
    {
        int x = 0;
        var nonFillControls = GetControlsWithoutSizing();
        int totalNonFillWidth = nonFillControls.Sum(c => c.Width);
        int fillCount = Items.Count - nonFillControls.Count; // Later with other sizing this should be changed
        int availableWidth = Width - totalNonFillWidth;
        foreach (IControl control in Items)
        {
            control.Location = new Point(x, 0);
            if (
                _sizing.TryGetValue(control, out ControlSizing sizing)
                && sizing == ControlSizing.Fill
            )
                control.Width = availableWidth / fillCount;
            x += control.Width;
        }
    }

    void StackVertically()
    {
        int y = 0;
        var nonFillControls = GetControlsWithoutSizing();
        int totalNonFillHeight = nonFillControls.Sum(c => c.Height);
        int fillCount = Items.Count - nonFillControls.Count;
        int availableHeight = Height - totalNonFillHeight;

        foreach (IControl control in Items)
        {
            control.Location = new Point(0, y);
            if (
                _sizing.TryGetValue(control, out ControlSizing sizing)
                && sizing == ControlSizing.Fill
            )
                control.Height = availableHeight / fillCount;
            y += control.Height;
        }
    }

    public override void DrawBorders(ref Graphics g)
    {
        base.DrawBorders(ref g);
        foreach (IControl control in Items)
            control.DrawBorders(ref g);
    }

    public override void DrawContent(ref Graphics g)
    {
        g.ResetOffset();
        base.DrawContent(ref g);
        g.SetOffset(Location.X, Location.Y);
        foreach (IControl control in Items)
            control.DrawContent(ref g);
    }

    public override void DrawShadows(ref Graphics g)
    {
        g.ResetOffset();
        base.DrawShadows(ref g);
        g.SetOffset(Location.X, Location.Y);
        foreach (IControl control in Items)
            control.DrawShadows(ref g);
    }

    public override void DrawBackground(ref Graphics g)
    {
        g.ResetOffset();
        base.DrawBackground(ref g);
        g.SetOffset(Location.X, Location.Y);
        foreach (IControl control in Items)
            control.DrawBackground(ref g);
    }

    IEnumerator<IControl> IEnumerable<IControl>.GetEnumerator() => Items.GetEnumerator();

    public IEnumerator GetEnumerator() => Items.GetEnumerator();

    public bool Remove(IControl item) => Items.Remove(item);

    public void Add(IControl control)
    {
        ArgumentNullException.ThrowIfNull(control);
        control.Parent = this;
        Items.Add(control);
    }

    public void Clear() => Items.Clear();

    public bool Contains(IControl item) => Items.Contains(item);

    public void CopyTo(IControl[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);
}
