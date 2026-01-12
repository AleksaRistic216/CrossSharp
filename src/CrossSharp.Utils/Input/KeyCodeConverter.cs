namespace CrossSharp.Utils.Input;

/// <summary>
/// Utility class for converting key codes to characters.
/// </summary>
public static class KeyCodeConverter
{
    /// <summary>
    /// Converts a key code to its corresponding character, taking shift modifier into account.
    /// </summary>
    /// <param name="keyCode">The key code to convert.</param>
    /// <param name="shift">Whether the shift key is pressed.</param>
    /// <returns>The character representation, or null if the key code has no character representation.</returns>
    public static char? ToChar(KeyCode keyCode, bool shift)
    {
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
}
