using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne.MainFormViews;

public sealed class DataGridView : StackedLayout
{
    public DataGridView()
    {
        Dock = DockStyle.Fill;

        var dataGrid = new DataGrid();
        dataGrid.Height = 300;
        Add(dataGrid);
    }
}
