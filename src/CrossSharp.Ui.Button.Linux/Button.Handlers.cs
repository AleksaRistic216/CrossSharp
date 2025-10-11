using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Button
{
    IInputHandler InputHandler { get; } = Services.GetSingleton<IInputHandler>();
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
}
