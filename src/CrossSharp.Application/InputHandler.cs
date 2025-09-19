using CrossSharp.Utils;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using SharpHook;

namespace CrossSharp.Application;

class InputHandler : IInputHandler
{
    readonly SimpleGlobalHook _hook = new();
    public event EventHandler<InputArgs>? KeyPressed;
    public event EventHandler<InputArgs>? MousePressed;
    public event EventHandler<MouseMoveInputArgs>? MouseMoved;
    public event EventHandler<InputArgs>? MouseWheel;

    internal Task StartListeningAsync(CancellationToken token)
    {
        if (_hook.IsRunning)
            throw new InvalidOperationException("InputHandler hook is already running.");
        return Task.Run(
            () =>
            {
                _hook.KeyPressed += OnKeyPressed;
                _hook.MousePressed += OnMousePressed;
                _hook.MouseMoved += OnMouseMoved;
                _hook.MouseWheel += OnMouseWheel;
                _hook.RunAsync();
            },
            token
        );
    }

    void OnKeyPressed(object? sender, HookEventArgs e)
    {
        KeyPressed?.Invoke(sender, null);
    }

    void OnMousePressed(object? sender, HookEventArgs e)
    {
        MousePressed?.Invoke(sender, null);
    }

    void OnMouseMoved(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseHookEventArgs;
        var args = new MouseMoveInputArgs()
        {
            Button = castedE.Data.Button switch
            {
                SharpHook.Data.MouseButton.Button1 => Utils.Enums.MouseButton.Left,
                SharpHook.Data.MouseButton.Button2 => Utils.Enums.MouseButton.Right,
                SharpHook.Data.MouseButton.Button3 => throw new NotImplementedException(),
                SharpHook.Data.MouseButton.Button4 => throw new NotImplementedException(),
                SharpHook.Data.MouseButton.Button5 => throw new NotImplementedException(),
                _ => Utils.Enums.MouseButton.None,
            },
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

    internal void StopListening()
    {
        if (!_hook.IsRunning)
            throw new InvalidOperationException("InputHandler hook is not running.");
        _hook.Stop();
    }
}
