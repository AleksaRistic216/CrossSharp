using CrossSharp.Utils;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using SharpHook;
using SharpHook.Data;
using KeyCode = SharpHook.Data.KeyCode;

namespace CrossSharp.Application;

class InputHandler : IInputHandler
{
    static bool DISABLED = false;
    readonly SimpleGlobalHook _hook = new();
    public event EventHandler<KeyInputArgs>? KeyPressed;
    public event EventHandler<MouseInputArgs>? MousePressed;
    public event EventHandler<MouseInputArgs>? MouseReleased;
    public event EventHandler<MouseInputArgs>? MouseMoved;
    public event EventHandler<MouseWheelInputArgs>? MouseWheel;
    public event EventHandler<MouseInputArgs>? MouseDragged;

    public void StartListeningAsync(CancellationToken token)
    {
        if (DISABLED)
        {
            Debug.LogWarning("InputHandler is disabled");
            return;
        }
        if (_hook.IsRunning)
        {
            Debug.LogError("InputHandler hook is already running");
            throw new InvalidOperationException("InputHandler hook is already running.");
        }
        Debug.Log(LogCategory.Input, "Starting input handler");
        var thread = new Thread(() =>
        {
            _hook.KeyPressed += OnKeyPressed;
            _hook.MousePressed += OnMousePressed;
            _hook.MouseReleased += OnMouseReleased;
            _hook.MouseMoved += OnMouseMoved;
            _hook.MouseWheel += OnMouseWheel;
            _hook.MouseDragged += OnMouseDragged;
            _hook.RunAsync();
            Debug.Log(LogCategory.Input, "Input hook started successfully");
        });
        thread.Start();
    }

    void OnKeyPressed(object? sender, HookEventArgs e)
    {
        if (e is not KeyboardHookEventArgs castedE)
            return;

        var keyCode = castedE.Data.KeyCode;
        var modifiers = castedE.RawEvent.Mask;

        var args = new KeyInputArgs
        {
            KeyCode = (CrossSharp.Utils.Input.KeyCode)castedE.Data.KeyCode,
            Char = ConvertKeyCodeToChar(keyCode, modifiers),
            IsShiftPressed = (modifiers & EventMask.Shift) != 0,
            IsCtrlPressed = (modifiers & EventMask.Ctrl) != 0,
            IsAltPressed = (modifiers & EventMask.Alt) != 0,
        };
        MainThreadDispatcher.Invoke(() =>
        {
            KeyPressed?.Invoke(sender, args);
        });
    }

    char? ConvertKeyCodeToChar(KeyCode keyCode, EventMask modifiers)
    {
        bool shift = (modifiers & EventMask.Shift) != 0;
        var crossSharpKeyCode = (CrossSharp.Utils.Input.KeyCode)keyCode;
        return KeyCodeConverter.ToChar(crossSharpKeyCode, shift);
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

        MainThreadDispatcher.Invoke(() =>
        {
            MousePressed?.Invoke(sender, args);
        });

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

        MainThreadDispatcher.Invoke(() =>
        {
            MouseReleased?.Invoke(sender, args);
        });
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
        MainThreadDispatcher.Invoke(() =>
        {
            MouseMoved?.Invoke(sender, args);
        });
    }

    void OnMouseWheel(object? sender, HookEventArgs e)
    {
        var castedE = e as MouseWheelHookEventArgs;
        var args = new MouseWheelInputArgs() { Rotation = castedE!.Data.Rotation };
        MainThreadDispatcher.Invoke(() =>
        {
            MouseWheel?.Invoke(sender, args);
        });
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
        MainThreadDispatcher.Invoke(() =>
        {
            MouseDragged?.Invoke(sender, args);
        });
    }

    internal void StopListening()
    {
        if (!_hook.IsRunning)
        {
            Debug.LogWarning("InputHandler hook is not running, cannot stop");
            return;
        }
        Debug.Log(LogCategory.Input, "Stopping input handler");
        _hook.Stop();
        Debug.Log(LogCategory.Input, "Input handler stopped");
    }

    static CrossSharp.Utils.Enums.MouseButton ToCrossSharpMouseButton(MouseButton button) =>
        button switch
        {
            SharpHook.Data.MouseButton.Button1 => CrossSharp.Utils.Enums.MouseButton.Left,
            SharpHook.Data.MouseButton.Button2 => CrossSharp.Utils.Enums.MouseButton.Right,
            _ => CrossSharp.Utils.Enums.MouseButton.None,
        };
}
