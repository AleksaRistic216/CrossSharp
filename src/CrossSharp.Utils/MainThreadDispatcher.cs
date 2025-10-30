using System.Collections.Concurrent;

namespace CrossSharp.Utils;

public static class MainThreadDispatcher
{
    private static readonly ConcurrentQueue<Action> _queue = new();
    private static int _mainThreadId;

    internal static void Initialize()
    {
        _mainThreadId = Environment.CurrentManagedThreadId;
    }

    internal static void RunPending()
    {
        while (_queue.TryDequeue(out var action))
            action();
    }

    public static void Invoke(Action action)
    {
        if (Environment.CurrentManagedThreadId == _mainThreadId)
        {
            action();
        }
        else
        {
            var done = new ManualResetEvent(false);
            _queue.Enqueue(() =>
            {
                action();
                done.Set();
            });
            done.WaitOne();
        }
    }

    public static T Invoke<T>(Func<T> func)
    {
        if (Environment.CurrentManagedThreadId == _mainThreadId)
            return func();

        T result = default!;
        var done = new ManualResetEvent(false);
        _queue.Enqueue(() =>
        {
            result = func();
            done.Set();
        });
        done.WaitOne();
        return result;
    }
}
