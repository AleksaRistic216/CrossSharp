using System.Collections.Concurrent;

namespace CrossSharp.Utils;

public static class MainThreadDispatcher
{
    static readonly ConcurrentQueue<Action> _queue = new();
    static int _mainThreadId;

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
            _queue.Enqueue(action);
        }
    }

    public static T Invoke<T>(Func<T> func)
    {
        if (Environment.CurrentManagedThreadId == _mainThreadId)
            return func();

        T result = default(T)!;
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
