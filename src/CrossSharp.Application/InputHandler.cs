using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using SharpHook;
using SharpHook.Data;

namespace CrossSharp.Application;

class InputHandler : IInputHandler
{
    readonly SimpleGlobalHook _hook = new();
    public event EventHandler<object>? KeyPressed;
    public event EventHandler<MouseInputArgs>? MousePressed;
    public event EventHandler<MouseInputArgs>? MouseReleased;
    public event EventHandler<MouseInputArgs>? MouseMoved;
    public event EventHandler<MouseInputArgs>? MouseWheel;
    public event EventHandler<MouseInputArgs>? MouseDragged;

    public void StartListeningAsync(CancellationToken token)
    {
        if (_hook.IsRunning)
            throw new InvalidOperationException("InputHandler hook is already running.");
        var thread = new Thread(() =>
        {
            _hook.KeyPressed += OnKeyPressed;
            _hook.MousePressed += OnMousePressed;
            _hook.MouseReleased += OnMouseReleased;
            _hook.MouseMoved += OnMouseMoved;
            _hook.MouseWheel += OnMouseWheel;
            _hook.MouseDragged += OnMouseDragged;
            _hook.RunAsync();
        });
        thread.Start();
    }

    void OnKeyPressed(object? sender, HookEventArgs e)
    {
        KeyPressed?.Invoke(sender, null);
    }

    void OnMousePressed(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseHookEventArgs;
        var args = new MouseInputArgs
        {
            Button = ToCrossSharpMouseButton(castedE!.Data.Button),
            X = castedE.Data.X,
            Y = castedE.Data.Y,
            Clicks = castedE.Data.Clicks,
        };
        MousePressed?.Invoke(sender, args);
    }

    void OnMouseReleased(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseHookEventArgs;
        var args = new MouseInputArgs
        {
            Button = ToCrossSharpMouseButton(castedE!.Data.Button),
            X = castedE.Data.X,
            Y = castedE.Data.Y,
            Clicks = castedE.Data.Clicks,
        };
        MouseReleased?.Invoke(sender, args);
    }

    void OnMouseMoved(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseHookEventArgs;
        var args = new MouseInputArgs
        {
            Button = ToCrossSharpMouseButton(castedE!.Data.Button),
            X = castedE.Data.X,
            Y = castedE.Data.Y,
            Clicks = castedE.Data.Clicks,
        };
        MouseMoved?.Invoke(sender, args);
    }

    void OnMouseWheel(object? sender, HookEventArgs e)
    {
        MouseWheel?.Invoke(sender, null);
    }

    void OnMouseDragged(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseHookEventArgs;
        var args = new MouseInputArgs
        {
            Button = ToCrossSharpMouseButton(castedE!.Data.Button),
            X = castedE.Data.X,
            Y = castedE.Data.Y,
            Clicks = castedE.Data.Clicks,
        };
        MouseDragged?.Invoke(sender, args);
    }

    internal void StopListening()
    {
        if (!_hook.IsRunning)
            throw new InvalidOperationException("InputHandler hook is not running.");
        _hook.Stop();
    }

    static CrossSharp.Utils.Enums.MouseButton ToCrossSharpMouseButton(MouseButton button) =>
        button switch
        {
            SharpHook.Data.MouseButton.Button1 => CrossSharp.Utils.Enums.MouseButton.Left,
            SharpHook.Data.MouseButton.Button2 => CrossSharp.Utils.Enums.MouseButton.Right,
            _ => CrossSharp.Utils.Enums.MouseButton.None,
        };
}
