namespace CrossSharp.Utils.SDL;

static class SDL_EventTypes
{
    // Application events
    internal const uint SDL_EVENT_QUIT = 0x100;
    internal const uint SDL_EVENT_TERMINATING = 0x101;
    internal const uint SDL_EVENT_LOW_MEMORY = 0x102;
    internal const uint SDL_EVENT_WILL_ENTER_BACKGROUND = 0x103;
    internal const uint SDL_EVENT_DID_ENTER_BACKGROUND = 0x104;
    internal const uint SDL_EVENT_WILL_ENTER_FOREGROUND = 0x105;
    internal const uint SDL_EVENT_DID_ENTER_FOREGROUND = 0x106;

    // Window events (each window event is now its own event type in SDL3)
    internal const uint SDL_EVENT_WINDOW_SHOWN = 0x202;
    internal const uint SDL_EVENT_WINDOW_HIDDEN = 0x203;
    internal const uint SDL_EVENT_WINDOW_EXPOSED = 0x204;
    internal const uint SDL_EVENT_WINDOW_MOVED = 0x205;
    internal const uint SDL_EVENT_WINDOW_RESIZED = 0x206;
    internal const uint SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED = 0x207;
    internal const uint SDL_EVENT_WINDOW_METAL_VIEW_RESIZED = 0x208;
    internal const uint SDL_EVENT_WINDOW_MINIMIZED = 0x209;
    internal const uint SDL_EVENT_WINDOW_MAXIMIZED = 0x20A;
    internal const uint SDL_EVENT_WINDOW_RESTORED = 0x20B;
    internal const uint SDL_EVENT_WINDOW_MOUSE_ENTER = 0x20C;
    internal const uint SDL_EVENT_WINDOW_MOUSE_LEAVE = 0x20D;
    internal const uint SDL_EVENT_WINDOW_FOCUS_GAINED = 0x20E;
    internal const uint SDL_EVENT_WINDOW_FOCUS_LOST = 0x20F;
    internal const uint SDL_EVENT_WINDOW_CLOSE_REQUESTED = 0x210;
    internal const uint SDL_EVENT_WINDOW_HIT_TEST = 0x211;
    internal const uint SDL_EVENT_WINDOW_ICCPROF_CHANGED = 0x212;
    internal const uint SDL_EVENT_WINDOW_DISPLAY_CHANGED = 0x213;
    internal const uint SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED = 0x214;
    internal const uint SDL_EVENT_WINDOW_SAFE_AREA_CHANGED = 0x215;
    internal const uint SDL_EVENT_WINDOW_OCCLUDED = 0x216;
    internal const uint SDL_EVENT_WINDOW_ENTER_FULLSCREEN = 0x217;
    internal const uint SDL_EVENT_WINDOW_LEAVE_FULLSCREEN = 0x218;
    internal const uint SDL_EVENT_WINDOW_DESTROYED = 0x219;
    internal const uint SDL_EVENT_WINDOW_HDR_STATE_CHANGED = 0x21A;

    // Keyboard events
    internal const uint SDL_EVENT_KEY_DOWN = 0x300;
    internal const uint SDL_EVENT_KEY_UP = 0x301;
    internal const uint SDL_EVENT_TEXT_EDITING = 0x302;
    internal const uint SDL_EVENT_TEXT_INPUT = 0x303;
    internal const uint SDL_EVENT_KEYMAP_CHANGED = 0x304;

    // Mouse events
    internal const uint SDL_EVENT_MOUSE_MOTION = 0x400;
    internal const uint SDL_EVENT_MOUSE_BUTTON_DOWN = 0x401;
    internal const uint SDL_EVENT_MOUSE_BUTTON_UP = 0x402;
    internal const uint SDL_EVENT_MOUSE_WHEEL = 0x403;

    // Joystick events
    internal const uint SDL_EVENT_JOYSTICK_AXIS_MOTION = 0x600;
    internal const uint SDL_EVENT_JOYSTICK_BALL_MOTION = 0x601;
    internal const uint SDL_EVENT_JOYSTICK_HAT_MOTION = 0x602;
    internal const uint SDL_EVENT_JOYSTICK_BUTTON_DOWN = 0x603;
    internal const uint SDL_EVENT_JOYSTICK_BUTTON_UP = 0x604;
    internal const uint SDL_EVENT_JOYSTICK_ADDED = 0x605;
    internal const uint SDL_EVENT_JOYSTICK_REMOVED = 0x606;

    // Gamepad events
    internal const uint SDL_EVENT_GAMEPAD_AXIS_MOTION = 0x650;
    internal const uint SDL_EVENT_GAMEPAD_BUTTON_DOWN = 0x651;
    internal const uint SDL_EVENT_GAMEPAD_BUTTON_UP = 0x652;
    internal const uint SDL_EVENT_GAMEPAD_ADDED = 0x653;
    internal const uint SDL_EVENT_GAMEPAD_REMOVED = 0x654;
    internal const uint SDL_EVENT_GAMEPAD_REMAPPED = 0x655;

    // Touch events
    internal const uint SDL_EVENT_FINGER_DOWN = 0x700;
    internal const uint SDL_EVENT_FINGER_UP = 0x701;
    internal const uint SDL_EVENT_FINGER_MOTION = 0x702;

    // Clipboard events
    internal const uint SDL_EVENT_CLIPBOARD_UPDATE = 0x900;

    // Drag and drop events
    internal const uint SDL_EVENT_DROP_FILE = 0x1000;
    internal const uint SDL_EVENT_DROP_TEXT = 0x1001;
    internal const uint SDL_EVENT_DROP_BEGIN = 0x1002;
    internal const uint SDL_EVENT_DROP_COMPLETE = 0x1003;

    // Audio device events
    internal const uint SDL_EVENT_AUDIO_DEVICE_ADDED = 0x1100;
    internal const uint SDL_EVENT_AUDIO_DEVICE_REMOVED = 0x1101;

    // Sensor events
    internal const uint SDL_EVENT_SENSOR_UPDATE = 0x1200;

    // Render events
    internal const uint SDL_EVENT_RENDER_TARGETS_RESET = 0x2000;
    internal const uint SDL_EVENT_RENDER_DEVICE_RESET = 0x2001;
    internal const uint SDL_EVENT_RENDER_DEVICE_LOST = 0x2002;
}
