using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Application;

static class CrossSharpApplicationRunner
{
    static IntPtr _renderer;

    internal static void Run<T>()
        where T : IForm
    {
        if (SDLHelpers.SDL_Init(SDLHelpers.SDL_INIT_VIDEO) != 0)
            throw new Exception("SDL_Init failed.");

        var application = Services.GetSingleton<IApplication>();
        application.MainFormType = typeof(T);
        application.Start();
        var form = application.MainForm;
        application.MainWindowHandle = form.Handle;
        _renderer = SDLHelpers.SDL_CreateRenderer(application.MainWindowHandle, -1, 0);
        while (
            Services.GetSingleton<IApplication>().MainForm.Handle != IntPtr.Zero // Need to use this instead of application.MainWindowHandle because it can be changed when the main form is replaced
        )
        {
            while (SDLHelpers.SDL_PollEvent(out SDL_Event e))
                HandleEvents(e);
            foreach (IForm f in Services.GetSingleton<IApplication>().Forms)
            {
                IGraphics g = new SDLGraphics(_renderer);
                f.Draw(ref g);
                g.Dispose();
                SDLHelpers.SDL_RenderPresent(_renderer);
            }
        }
        SDLHelpers.SDL_DestroyWindow(Services.GetSingleton<IApplication>().MainWindowHandle);
        SDLHelpers.SDL_Quit();
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
                        .First(x => x.WindowId == e.window.windowID);
                    form.Close();
                }
                break;
        }
    }
}
