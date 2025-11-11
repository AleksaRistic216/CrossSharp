using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using Demos.AllInOne.Models;

namespace Demos.AllInOne.MainFormViews;

public sealed class DataGridView : StaticLayout
{
    ILabel currentlyCachedRowsCountLabel = new Label();
    IDataGrid dataGrid = new DataGrid();
    IStackedLayout statusBar = new StackedLayout();
    int usersCount = 1_000_000;

    public DataGridView()
    {
        Dock = DockStyle.Fill;
        InitializeDataGrid();
        InitializeStatusBar();
        Task.Run(() =>
        {
            while (true)
            {
                currentlyCachedRowsCountLabel.Text =
                    "Cached rows: " + dataGrid.CurrentlyLoadedItemsCount.ToString("##,###");
                Thread.Sleep(500);
            }
        });
    }

    void InitializeDataGrid()
    {
        dataGrid.Dock = DockStyle.Fill;
        dataGrid.DockIndex = 1;
        dataGrid.Configuration.Columns["Asd"].HeaderText = "Hello";
        var list = new List<User>();
        for (int i = 0; i < usersCount; i++)
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
        statusBar.DockIndex = 0;
        statusBar.Orientation = Orientation.Horizontal;
        Add(statusBar);

        var label = new Label();
        label.Text = "Total rows: " + usersCount.ToString("##,###");
        label.Width = 200;
        label.Height = 20;
        statusBar.Add(label);

        currentlyCachedRowsCountLabel.Text = "Cached rows: " + dataGrid.CurrentlyLoadedItemsCount.ToString("0");
        currentlyCachedRowsCountLabel.Width = 200;
        currentlyCachedRowsCountLabel.Height = 20;
        statusBar.Add(currentlyCachedRowsCountLabel);
    }
}
