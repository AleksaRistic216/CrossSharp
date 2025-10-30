using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Application;

static class CrossSharpApplicationRunner
{
    internal static void Run<T>()
        where T : IForm
    {
        if (SDLHelpers.SDL_Init(SDLHelpers.SDL_INIT_VIDEO) != 0)
            throw new Exception("SDL_Init failed.");

        var application = Services.GetSingleton<IApplication>();
        application.MainFormType = typeof(T);
        application.Start();
        var form = application.MainForm;
        form.Show();
        application.MainWindowHandle = form.Handle;
        while (
            Services.GetSingleton<IApplication>().MainForm.Handle != IntPtr.Zero // Need to use this instead of application.MainWindowHandle because it can be changed when the main form is replaced
        )
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
                f.Redraw();
            }
            Services.GetSingleton<IApplication>().Tick?.Invoke(null, EventArgs.Empty);
            MainThreadDispatcher.RunPending();
        }
        SDLHelpers.SDL_DestroyWindow(Services.GetSingleton<IApplication>().MainWindowHandle);
        SDLHelpers.SDL_Quit();
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
        switch (e.type)
        {
            case SDL_EventTypes.SDL_WINDOWEVENT:
                if (e.window.eventType == SDL_EventTypes.WindowEvents.SDL_WINDOWEVENT_CLOSE)
                {
                    var form = Services
                        .GetSingleton<IApplication>()
                        .Forms.OfType<IFormSDL>()
                        .FirstOrDefault(x => x.WindowId == e.window.windowID);
                    form?.Close();
                }
                break;
        }
    }
}
