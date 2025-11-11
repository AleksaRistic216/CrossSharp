using CrossSharp.Ui;
using CrossSharp.Ui.Common;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.AllInOne.MainFormViews;

public sealed class DataGridView : StackedLayout
{
    class User : IDataGridDataItem
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }
    }

    public DataGridView()
    {
        Dock = DockStyle.Fill;
        var dataGrid = new DataGrid();
        dataGrid.Height = 300;
        dataGrid.Configuration.Columns["Asd"].HeaderText = "Hello";
        var list = new List<User>();
        for (int i = 0; i < 1_000_000; i++)
        {
            list.Add(
                new User
                {
                    Name = "User " + i,
                    Age = 20 + (i % 30),
                    Country = "Country " + (i % 100),
                }
            );
        }
        dataGrid.DataSource = list.AsQueryable();
        Add(dataGrid);
    }
}
