using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Application;

static class CrossSharpApplicationRunner
{
    internal static void Run<T>()
        where T : IForm
    {
        Debug.Log(LogCategory.SDL, "Initializing SDL3 with Video subsystem");
        if (!SDLHelpers.SDL_Init(SDLInitFlags.Video))
        {
            Debug.LogError("SDL_Init failed");
            throw new Exception("SDL_Init failed.");
        }
        Debug.Log(LogCategory.SDL, "SDL3 initialized successfully");

        var application = Services.GetSingleton<IApplication>();
        application.MainFormType = typeof(T);
        Debug.Log(LogCategory.App, $"Creating main form: {typeof(T).Name}");
        application.Start();
        application.MainWindowHandle = application.MainForm.Handle;
        Debug.Log(LogCategory.App, $"Main form created, handle: 0x{application.MainWindowHandle:X}");
        Debug.Log(LogCategory.App, "Entering main loop");
        while (
            Services.GetSingleton<IApplication>().MainForm.Handle != IntPtr.Zero // Need to use this instead of application.MainWindowHandle because it can be changed when the main form is replaced
        )
        {
            try
            {
                WaitForTargetFps();
                Diagnostics.Ui.FrameCount++;
                while (SDLHelpers.SDL_PollEvent(out SDL_Event e))
                    HandleEvents(e);
                foreach (var form1 in Services.GetSingleton<IApplication>().Forms.ToArray())
                {
                    if (form1 is not IFormSDL f)
                        continue;
                    f.RecordLocation();
                    f.RecordSize();
                    f.RecordState();
                    f.Redraw();
                }
                Services.GetSingleton<IApplication>().Tick?.Invoke(null, EventArgs.Empty);
                MainThreadDispatcher.RunPending();
            }
            catch (Exception ex)
            {
                Debug.LogError("Exception in main loop", ex);
            }
        }
        Debug.Log(LogCategory.App, "Main loop exited");
        Debug.Log(LogCategory.SDL, "Destroying main window");
        SDLHelpers.SDL_DestroyWindow(Services.GetSingleton<IApplication>().MainWindowHandle);
        Debug.Log(LogCategory.SDL, "Shutting down SDL");
        SDLHelpers.SDL_Quit();
        Debug.Log(LogCategory.App, "Application shutdown complete");
    }

    static DateTime _lastFrameTime = DateTime.UtcNow;

    static void WaitForTargetFps()
    {
        var fpsToRun = Services.GetSingleton<IApplicationConfiguration>().CoreFps;
        var targetFrameDuration = TimeSpan.FromSeconds(1.0 / fpsToRun);
        var now = DateTime.UtcNow;
        var timeSinceLastFrame = now - _lastFrameTime;
        if (timeSinceLastFrame < targetFrameDuration)
        {
            var timeToWait = targetFrameDuration - timeSinceLastFrame;
            if (timeToWait.TotalMilliseconds > 1)
                SDLHelpers.SDL_Delay((uint)timeToWait.TotalMilliseconds);
            while (DateTime.UtcNow - _lastFrameTime < targetFrameDuration) { }
        }
        _lastFrameTime = DateTime.UtcNow;
    }

    static void HandleEvents(SDL_Event e)
    {
        // SDL3: window events are now individual event types, not sub-types
        switch (e.type)
        {
            case SDL_EventTypes.SDL_EVENT_WINDOW_CLOSE_REQUESTED:
                var form = Services
                    .GetSingleton<IApplication>()
                    .Forms.OfType<IFormSDL>()
                    .FirstOrDefault(x => x.WindowId == e.window.windowID);
                form?.Close();
                break;
        }
    }
}
