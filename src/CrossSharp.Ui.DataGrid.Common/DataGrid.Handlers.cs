using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Common;

partial class DataGrid
{
    public EventHandler<DataGridCellsSelectionChangedArgs>? CellsSelectionChanged { get; set; }

    void RaiseCellsSelectionChanged(DataGridCellsSelectionChangedArgs args)
    {
        CellsSelectionChanged?.Invoke(this, args);
    }

    void OnCellsSelectionChanged(DataGridCellsSelectionChangedArgs args)
    {
        RaiseCellsSelectionChanged(args);
    }

    public EventHandler? BackgroundColorChanged { get; set; }

    void RaiseBackgroundColorChanged()
    {
        BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnBackgroundColorChanged()
    {
        Invalidate();
    }

    public EventHandler? DataSourceChanged { get; set; }

    void RaiseDataSourceChanged()
    {
        DataSourceChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnDataSourceChanged()
    {
        InvalidateDataSourcePropertiesCache();
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    void InputHandlerOnMouseWheel(object? sender, MouseWheelInputArgs e)
    {
        if (!IsMouseOver)
            return;

        var rotation = e.Rotation;
        rotation /= 10;
        if (Math.Abs(rotation) <= 0)
            return;
        if (Scrollable == ScrollableMode.Vertical)
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, this, ref _viewport);
        else if (Scrollable == ScrollableMode.Horizontal)
            ScrollableHelpers.Scroll(Orientation.Horizontal, rotation, this, ref _viewport);
        else if (Scrollable == ScrollableMode.Both)
        {
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, this, ref _viewport);
            ScrollableHelpers.Scroll(Orientation.Horizontal, rotation, this, ref _viewport);
        }
        OnScrolled();
    }

    void InputHandlerOnKeyPressed(object? sender, KeyInputArgs e)
    {
        // if arrow
        // if (!IsFocused)
        //     return;
        switch (e.KeyCode)
        {
            case KeyCode.VcEscape:
                _selectedCells.Clear();
                OnCellsSelectionChanged(new DataGridCellsSelectionChangedArgs([]));
                break;
            case KeyCode.VcRight:
                ShiftCellSelection(e.IsShiftPressed, 1, 0);
                break;
            case KeyCode.VcLeft:
                ShiftCellSelection(e.IsShiftPressed, -1, 0);
                return;
            case KeyCode.VcUp:
                ShiftCellSelection(e.IsShiftPressed, 0, -1);
                return;
            case KeyCode.VcDown:
                ShiftCellSelection(e.IsShiftPressed, 0, 1);
                return;
        }
    }

    void ShiftCellSelection(bool isShiftPressed, int byX, int byY)
    {
        if (_dataSourceProperties == null || !_dataSourceProperties.Any())
            return;

        var lastSelectedCell = _selectedCells.LastOrDefault();
        if (lastSelectedCell is null)
        {
            _selectedCells.Add(new DataGridCell(0, _dataSourceProperties.First().Key));
            OnCellsSelectionChanged(new DataGridCellsSelectionChangedArgs(_selectedCells.ToArray()));
            return;
        }

        if (!isShiftPressed)
            _selectedCells.Clear();

        int newRowIndex = Math.Max(lastSelectedCell.RowIndex + byY, 0);
        string? newColumn = GetNextColumnName(lastSelectedCell.Column, byX);

        if (!_dataSourceProperties.ContainsKey(newColumn))
            return;

        var nextCell = new DataGridCell(newRowIndex, newColumn);

        if (isShiftPressed && _selectedCells.Count > 0)
        {
            var firstCell = _selectedCells.First();
            int minRow = Math.Min(firstCell.RowIndex, nextCell.RowIndex);
            int maxRow = Math.Max(firstCell.RowIndex, nextCell.RowIndex);

            var columns = _dataSourceProperties.Keys.ToList();
            int firstColIndex = columns.IndexOf(firstCell.Column);
            int nextColIndex = columns.IndexOf(nextCell.Column);
            int minCol = Math.Min(firstColIndex, nextColIndex);
            int maxCol = Math.Max(firstColIndex, nextColIndex);

            _selectedCells.Clear();
            for (int row = minRow; row <= maxRow; row++)
            {
                for (int col = minCol; col <= maxCol; col++)
                {
                    _selectedCells.Add(new DataGridCell(row, columns[col]));
                }
            }
        }
        else
        {
            if (_selectedCells.Any(x => x.RowIndex == nextCell.RowIndex && x.Column == nextCell.Column))
            {
                _selectedCells.RemoveAll(x => x.RowIndex == nextCell.RowIndex && x.Column == nextCell.Column);
            }
            else
            {
                _selectedCells.Add(nextCell);
            }
        }

        OnCellsSelectionChanged(new DataGridCellsSelectionChangedArgs(_selectedCells.ToArray()));
    }

    public EventHandler? Scrolled { get; set; }

    void RaiseScrolled()
    {
        Scrolled?.Invoke(this, EventArgs.Empty);
    }

    void OnScrolled()
    {
        CacheItems();
        RaiseScrolled();
    }
}
