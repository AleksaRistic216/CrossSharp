using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;
using SharpHook;

namespace CrossSharp.Application;

class InputHandler : IInputHandler
{
    readonly SimpleGlobalHook _hook = new();
    public event EventHandler<InputArgs>? KeyPressed;
    public event EventHandler<InputArgs>? MousePressed;
    public event EventHandler<InputArgs>? MouseMoved;
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
        MouseMoved?.Invoke(sender, null);
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
