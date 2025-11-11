using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using Demos.AllInOne.Models;

namespace Demos.AllInOne.MainFormViews;

public sealed class DataGridView : StaticLayout
{
    IDataGrid dataGrid = new DataGrid();
    IStackedLayout statusBar = new StackedLayout();

    public DataGridView()
    {
        Dock = DockStyle.Fill;
        InitializeDataGrid();
        InitializeStatusBar();
    }

    void InitializeDataGrid()
    {
        dataGrid.Dock = DockStyle.Fill;
        dataGrid.DockIndex = 0;
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

    void InitializeStatusBar()
    {
        statusBar.Height = 25;
        statusBar.Dock = DockStyle.Bottom;
        statusBar.DockIndex = 1;
        statusBar.BackgroundColor = Services.GetSingleton<ITheme>().PrimaryColor;
        Add(statusBar);

        // var label = new Label();
        // label.Text = "Status Bar";
        // label.Width = 200;
        // label.Height = 20;
        // statusBar.Add(label);
    }
}
