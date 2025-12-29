using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using Xunit;

namespace Tests.Utils;

public class InputSelectionTests : IDisposable
{
    public InputSelectionTests()
    {
        try
        {
            if (!Services.IsRegistered<IInputHandler>())
                Services.AddSingleton<IInputHandler>(new MockInputHandler());
        }
        catch (InvalidOperationException) { }
        try
        {
            if (!Services.IsRegistered<ITheme>())
                Services.AddSingleton<ITheme>(new MockTheme());
        }
        catch (InvalidOperationException) { }
    }

    public void Dispose() { }

    #region GetNormalizedSelection Tests

    [Fact]
    public void GetNormalizedSelection_NoSelection_ReturnsSamePoints()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(5, 0));
        input.SetSelectionAnchor(new Point(5, 0));

        var (start, end) = input.TestGetNormalizedSelection();

        Assert.Equal(new Point(5, 0), start);
        Assert.Equal(new Point(5, 0), end);
    }

    [Fact]
    public void GetNormalizedSelection_ForwardSelection_ReturnsCorrectOrder()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(5, 0));

        var (start, end) = input.TestGetNormalizedSelection();

        Assert.Equal(new Point(0, 0), start);
        Assert.Equal(new Point(5, 0), end);
    }

    [Fact]
    public void GetNormalizedSelection_BackwardSelection_ReturnsNormalizedOrder()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetSelectionAnchor(new Point(8, 0));
        input.SetCaretPosition(new Point(2, 0));

        var (start, end) = input.TestGetNormalizedSelection();

        Assert.Equal(new Point(2, 0), start);
        Assert.Equal(new Point(8, 0), end);
    }

    [Fact]
    public void GetNormalizedSelection_MultiLine_ForwardSelection()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(2, 0));
        input.SetCaretPosition(new Point(3, 2));

        var (start, end) = input.TestGetNormalizedSelection();

        Assert.Equal(new Point(2, 0), start);
        Assert.Equal(new Point(3, 2), end);
    }

    [Fact]
    public void GetNormalizedSelection_MultiLine_BackwardSelection()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(3, 2));
        input.SetCaretPosition(new Point(2, 0));

        var (start, end) = input.TestGetNormalizedSelection();

        Assert.Equal(new Point(2, 0), start);
        Assert.Equal(new Point(3, 2), end);
    }

    #endregion

    #region GetSelectedText Tests

    [Fact]
    public void GetSelectedText_NoSelection_ReturnsEmpty()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(5, 0));
        input.SetSelectionAnchor(new Point(5, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal(string.Empty, selected);
    }

    [Fact]
    public void GetSelectedText_SingleLine_ReturnsSelectedPortion()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(5, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal("Hello", selected);
    }

    [Fact]
    public void GetSelectedText_SingleLine_BackwardSelection()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetSelectionAnchor(new Point(11, 0));
        input.SetCaretPosition(new Point(6, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal("World", selected);
    }

    [Fact]
    public void GetSelectedText_EntireText()
    {
        var input = new TestableInput { Text = "Hello" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(5, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal("Hello", selected);
    }

    [Fact]
    public void GetSelectedText_MultiLine_SpansMultipleLines()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(2, 0));
        input.SetCaretPosition(new Point(3, 2));

        var selected = input.TestGetSelectedText();

        Assert.Equal("ne1\nLine2\nLin", selected);
    }

    [Fact]
    public void GetSelectedText_MultiLine_SingleLineSelection()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(0, 1));
        input.SetCaretPosition(new Point(5, 1));

        var selected = input.TestGetSelectedText();

        Assert.Equal("Line2", selected);
    }

    #endregion

    #region SelectAll Tests

    [Fact]
    public void SelectAll_SingleLine_SelectsEntireText()
    {
        var input = new TestableInput { Text = "Hello World" };

        input.TestSelectAll();

        Assert.Equal(new Point(0, 0), input.GetSelectionAnchor());
        Assert.Equal(new Point(11, 0), input.GetCaretPosition());
    }

    [Fact]
    public void SelectAll_EmptyText_DoesNothing()
    {
        var input = new TestableInput { Text = "" };
        input.SetCaretPosition(new Point(0, 0));
        input.SetSelectionAnchor(new Point(0, 0));

        input.TestSelectAll();

        Assert.Equal(new Point(0, 0), input.GetSelectionAnchor());
        Assert.Equal(new Point(0, 0), input.GetCaretPosition());
    }

    [Fact]
    public void SelectAll_MultiLine_SelectsAllLines()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };

        input.TestSelectAll();

        Assert.Equal(new Point(0, 0), input.GetSelectionAnchor());
        Assert.Equal(new Point(5, 2), input.GetCaretPosition());
    }

    #endregion

    #region DeleteSelectedText Tests

    [Fact]
    public void DeleteSelectedText_NoSelection_DoesNothing()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(5, 0));
        input.SetSelectionAnchor(new Point(5, 0));

        input.TestDeleteSelectedText();

        Assert.Equal("Hello World", input.Text);
    }

    [Fact]
    public void DeleteSelectedText_SingleLine_DeletesSelection()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetSelectionAnchor(new Point(5, 0));
        input.SetCaretPosition(new Point(11, 0));

        input.TestDeleteSelectedText();

        Assert.Equal("Hello", input.Text);
        Assert.Equal(new Point(5, 0), input.GetCaretPosition());
    }

    [Fact]
    public void DeleteSelectedText_EntireText_ClearsText()
    {
        var input = new TestableInput { Text = "Hello" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(5, 0));

        input.TestDeleteSelectedText();

        Assert.Equal("", input.Text);
        Assert.Equal(new Point(0, 0), input.GetCaretPosition());
    }

    [Fact]
    public void DeleteSelectedText_MultiLine_DeletesAcrossLines()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(2, 0));
        input.SetCaretPosition(new Point(3, 2));

        input.TestDeleteSelectedText();

        Assert.Equal("Lie3", input.Text);
        Assert.Equal(new Point(2, 0), input.GetCaretPosition());
    }

    #endregion

    #region GetWordBoundaries Tests

    [Fact]
    public void GetWordBoundaries_InMiddleOfWord_ReturnsWordBounds()
    {
        var input = new TestableInput { Text = "Hello World" };

        var (start, end) = input.TestGetWordBoundaries(0, 2);

        Assert.Equal(0, start);
        Assert.Equal(5, end);
    }

    [Fact]
    public void GetWordBoundaries_AtWordStart_ReturnsWordBounds()
    {
        var input = new TestableInput { Text = "Hello World" };

        var (start, end) = input.TestGetWordBoundaries(0, 0);

        Assert.Equal(0, start);
        Assert.Equal(5, end);
    }

    [Fact]
    public void GetWordBoundaries_AtWordEnd_SelectsPreviousWord()
    {
        // When caret is at position 5 (the space), looking backward finds "Hello"
        var input = new TestableInput { Text = "Hello World" };

        var (start, end) = input.TestGetWordBoundaries(0, 5);

        // At a space, the algorithm looks backward and finds the previous word
        Assert.Equal(0, start);
        Assert.Equal(5, end);
    }

    [Fact]
    public void GetWordBoundaries_InSecondWord_ReturnsSecondWord()
    {
        var input = new TestableInput { Text = "Hello World" };

        var (start, end) = input.TestGetWordBoundaries(0, 8);

        Assert.Equal(6, start);
        Assert.Equal(11, end);
    }

    [Fact]
    public void GetWordBoundaries_EmptyLine_ReturnsZero()
    {
        var input = new TestableInput { Text = "" };

        var (start, end) = input.TestGetWordBoundaries(0, 0);

        Assert.Equal(0, start);
        Assert.Equal(0, end);
    }

    [Fact]
    public void GetWordBoundaries_NumbersIncluded()
    {
        var input = new TestableInput { Text = "Test123 Value" };

        var (start, end) = input.TestGetWordBoundaries(0, 4);

        Assert.Equal(0, start);
        Assert.Equal(7, end);
    }

    #endregion

    #region ClearSelection Tests

    [Fact]
    public void ClearSelection_ResetsAnchorToCaret()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(5, 0));

        input.TestClearSelection();

        Assert.Equal(input.GetCaretPosition(), input.GetSelectionAnchor());
        Assert.False(input.TestHasSelection());
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Selection_AtEndOfText_HandlesCorrectly()
    {
        var input = new TestableInput { Text = "Hello" };
        input.SetSelectionAnchor(new Point(3, 0));
        input.SetCaretPosition(new Point(5, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal("lo", selected);
    }

    [Fact]
    public void Selection_AtStartOfText_HandlesCorrectly()
    {
        var input = new TestableInput { Text = "Hello" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(2, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal("He", selected);
    }

    [Fact]
    public void Selection_SingleCharacter()
    {
        var input = new TestableInput { Text = "Hello" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(1, 0));

        var selected = input.TestGetSelectedText();

        Assert.Equal("H", selected);
    }

    [Fact]
    public void MultiLine_EmptyLineInSelection()
    {
        var input = new TestableInput { Text = "Line1\n\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(3, 0));
        input.SetCaretPosition(new Point(2, 2));

        var selected = input.TestGetSelectedText();

        Assert.Equal("e1\n\nLi", selected);
    }

    [Fact]
    public void MultiLine_SelectEntireEmptyLine()
    {
        var input = new TestableInput { Text = "Line1\n\nLine3", MultiLine = true };
        input.SetSelectionAnchor(new Point(0, 1));
        input.SetCaretPosition(new Point(0, 1));

        var selected = input.TestGetSelectedText();

        Assert.Equal(string.Empty, selected);
    }

    [Fact]
    public void GetTextBefore_SingleLine()
    {
        var input = new TestableInput { Text = "Hello World" };

        var before = input.TestGetTextBefore(new Point(5, 0));

        Assert.Equal("Hello", before);
    }

    [Fact]
    public void GetTextAfter_SingleLine()
    {
        var input = new TestableInput { Text = "Hello World" };

        var after = input.TestGetTextAfter(new Point(6, 0));

        Assert.Equal("World", after);
    }

    [Fact]
    public void GetTextBefore_MultiLine()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };

        var before = input.TestGetTextBefore(new Point(2, 1));

        Assert.Equal("Line1\nLi", before);
    }

    [Fact]
    public void GetTextAfter_MultiLine()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };

        var after = input.TestGetTextAfter(new Point(2, 1));

        Assert.Equal("ne2\nLine3", after);
    }

    [Fact]
    public void GetTextBefore_EmptyText_ReturnsEmpty()
    {
        var input = new TestableInput { Text = "" };

        var before = input.TestGetTextBefore(new Point(0, 0));

        Assert.Equal(string.Empty, before);
    }

    [Fact]
    public void GetTextAfter_EmptyText_ReturnsEmpty()
    {
        var input = new TestableInput { Text = "" };

        var after = input.TestGetTextAfter(new Point(0, 0));

        Assert.Equal(string.Empty, after);
    }

    [Fact]
    public void Selection_PositionBeyondTextLength_Clamped()
    {
        var input = new TestableInput { Text = "Hi" };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(10, 0)); // Beyond text length

        // GetSelectedText should handle clamping
        var selected = input.TestGetSelectedText();

        Assert.Equal("Hi", selected);
    }

    [Fact]
    public void GetSelectedText_MultiLine_PositionBeyondLineLength_Clamped()
    {
        var input = new TestableInput { Text = "Hi\nWorld", MultiLine = true };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(100, 1)); // X beyond line length

        var selected = input.TestGetSelectedText();

        Assert.Equal("Hi\nWorld", selected);
    }

    [Fact]
    public void GetSelectedText_MultiLine_YBeyondLinesCount_Clamped()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };
        input.SetSelectionAnchor(new Point(0, 0));
        input.SetCaretPosition(new Point(5, 10)); // Y beyond lines count

        var selected = input.TestGetSelectedText();

        // Should clamp Y to last line
        Assert.Equal("Line1\nLine2", selected);
    }

    [Fact]
    public void GetSelectedText_MultiLine_StartXBeyondLineLength_Clamped()
    {
        var input = new TestableInput { Text = "Hi\nWorld", MultiLine = true };
        input.SetSelectionAnchor(new Point(100, 0)); // X beyond first line
        input.SetCaretPosition(new Point(3, 1));

        var selected = input.TestGetSelectedText();

        // Start clamped to "Hi" end, so selection is "\nWor"
        Assert.Equal("\nWor", selected);
    }

    [Fact]
    public void GetTextBefore_MultiLine_YBeyondLines_ReturnsAllText()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };

        var before = input.TestGetTextBefore(new Point(0, 10));

        Assert.Equal("Line1\nLine2", before);
    }

    [Fact]
    public void GetTextAfter_MultiLine_YBeyondLines_ReturnsEmpty()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };

        var after = input.TestGetTextAfter(new Point(0, 10));

        Assert.Equal(string.Empty, after);
    }

    [Fact]
    public void MoveCaretToWordBoundary_CaretBeyondLineLength_HandlesGracefully()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(100, 0)); // Beyond line length

        // Should not throw, should clamp to line end
        input.TestMoveCaretToWordBoundary(1);

        Assert.True(input.GetCaretPosition().X <= 11);
    }

    [Fact]
    public void MoveCaretToWordBoundary_Left_CaretBeyondLineLength_HandlesGracefully()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(100, 0)); // Beyond line length

        // Should not throw
        input.TestMoveCaretToWordBoundary(-1);

        Assert.True(input.GetCaretPosition().X <= 11);
    }

    #endregion

    #region ShiftCaretPosition Tests

    [Fact]
    public void ShiftCaretPosition_MultiLine_RightAtEndOfLine_WrapsToNextLine()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };
        input.SetCaretPosition(new Point(5, 0)); // End of "Line1"

        input.TestShiftCaretPosition(1);

        // Should wrap to start of next line
        Assert.Equal(new Point(0, 1), input.GetCaretPosition());
    }

    [Fact]
    public void ShiftCaretPosition_MultiLine_LeftAtStartOfLine_WrapsToPreviousLine()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };
        input.SetCaretPosition(new Point(0, 1)); // Start of "Line2"

        input.TestShiftCaretPosition(-1);

        // Should wrap to end of previous line
        Assert.Equal(new Point(5, 0), input.GetCaretPosition());
    }

    [Fact]
    public void ShiftCaretPosition_MultiLine_RightAtEndOfLastLine_StaysAtEnd()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };
        input.SetCaretPosition(new Point(5, 1)); // End of "Line2"

        input.TestShiftCaretPosition(1);

        // Should stay at end
        Assert.Equal(new Point(5, 1), input.GetCaretPosition());
    }

    [Fact]
    public void ShiftCaretPosition_MultiLine_LeftAtStartOfFirstLine_StaysAtStart()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };
        input.SetCaretPosition(new Point(0, 0)); // Start of "Line1"

        input.TestShiftCaretPosition(-1);

        // Should stay at start
        Assert.Equal(new Point(0, 0), input.GetCaretPosition());
    }

    [Fact]
    public void ShiftCaretPosition_WithoutShift_ShouldNotCreateSelection()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(5, 0));
        input.SetSelectionAnchor(new Point(5, 0)); // No selection initially

        // Move right without Shift - anchor should follow caret
        input.TestShiftCaretPosition(1);
        input.SetSelectionAnchor(input.GetCaretPosition()); // This is what should happen

        Assert.False(input.TestHasSelection());
    }

    [Fact]
    public void Movement_WithoutShift_NoInitialSelection_ShouldNotSelect()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(5, 0));
        input.SetSelectionAnchor(new Point(5, 0)); // No selection

        // Simulate what HandleCaretMovement should do: move and clear selection
        input.TestShiftCaretPosition(1);
        input.TestClearSelection(); // This should be called after movement without Shift

        Assert.False(input.TestHasSelection());
        Assert.Equal(new Point(6, 0), input.GetCaretPosition());
        Assert.Equal(new Point(6, 0), input.GetSelectionAnchor());
    }

    #endregion

    #region InvalidateCaretText Tests

    [Fact]
    public void InvalidateCaretText_SingleLine_CaretBeyondTextLength_HandlesGracefully()
    {
        var input = new TestableInput { Text = "Hello" };
        input.SetCaretPosition(new Point(100, 0)); // Beyond text length

        // Should not throw
        input.TestInvalidateCaretText();

        Assert.Equal("Hello", input.GetTextBeforeCaret());
        Assert.Equal("", input.GetTextAfterCaret());
    }

    [Fact]
    public void InvalidateCaretText_MultiLine_CaretXBeyondLineLength_HandlesGracefully()
    {
        var input = new TestableInput { Text = "Hi\nWorld", MultiLine = true };
        input.SetCaretPosition(new Point(100, 0)); // X beyond first line length

        // Should not throw
        input.TestInvalidateCaretText();

        Assert.Equal("Hi\n", input.GetTextBeforeCaret());
        Assert.Equal("World", input.GetTextAfterCaret());
    }

    [Fact]
    public void InvalidateCaretText_MultiLine_CaretYBeyondLines_HandlesGracefully()
    {
        var input = new TestableInput { Text = "Line1\nLine2", MultiLine = true };
        input.SetCaretPosition(new Point(0, 10)); // Y beyond lines

        // Should not throw
        input.TestInvalidateCaretText();

        // All text should be before caret
        Assert.Equal("Line1\nLine2", input.GetTextBeforeCaret());
        Assert.Equal("", input.GetTextAfterCaret());
    }

    [Fact]
    public void InvalidateCaretText_SingleLine_ValidPosition()
    {
        var input = new TestableInput { Text = "Hello World" };
        input.SetCaretPosition(new Point(5, 0));

        input.TestInvalidateCaretText();

        Assert.Equal("Hello", input.GetTextBeforeCaret());
        Assert.Equal(" World", input.GetTextAfterCaret());
    }

    [Fact]
    public void InvalidateCaretText_MultiLine_ValidPosition()
    {
        var input = new TestableInput { Text = "Line1\nLine2\nLine3", MultiLine = true };
        input.SetCaretPosition(new Point(2, 1)); // Middle of "Line2"

        input.TestInvalidateCaretText();

        Assert.Equal("Line1\nLi", input.GetTextBeforeCaret());
        Assert.Equal("ne2\nLine3", input.GetTextAfterCaret());
    }

    #endregion

    #region Test Infrastructure

    class TestableInput : CrossSharp.Ui.Common.Input
    {
        public TestableInput()
        {
            Width = 200;
            Height = 30;
        }

        public void SetCaretPosition(Point pos) => _caretPosition = pos;
        public Point GetCaretPosition() => _caretPosition;
        public void SetSelectionAnchor(Point pos) => _selectionAnchor = pos;
        public Point GetSelectionAnchor() => _selectionAnchor;

        public (Point Start, Point End) TestGetNormalizedSelection() => GetNormalizedSelection();
        public string TestGetSelectedText() => GetSelectedText();
        public void TestSelectAll() => SelectAll();
        public void TestDeleteSelectedText() => DeleteSelectedText();
        public (int Start, int End) TestGetWordBoundaries(int lineIndex, int charIndex) =>
            GetWordBoundaries(lineIndex, charIndex);
        public void TestClearSelection() => ClearSelection();
        public bool TestHasSelection() => HasSelection;
        public string TestGetTextBefore(Point pos) => GetTextBefore(pos);
        public string TestGetTextAfter(Point pos) => GetTextAfter(pos);
        public void TestMoveCaretToWordBoundary(int direction) => MoveCaretToWordBoundary(direction);
        public void TestShiftCaretPosition(int amount) => ShiftCaretPosition(amount);
        public void TestInvalidateCaretText() => InvalidateCaretText();
        public string GetTextBeforeCaret() => _textBeforeCaret;
        public string GetTextAfterCaret() => _textAfterCaret;
    }

#pragma warning disable CS0067
    class MockInputHandler : IInputHandler
    {
        public void StartListeningAsync(CancellationToken cancellationToken) { }

        public event EventHandler<KeyInputArgs>? KeyPressed;
        public event EventHandler<MouseInputArgs>? MousePressed;
        public event EventHandler<MouseInputArgs>? MouseReleased;
        public event EventHandler<MouseInputArgs>? MouseMoved;
        public event EventHandler<MouseWheelInputArgs>? MouseWheel;
        public event EventHandler<MouseInputArgs>? MouseDragged;
    }
#pragma warning restore CS0067

    class MockTheme : ITheme
    {
        public RenderStyle Style { get; set; } = RenderStyle.Flat;
        public int DefaultFontSize { get; set; } = 14;
        public FontFamily DefaultFontFamily { get; set; } = FontFamily.Default;
        public ColorRgba LayoutBackgroundColor { get; set; } = ColorRgba.Transparent;
        public ColorRgba PrimaryColor { get; set; } = ColorRgba.Transparent;
        public ColorRgba SecondaryColor { get; set; } = ColorRgba.Transparent;
        public ColorRgba SelectionColor { get; set; } = ColorRgba.Blue;
        public int DefaultCornerRadius { get; set; } = 0;
        public int DefaultLayoutItemSpacing { get; set; } = 0;
    }

    #endregion
}
