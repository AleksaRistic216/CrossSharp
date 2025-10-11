namespace CrossSharp.Utils.Input;

public enum KeyCode : ushort
{
    /// <summary>Undefined key</summary>
    VcUndefined = 0,

    /// <summary>Backspace (on Windows and Linux) or Delete (on macOS)</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcBackspace = 8,

    /// <summary>Tab</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcTab = 9,

    /// <summary>Enter</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcEnter = 10, // 0x000A

    /// <summary>Num-Pad Clear</summary>
    /// <remarks>Available on: Windows, macOS</remarks>
    VcNumPadClear = 12, // 0x000C

    /// <summary>Pause</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcPause = 19, // 0x0013

    /// <summary>Caps Lock</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcCapsLock = 20, // 0x0014

    /// <summary>IME Kana mode</summary>
    /// <remarks>Available on: Windows, macOS</remarks>
    VcKana = 21, // 0x0015

    /// <summary>IME Kanji mode</summary>
    /// <remarks>Available on: Windows</remarks>
    VcKanji = 25, // 0x0019

    /// <summary>Escape</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcEscape = 27, // 0x001B

    /// <summary>IME Convert (henkan)</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcConvert = 28, // 0x001C

    /// <summary>IME Non-Convert (muhenkan)</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcNonConvert = 29, // 0x001D

    /// <summary>IME Accept</summary>
    /// <remarks>Available on: Windows</remarks>
    VcAccept = 30, // 0x001E

    /// <summary>Space</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcSpace = 32, // 0x0020

    /// <summary>Page Up</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcPageUp = 33, // 0x0021

    /// <summary>Page Down</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcPageDown = 34, // 0x0022

    /// <summary>End</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcEnd = 35, // 0x0023

    /// <summary>Home</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcHome = 36, // 0x0024

    /// <summary>Left Arrow</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcLeft = 37, // 0x0025

    /// <summary>Up Arrow</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcUp = 38, // 0x0026

    /// <summary>Right Arrow</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcRight = 39, // 0x0027

    /// <summary>Down Arrow</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcDown = 40, // 0x0028

    /// <summary>,</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcComma = 44, // 0x002C

    /// <summary>-</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcMinus = 45, // 0x002D

    /// <summary>.</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcPeriod = 46, // 0x002E

    /// <summary>/</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcSlash = 47, // 0x002F

    /// <summary>0</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc0 = 48, // 0x0030

    /// <summary>1</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc1 = 49, // 0x0031

    /// <summary>2</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc2 = 50, // 0x0032

    /// <summary>3</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc3 = 51, // 0x0033

    /// <summary>4</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc4 = 52, // 0x0034

    /// <summary>5</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc5 = 53, // 0x0035

    /// <summary>6</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc6 = 54, // 0x0036

    /// <summary>7</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc7 = 55, // 0x0037

    /// <summary>8</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc8 = 56, // 0x0038

    /// <summary>9</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc9 = 57, // 0x0039

    /// <summary>;</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcSemicolon = 59, // 0x003B

    /// <summary>=</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcEquals = 61, // 0x003D

    /// <summary>A</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcA = 65, // 0x0041

    /// <summary>B</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcB = 66, // 0x0042

    /// <summary>C</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcC = 67, // 0x0043

    /// <summary>D</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcD = 68, // 0x0044

    /// <summary>E</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcE = 69, // 0x0045

    /// <summary>F</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF = 70, // 0x0046

    /// <summary>G</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcG = 71, // 0x0047

    /// <summary>H</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcH = 72, // 0x0048

    /// <summary>I</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcI = 73, // 0x0049

    /// <summary>J</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcJ = 74, // 0x004A

    /// <summary>K</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcK = 75, // 0x004B

    /// <summary>L</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcL = 76, // 0x004C

    /// <summary>M</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcM = 77, // 0x004D

    /// <summary>N</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcN = 78, // 0x004E

    /// <summary>O</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcO = 79, // 0x004F

    /// <summary>P</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcP = 80, // 0x0050

    /// <summary>Q</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcQ = 81, // 0x0051

    /// <summary>R</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcR = 82, // 0x0052

    /// <summary>S</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcS = 83, // 0x0053

    /// <summary>T</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcT = 84, // 0x0054

    /// <summary>U</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcU = 85, // 0x0055

    /// <summary>V</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcV = 86, // 0x0056

    /// <summary>W</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcW = 87, // 0x0057

    /// <summary>X</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcX = 88, // 0x0058

    /// <summary>Y</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcY = 89, // 0x0059

    /// <summary>Z</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcZ = 90, // 0x005A

    /// <summary>[</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcOpenBracket = 91, // 0x005B

    /// <summary>]</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcCloseBracket = 92, // 0x005C

    /// <summary>\</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcBackslash = 93, // 0x005D

    /// <summary>Num-Pad 0</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad0 = 96, // 0x0060

    /// <summary>Num-Pad 1</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad1 = 97, // 0x0061

    /// <summary>Num-Pad 2</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad2 = 98, // 0x0062

    /// <summary>Num-Pad 3</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad3 = 99, // 0x0063

    /// <summary>Num-Pad 4</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad4 = 100, // 0x0064

    /// <summary>Num-Pad 5</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad5 = 101, // 0x0065

    /// <summary>Num-Pad 6</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad6 = 102, // 0x0066

    /// <summary>Num-Pad 7</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad7 = 103, // 0x0067

    /// <summary>Num-Pad 8</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad8 = 104, // 0x0068

    /// <summary>Num-Pad 9</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPad9 = 105, // 0x0069

    /// <summary>Num-Pad *</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadMultiply = 106, // 0x006A

    /// <summary>Num-Pad +</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadAdd = 107, // 0x006B

    /// <summary>Num-Pad Separator</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcNumPadSeparator = 108, // 0x006C

    /// <summary>Num-Pad -</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadSubtract = 109, // 0x006D

    /// <summary>Num-Pad Decimal</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadDecimal = 110, // 0x006E

    /// <summary>Num-Pad /</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadDivide = 111, // 0x006F

    /// <summary>F1</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF1 = 112, // 0x0070

    /// <summary>F2</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF2 = 113, // 0x0071

    /// <summary>F3</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF3 = 114, // 0x0072

    /// <summary>F4</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF4 = 115, // 0x0073

    /// <summary>F5</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF5 = 116, // 0x0074

    /// <summary>F6</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF6 = 117, // 0x0075

    /// <summary>F7</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF7 = 118, // 0x0076

    /// <summary>F8</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF8 = 119, // 0x0077

    /// <summary>F9</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF9 = 120, // 0x0078

    /// <summary>F10</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF10 = 121, // 0x0079

    /// <summary>F11</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF11 = 122, // 0x007A

    /// <summary>F12</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF12 = 123, // 0x007B

    /// <summary>Num-Pad =</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadEquals = 124, // 0x007C

    /// <summary>Num-Pad Enter</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcNumPadEnter = 125, // 0x007D

    /// <summary>
    /// Delete (on Windows and Linux) or Forward Delete (on macOS)
    /// </summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcDelete = 127, // 0x007F

    /// <summary>Num Lock</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcNumLock = 144, // 0x0090

    /// <summary>Scroll Lock</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcScrollLock = 145, // 0x0091

    /// <summary>
    /// The &lt;&gt; key on the US standard keyboard, or the \| key on the non-US 102-key keyboard,
    /// or the Section key (§) on the macOS ISO keyboard
    /// </summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    Vc102 = 153, // 0x0099

    /// <summary>Print Screen</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcPrintScreen = 154, // 0x009A

    /// <summary>Insert</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcInsert = 155, // 0x009B

    /// <summary>Help</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcHelp = 159, // 0x009F

    /// <summary>`</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcBackQuote = 192, // 0x00C0

    /// <summary>Cancel</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcCancel = 211, // 0x00D3

    /// <summary>'</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcQuote = 222, // 0x00DE

    /// <summary>IME Hanja mode</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcHanja = 230, // 0x00E6

    /// <summary>IME Final mode</summary>
    /// <remarks>Available on: Windows</remarks>
    VcFinal = 231, // 0x00E7

    /// <summary>IME Junja mode</summary>
    /// <remarks>Available on: Windows</remarks>
    VcJunja = 232, // 0x00E8

    /// <summary>IME Hangul mode</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcHangul = 233, // 0x00E9

    /// <summary>IME Alphanumeric mode (eisū)</summary>
    /// <remarks>Available on: macOS</remarks>
    VcAlphanumeric = 240, // 0x00F0

    /// <summary>IME Katakana mode</summary>
    /// <remarks>Available on: Linux</remarks>
    VcKatakana = 241, // 0x00F1

    /// <summary>IME Hiragana mode</summary>
    /// <remarks>Available on: Linux</remarks>
    VcHiragana = 242, // 0x00F2

    /// <summary>IME Process</summary>
    /// <remarks>Available on: Windows</remarks>
    VcProcess = 261, // 0x0105

    /// <summary>IME Katakana/Hiragana toggle</summary>
    /// <remarks>Available on: Linux</remarks>
    VcKatakanaHiragana = 262, // 0x0106

    /// <summary>IME Mode Change</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcModeChange = 263, // 0x0107

    /// <summary>IME Off</summary>
    /// <remarks>Available on: Windows</remarks>
    VcImeOff = 264, // 0x0108

    /// <summary>IME On</summary>
    /// <remarks>Available on: Windows</remarks>
    VcImeOn = 265, // 0x0109

    /// <summary>_</summary>
    /// <remarks>Available on: macOS, Linux</remarks>
    VcUnderscore = 523, // 0x020B

    /// <summary>Yen</summary>
    /// <remarks>Available on: macOS, Linux</remarks>
    VcYen = 524, // 0x020C

    /// <summary>Context Menu</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcContextMenu = 525, // 0x020D

    /// <summary>Function</summary>
    /// <remarks>Available on: macOS</remarks>
    VcFunction = 526, // 0x020E

    /// <summary>
    /// Function key when used to change an input source on macOS
    /// </summary>
    /// <remarks>Available on: macOS</remarks>
    VcChangeInputSource = 527, // 0x020F

    /// <summary>JP Comma</summary>
    /// <remarks>Available on: macOS, Linux</remarks>
    VcJpComma = 528, // 0x0210

    /// <summary>Miscellaneous OEM-specific key</summary>
    /// <remarks>Available on: Windows</remarks>
    VcMisc = 3585, // 0x0E01

    /// <summary>Left Shift</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcLeftShift = 40976, // 0xA010

    /// <summary>Left Control</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcLeftControl = 40977, // 0xA011

    /// <summary>Left Alt (on Windows and Linux) or Option (on macOS)</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcLeftAlt = 40978, // 0xA012

    /// <summary>
    /// Left Win (on Windows), Command (on macOS), or Super/Meta (on Linux)
    /// </summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcLeftMeta = 41117, // 0xA09D

    /// <summary>Right Shift</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcRightShift = 45072, // 0xB010

    /// <summary>Right Control</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcRightControl = 45073, // 0xB011

    /// <summary>Right Alt (on Windows and Linux) or Option (on macOS)</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcRightAlt = 45074, // 0xB012

    /// <summary>
    /// Right Win (on Windows), Command (on macOS), or Super/Meta (on Linux)
    /// </summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcRightMeta = 45213, // 0xB09D

    /// <summary>Previous Media</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcMediaPrevious = 57360, // 0xE010

    /// <summary>Next Media</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcMediaNext = 57369, // 0xE019

    /// <summary>Volume Mute</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcVolumeMute = 57376, // 0xE020

    /// <summary>Launch calculator</summary>
    /// <remarks>Available on: Linux</remarks>
    VcAppCalculator = 57377, // 0xE021

    /// <summary>Play/Pause Media</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcMediaPlay = 57378, // 0xE022

    /// <summary>Stop Media</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcMediaStop = 57380, // 0xE024

    /// <summary>Launch browser</summary>
    /// <remarks>Available on: Linux</remarks>
    VcAppBrowser = 57381, // 0xE025

    /// <summary>Launch app 1</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcApp1 = 57382, // 0xE026

    /// <summary>Launch app 2</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcApp2 = 57383, // 0xE027

    /// <summary>Launch app 3</summary>
    /// <remarks>Available on: Linux</remarks>
    VcApp3 = 57384, // 0xE028

    /// <summary>Launch app 4</summary>
    /// <remarks>Available on: Linux</remarks>
    VcApp4 = 57385, // 0xE029

    /// <summary>Eject Media</summary>
    /// <remarks>Available on: macOS, Linux</remarks>
    VcMediaEject = 57388, // 0xE02C

    /// <summary>Volume Up</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcVolumeUp = 57390, // 0xE02E

    /// <summary>Volume Down</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcVolumeDown = 57392, // 0xE030

    /// <summary>Browser Home</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcBrowserHome = 57394, // 0xE032

    /// <summary>Power</summary>
    /// <remarks>Available on: macOS, Linux</remarks>
    VcPower = 57438, // 0xE05E

    /// <summary>Sleep</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcSleep = 57439, // 0xE05F

    /// <summary>Browser Search</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcBrowserSearch = 57445, // 0xE065

    /// <summary>Browser Favorites</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcBrowserFavorites = 57446, // 0xE066

    /// <summary>Browser Refresh</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcBrowserRefresh = 57447, // 0xE067

    /// <summary>Browser Stop</summary>
    /// <remarks>Available on: Windows</remarks>
    VcBrowserStop = 57448, // 0xE068

    /// <summary>Browser Forward</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcBrowserForward = 57449, // 0xE069

    /// <summary>Browser Back</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcBrowserBack = 57450, // 0xE06A

    /// <summary>Launch mail</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcAppMail = 57452, // 0xE06C

    /// <summary>Select Media</summary>
    /// <remarks>Available on: Windows</remarks>
    VcMediaSelect = 57453, // 0xE06D

    /// <summary>F13</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF13 = 61440, // 0xF000

    /// <summary>F14</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF14 = 61441, // 0xF001

    /// <summary>F15</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF15 = 61442, // 0xF002

    /// <summary>F16</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF16 = 61443, // 0xF003

    /// <summary>F17</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF17 = 61444, // 0xF004

    /// <summary>F18</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF18 = 61445, // 0xF005

    /// <summary>F19</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF19 = 61446, // 0xF006

    /// <summary>F20</summary>
    /// <remarks>Available on: Windows, macOS, Linux</remarks>
    VcF20 = 61447, // 0xF007

    /// <summary>F21</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcF21 = 61448, // 0xF008

    /// <summary>F22</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcF22 = 61449, // 0xF009

    /// <summary>F23</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcF23 = 61450, // 0xF00A

    /// <summary>F24</summary>
    /// <remarks>Available on: Windows, Linux</remarks>
    VcF24 = 61451, // 0xF00B
}
