using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using SharpHook;
using SharpHook.Data;
using KeyCode = SharpHook.Data.KeyCode;

namespace CrossSharp.Application;

class InputHandler : IInputHandler
{
    readonly SimpleGlobalHook _hook = new();
    public event EventHandler<KeyInputArgs>? KeyPressed;
    public event EventHandler<MouseInputArgs>? MousePressed;
    public event EventHandler<MouseInputArgs>? MouseReleased;
    public event EventHandler<MouseInputArgs>? MouseMoved;
    public event EventHandler<MouseWheelInputArgs>? MouseWheel;
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
        if (e is not KeyboardHookEventArgs castedE)
            return;

        var keyCode = castedE.Data.KeyCode;
        var modifiers = castedE.RawEvent.Mask;

        var args = new KeyInputArgs
        {
            KeyCode = (CrossSharp.Utils.Input.KeyCode)castedE.Data.KeyCode,
            Char = ConvertKeyCodeToChar(keyCode, modifiers),
        };
        KeyPressed?.Invoke(sender, args);
    }

    char? ConvertKeyCodeToChar(KeyCode keyCode, EventMask modifiers)
    {
        if (OperatingSystem.IsLinux())
        {
            return GetCharFromKeyCodeLinux(keyCode, modifiers);
        }
        if (OperatingSystem.IsWindows())
        {
            throw new NotImplementedException();
        }
        if (OperatingSystem.IsMacOS())
        {
            throw new NotImplementedException();
        }
        throw new NotImplementedException();
    }

    char? GetCharFromKeyCodeLinux(KeyCode keyCode, EventMask modifiers)
    {
        bool shift = (modifiers & EventMask.Shift) != 0;

        return keyCode switch
        {
            KeyCode.VcA => shift ? 'A' : 'a',
            KeyCode.VcB => shift ? 'B' : 'b',
            KeyCode.VcC => shift ? 'C' : 'c',
            KeyCode.VcD => shift ? 'D' : 'd',
            KeyCode.VcE => shift ? 'E' : 'e',
            KeyCode.VcF => shift ? 'F' : 'f',
            KeyCode.VcG => shift ? 'G' : 'g',
            KeyCode.VcH => shift ? 'H' : 'h',
            KeyCode.VcI => shift ? 'I' : 'i',
            KeyCode.VcJ => shift ? 'J' : 'j',
            KeyCode.VcK => shift ? 'K' : 'k',
            KeyCode.VcL => shift ? 'L' : 'l',
            KeyCode.VcM => shift ? 'M' : 'm',
            KeyCode.VcN => shift ? 'N' : 'n',
            KeyCode.VcO => shift ? 'O' : 'o',
            KeyCode.VcP => shift ? 'P' : 'p',
            KeyCode.VcQ => shift ? 'Q' : 'q',
            KeyCode.VcR => shift ? 'R' : 'r',
            KeyCode.VcS => shift ? 'S' : 's',
            KeyCode.VcT => shift ? 'T' : 't',
            KeyCode.VcU => shift ? 'U' : 'u',
            KeyCode.VcV => shift ? 'V' : 'v',
            KeyCode.VcW => shift ? 'W' : 'w',
            KeyCode.VcX => shift ? 'X' : 'x',
            KeyCode.VcY => shift ? 'Y' : 'y',
            KeyCode.VcZ => shift ? 'Z' : 'z',
            KeyCode.Vc1 => shift ? '!' : '1',
            KeyCode.Vc2 => shift ? '@' : '2',
            KeyCode.Vc3 => shift ? '#' : '3',
            KeyCode.Vc4 => shift ? '$' : '4',
            KeyCode.Vc5 => shift ? '%' : '5',
            KeyCode.Vc6 => shift ? '^' : '6',
            KeyCode.Vc7 => shift ? '&' : '7',
            KeyCode.Vc8 => shift ? '*' : '8',
            KeyCode.Vc9 => shift ? '(' : '9',
            KeyCode.Vc0 => shift ? ')' : '0',
            KeyCode.VcSpace => ' ',
            KeyCode.VcMinus => shift ? '_' : '-',
            KeyCode.VcEquals => shift ? '+' : '=',
            KeyCode.VcBackslash => shift ? '|' : '\\',
            KeyCode.VcSemicolon => shift ? ':' : ';',
            KeyCode.VcQuote => shift ? '"' : '\'',
            KeyCode.VcComma => shift ? '<' : ',',
            KeyCode.VcPeriod => shift ? '>' : '.',
            KeyCode.VcSlash => shift ? '?' : '/',
            _ => null,
        };
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
        var castedE = e as MouseWheelHookEventArgs;
        var args = new MouseWheelInputArgs() { Rotation = castedE!.Data.Rotation };
        MouseWheel?.Invoke(sender, args);
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
