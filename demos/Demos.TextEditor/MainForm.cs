using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Interfaces;

namespace Demos.TextEditor;

public class MainForm : Form
{
    readonly TabbedLayout _tabbedLayout = new();
    ITheme Theme => Services.GetSingleton<ITheme>();
    string _selectedFilePath = string.Empty;

    public MainForm()
    {
        InitializeMenuBar();
        InitializeTabbedLayout();
    }

    void InitializeTabbedLayout()
    {
        _tabbedLayout.Dock = DockStyle.Fill;
        _tabbedLayout.DockIndex = 2;
        Controls.Add(_tabbedLayout);
        _tabbedLayout.AddTabButton(
            "+",
            () =>
            {
                Notifications.Show("New Tab Clicked", "You clicked the new tab button.");
            }
        );
    }

    void AddTextEditTab()
    {
        _tabbedLayout.AddTab("New Tab", typeof(TextEditTab));
    }

    void InitializeMenuBar()
    {
        var menuBarHeight = 30;
        var menuBar = new StackedLayout();
        menuBar.Height = menuBarHeight;
        menuBar.BackgroundColor = Theme.SecondaryBackgroundColor.Darkened;
        menuBar.Direction = Direction.Horizontal;
        menuBar.Dock = DockStyle.Top;
        menuBar.DockIndex = 0;
        Controls.Add(menuBar);

        var button1 = new Button();
        button1.Text = "New File";
        button1.AutoSize = true;
        button1.MinHeight = menuBarHeight;
        menuBar.Add(button1);

        var button2 = new Button();
        button2.Text = "Open File";
        button2.AutoSize = true;
        button2.MinHeight = menuBarHeight;
        button2.Click += OpenFileButton_Click;
        menuBar.Add(button2);
    }

    void OpenFileButton_Click(object? sender, EventArgs e)
    {
        var filesPicker = new FilesPicker();
        filesPicker.FilesSelected += OpenFileSelected;
        filesPicker.Show();
    }

    void OpenFileSelected(object? sender, FilesSelectedEventArgs e)
    {
        if (e.SelectedFiles.Length > 0)
        {
            var filePath = e.SelectedFiles[0];
            Notifications.Show("File Selected", $"You selected the file: {filePath}");
        }
        else
        {
            Notifications.Show("No File Selected", "You did not select any file.");
        }
    }
}
