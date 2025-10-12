using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Button
{
    public EventHandler? OnTextChange { get; set; }

    void OnTextChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnTextChange();
    }

    void RaiseOnTextChange() => OnTextChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnBackgroundColorChange { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnBackgroundColorChange();
    }

    void RaiseOnBackgroundColorChange() => OnBackgroundColorChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnClick { get; set; }

    void RaiseOnClick() => OnClick?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnTagChange { get; set; }

    void OnTagChangedInternal()
    {
        RaiseOnTagChange();
    }

    void RaiseOnTagChange() => OnTagChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnTextAlignmentChange { get; set; }

    void OnTextAlignmentChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnTextAlignmentChange();
    }

    void RaiseOnTextAlignmentChange() => OnTextAlignmentChange?.Invoke(this, EventArgs.Empty);
}
