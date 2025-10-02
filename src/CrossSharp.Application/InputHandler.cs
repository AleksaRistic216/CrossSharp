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

    private DateTime lastClickTime = DateTime.MinValue;
    private (short X, short Y) lastClickPosition = (0, 0);
    private const int DoubleClickThresholdMs = 500;
    private const int PositionTolerance = 2;

    void OnMousePressed(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseHookEventArgs;
        if (castedE == null)
            return;

        var now = DateTime.Now;
        var timeDiff = (now - lastClickTime).TotalMilliseconds;
        var isSamePosition =
            Math.Abs(castedE.Data.X - lastClickPosition.X) <= PositionTolerance
            && Math.Abs(castedE.Data.Y - lastClickPosition.Y) <= PositionTolerance;

        int clickCount = (timeDiff <= DoubleClickThresholdMs && isSamePosition) ? 2 : 1;

        var args = new MouseInputArgs
        {
            Button = ToCrossSharpMouseButton(castedE.Data.Button),
            X = castedE.Data.X,
            Y = castedE.Data.Y,
            Clicks = clickCount,
        };

        MousePressed?.Invoke(sender, args);

        lastClickTime = now;
        lastClickPosition = (castedE.Data.X, castedE.Data.Y);
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
