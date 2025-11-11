using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class DataGrid() : CrossControl<IDataGrid>(Services.GetSingleton<IDataGridFactory>().Create()), IDataGrid
{
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }
    public int CornerRadius
    {
        get => Implementation.CornerRadius;
        set => Implementation.CornerRadius = value;
    }
}
