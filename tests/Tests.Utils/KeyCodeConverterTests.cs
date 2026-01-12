using CrossSharp.Utils.Input;
using Xunit;

namespace Tests.Utils;

public class KeyCodeConverterTests
{
    [Theory]
    [InlineData(KeyCode.VcA, false, 'a')]
    [InlineData(KeyCode.VcA, true, 'A')]
    [InlineData(KeyCode.VcB, false, 'b')]
    [InlineData(KeyCode.VcB, true, 'B')]
    [InlineData(KeyCode.VcC, false, 'c')]
    [InlineData(KeyCode.VcC, true, 'C')]
    [InlineData(KeyCode.VcZ, false, 'z')]
    [InlineData(KeyCode.VcZ, true, 'Z')]
    public void ToChar_Letters_ReturnsCorrectCharacter(KeyCode keyCode, bool shift, char expected)
    {
        var result = KeyCodeConverter.ToChar(keyCode, shift);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(KeyCode.Vc0, false, '0')]
    [InlineData(KeyCode.Vc0, true, ')')]
    [InlineData(KeyCode.Vc1, false, '1')]
    [InlineData(KeyCode.Vc1, true, '!')]
    [InlineData(KeyCode.Vc2, false, '2')]
    [InlineData(KeyCode.Vc2, true, '@')]
    [InlineData(KeyCode.Vc3, false, '3')]
    [InlineData(KeyCode.Vc3, true, '#')]
    [InlineData(KeyCode.Vc4, false, '4')]
    [InlineData(KeyCode.Vc4, true, '$')]
    [InlineData(KeyCode.Vc5, false, '5')]
    [InlineData(KeyCode.Vc5, true, '%')]
    [InlineData(KeyCode.Vc6, false, '6')]
    [InlineData(KeyCode.Vc6, true, '^')]
    [InlineData(KeyCode.Vc7, false, '7')]
    [InlineData(KeyCode.Vc7, true, '&')]
    [InlineData(KeyCode.Vc8, false, '8')]
    [InlineData(KeyCode.Vc8, true, '*')]
    [InlineData(KeyCode.Vc9, false, '9')]
    [InlineData(KeyCode.Vc9, true, '(')]
    public void ToChar_Numbers_ReturnsCorrectCharacter(KeyCode keyCode, bool shift, char expected)
    {
        var result = KeyCodeConverter.ToChar(keyCode, shift);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(KeyCode.VcMinus, false, '-')]
    [InlineData(KeyCode.VcMinus, true, '_')]
    [InlineData(KeyCode.VcEquals, false, '=')]
    [InlineData(KeyCode.VcEquals, true, '+')]
    [InlineData(KeyCode.VcBackslash, false, '\\')]
    [InlineData(KeyCode.VcBackslash, true, '|')]
    [InlineData(KeyCode.VcSemicolon, false, ';')]
    [InlineData(KeyCode.VcSemicolon, true, ':')]
    [InlineData(KeyCode.VcQuote, false, '\'')]
    [InlineData(KeyCode.VcQuote, true, '"')]
    [InlineData(KeyCode.VcComma, false, ',')]
    [InlineData(KeyCode.VcComma, true, '<')]
    [InlineData(KeyCode.VcPeriod, false, '.')]
    [InlineData(KeyCode.VcPeriod, true, '>')]
    [InlineData(KeyCode.VcSlash, false, '/')]
    [InlineData(KeyCode.VcSlash, true, '?')]
    public void ToChar_Punctuation_ReturnsCorrectCharacter(KeyCode keyCode, bool shift, char expected)
    {
        var result = KeyCodeConverter.ToChar(keyCode, shift);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ToChar_Space_ReturnsSpace(bool shift)
    {
        var result = KeyCodeConverter.ToChar(KeyCode.VcSpace, shift);
        Assert.Equal(' ', result);
    }

    [Theory]
    [InlineData(KeyCode.VcEnter)]
    [InlineData(KeyCode.VcTab)]
    [InlineData(KeyCode.VcBackspace)]
    [InlineData(KeyCode.VcEscape)]
    [InlineData(KeyCode.VcF1)]
    [InlineData(KeyCode.VcLeftShift)]
    [InlineData(KeyCode.VcLeftControl)]
    [InlineData(KeyCode.VcLeftAlt)]
    public void ToChar_NonPrintableKeys_ReturnsNull(KeyCode keyCode)
    {
        var result = KeyCodeConverter.ToChar(keyCode, false);
        Assert.Null(result);

        var resultShifted = KeyCodeConverter.ToChar(keyCode, true);
        Assert.Null(resultShifted);
    }

    [Fact]
    public void ToChar_AllLetters_WithoutShift_ReturnsLowercase()
    {
        var letters = new[]
        {
            (KeyCode.VcA, 'a'),
            (KeyCode.VcB, 'b'),
            (KeyCode.VcC, 'c'),
            (KeyCode.VcD, 'd'),
            (KeyCode.VcE, 'e'),
            (KeyCode.VcF, 'f'),
            (KeyCode.VcG, 'g'),
            (KeyCode.VcH, 'h'),
            (KeyCode.VcI, 'i'),
            (KeyCode.VcJ, 'j'),
            (KeyCode.VcK, 'k'),
            (KeyCode.VcL, 'l'),
            (KeyCode.VcM, 'm'),
            (KeyCode.VcN, 'n'),
            (KeyCode.VcO, 'o'),
            (KeyCode.VcP, 'p'),
            (KeyCode.VcQ, 'q'),
            (KeyCode.VcR, 'r'),
            (KeyCode.VcS, 's'),
            (KeyCode.VcT, 't'),
            (KeyCode.VcU, 'u'),
            (KeyCode.VcV, 'v'),
            (KeyCode.VcW, 'w'),
            (KeyCode.VcX, 'x'),
            (KeyCode.VcY, 'y'),
            (KeyCode.VcZ, 'z'),
        };

        foreach (var (keyCode, expected) in letters)
        {
            var result = KeyCodeConverter.ToChar(keyCode, shift: false);
            Assert.Equal(expected, result);
        }
    }

    [Fact]
    public void ToChar_AllLetters_WithShift_ReturnsUppercase()
    {
        var letters = new[]
        {
            (KeyCode.VcA, 'A'),
            (KeyCode.VcB, 'B'),
            (KeyCode.VcC, 'C'),
            (KeyCode.VcD, 'D'),
            (KeyCode.VcE, 'E'),
            (KeyCode.VcF, 'F'),
            (KeyCode.VcG, 'G'),
            (KeyCode.VcH, 'H'),
            (KeyCode.VcI, 'I'),
            (KeyCode.VcJ, 'J'),
            (KeyCode.VcK, 'K'),
            (KeyCode.VcL, 'L'),
            (KeyCode.VcM, 'M'),
            (KeyCode.VcN, 'N'),
            (KeyCode.VcO, 'O'),
            (KeyCode.VcP, 'P'),
            (KeyCode.VcQ, 'Q'),
            (KeyCode.VcR, 'R'),
            (KeyCode.VcS, 'S'),
            (KeyCode.VcT, 'T'),
            (KeyCode.VcU, 'U'),
            (KeyCode.VcV, 'V'),
            (KeyCode.VcW, 'W'),
            (KeyCode.VcX, 'X'),
            (KeyCode.VcY, 'Y'),
            (KeyCode.VcZ, 'Z'),
        };

        foreach (var (keyCode, expected) in letters)
        {
            var result = KeyCodeConverter.ToChar(keyCode, shift: true);
            Assert.Equal(expected, result);
        }
    }
}
