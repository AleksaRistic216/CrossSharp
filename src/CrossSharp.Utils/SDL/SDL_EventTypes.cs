namespace CrossSharp.Utils.SDL;

static class SDL_EventTypes
{
    internal const uint SDL_QUIT = 0x100;
    internal const uint SDL_APP_TERMINATING = 0x101;
    internal const uint SDL_APP_LOWMEMORY = 0x102;
    internal const uint SDL_APP_WILLENTERBACKGROUND = 0x103;
    internal const uint SDL_APP_DIDENTERBACKGROUND = 0x104;
    internal const uint SDL_APP_WILLENTERFOREGROUND = 0x105;
    internal const uint SDL_APP_DIDENTERFOREGROUND = 0x106;
    internal const uint SDL_WINDOWEVENT = 0x200;
    internal const uint SDL_SYSWMEVENT = 0x201;
    internal const uint SDL_KEYDOWN = 0x300;
    internal const uint SDL_KEYUP = 0x301;
    internal const uint SDL_TEXTEDITING = 0x302;
    internal const uint SDL_TEXTINPUT = 0x303;
    internal const uint SDL_KEYMAPCHANGED = 0x304;
    internal const uint SDL_MOUSEMOTION = 0x400;
    internal const uint SDL_MOUSEBUTTONDOWN = 0x401;
    internal const uint SDL_MOUSEBUTTONUP = 0x402;
    internal const uint SDL_MOUSEWHEEL = 0x403;
    internal const uint SDL_JOYAXISMOTION = 0x600;
    internal const uint SDL_JOYBALLMOTION = 0x601;
    internal const uint SDL_JOYHATMOTION = 0x602;
    internal const uint SDL_JOYBUTTONDOWN = 0x603;
    internal const uint SDL_JOYBUTTONUP = 0x604;
    internal const uint SDL_JOYDEVICEADDED = 0x605;
    internal const uint SDL_JOYDEVICEREMOVED = 0x606;
    internal const uint SDL_CONTROLLERAXISMOTION = 0x650;
    internal const uint SDL_CONTROLLERBUTTONDOWN = 0x651;
    internal const uint SDL_CONTROLLERBUTTONUP = 0x652;
    internal const uint SDL_CONTROLLERDEVICEADDED = 0x653;
    internal const uint SDL_CONTROLLERDEVICEREMOVED = 0x654;
    internal const uint SDL_CONTROLLERDEVICEREMAPPED = 0x655;
    internal const uint SDL_CONTROLLERTOUCHPADDOWN = 0x656;
    internal const uint SDL_CONTROLLERTOUCHPADMOTION = 0x657;
    internal const uint SDL_CONTROLLERTOUCHPADUP = 0x658;
    internal const uint SDL_CONTROLLERSTEAM = 0x659;
    internal const uint SDL_FINGERDOWN = 0x700;
    internal const uint SDL_FINGERUP = 0x701;
    internal const uint SDL_FINGERMOTION = 0x702;
    internal const uint SDL_DOLLARGESTURE = 0x800;
    internal const uint SDL_DOLLARRECORD = 0x801;
    internal const uint SDL_MULTIGESTURE = 0x802;
    internal const uint SDL_CLIPBOARDUPDATE = 0x900;
    internal const uint SDL_DROPFILE = 0x1000;
    internal const uint SDL_DROPTEXT = 0x1001;
    internal const uint SDL_DROPBEGIN = 0x1002;
    internal const uint SDL_DROPCOMPLETE = 0x1003;
    internal const uint SDL_AUDIODEVICEADDED = 0x1100;
    internal const uint SDL_AUDIODEVICEREMOVED = 0x1101;
    internal const uint SDL_SENSORUPDATE = 0x1200;
    internal const uint SDL_RENDER_TARGETS_RESET = 0x2000;
    internal const uint SDL_RENDER_DEVICE_RESET = 0x2001;

    internal class WindowEvents
    {
        internal const byte SDL_WINDOWEVENT_NONE = 0x00;
        internal const byte SDL_WINDOWEVENT_SHOWN = 0x01;
        internal const byte SDL_WINDOWEVENT_HIDDEN = 0x02;
        internal const byte SDL_WINDOWEVENT_EXPOSED = 0x03;
        internal const byte SDL_WINDOWEVENT_MOVED = 0x04;
        internal const byte SDL_WINDOWEVENT_RESIZED = 0x05;
        internal const byte SDL_WINDOWEVENT_SIZE_CHANGED = 0x06;
        internal const byte SDL_WINDOWEVENT_MINIMIZED = 0x07;
        internal const byte SDL_WINDOWEVENT_MAXIMIZED = 0x08;
        internal const byte SDL_WINDOWEVENT_RESTORED = 0x09;
        internal const byte SDL_WINDOWEVENT_ENTER = 0x0A;
        internal const byte SDL_WINDOWEVENT_LEAVE = 0x0B;
        internal const byte SDL_WINDOWEVENT_FOCUS_GAINED = 0x0C;
        internal const byte SDL_WINDOWEVENT_FOCUS_LOST = 0x0D;
        internal const byte SDL_WINDOWEVENT_CLOSE = 0x0E;
        internal const byte SDL_WINDOWEVENT_TAKE_FOCUS = 0x10;
        internal const byte SDL_WINDOWEVENT_HIT_TEST = 0x11;
    }
}
