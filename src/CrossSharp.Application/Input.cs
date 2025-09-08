using SharpHook;
namespace CrossSharp.Application;

internal static class Input {
    static SimpleGlobalHook _hook = new ();
    internal static event EventHandler<HookEventArgs>? KeyPressed;
    internal static event EventHandler<HookEventArgs>? MousePressed;
    internal static event EventHandler<HookEventArgs>? MouseMoved;
    internal static event EventHandler<HookEventArgs>? MouseWheel;
    internal static Task StartAsync(CancellationToken token) {
        if(_hook.IsRunning)
            throw new InvalidOperationException("Input hook is already running.");
        return Task.Run(() => {
            _hook.KeyPressed += OnKeyPressed;
            _hook.MousePressed += OnMousePressed;
            _hook.MouseMoved += OnMouseMoved;
            _hook.MouseWheel += OnMouseWheel;
            _hook.RunAsync();
        }, token);
    }
    static void OnKeyPressed(object? sender, HookEventArgs e) {
        KeyPressed?.Invoke(sender, e);
    }
    static void OnMousePressed(object? sender, HookEventArgs e) {
        MousePressed?.Invoke(sender, e);
    }
    static void OnMouseMoved(object? sender, HookEventArgs e) {
        MouseMoved?.Invoke(sender, e);
    }
    static void OnMouseWheel(object? sender, HookEventArgs e) {
        MouseWheel?.Invoke(sender, e);
    }
}