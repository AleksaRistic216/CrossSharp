using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Interfaces;

namespace Demos.TextEditor;

public class MainForm : Form
{
    FilesPicker? _filesPicker;
    readonly TabbedLayout _tabbedLayout = new();
    ITheme Theme => Services.GetSingleton<ITheme>();

    public MainForm()
    {
        // Registers service which provides data for TextEditTab instances
        Services.AddSingleton(new TextEditTabDataProvider());
        InitializeMenuBar();
        InitializeTabbedLayout();
        LoadInitialFiles();
    }

    void LoadInitialFiles()
    {
        var settings = SettingsHelpers.GetSettings();
        var dataProvider = GetDataProvider();
        foreach (var file in settings.Files)
        {
            if (!File.Exists(file))
                continue;
            var fileName = Path.GetFileName(file);
            dataProvider.LoadFile(file, fileName);
            _tabbedLayout.AddTab(fileName, typeof(TextEditTab));
        }
    }

    TextEditTabDataProvider GetDataProvider() => Services.GetSingleton<TextEditTabDataProvider>();

    void InitializeTabbedLayout()
    {
        _tabbedLayout.Dock = DockStyle.Fill;
        _tabbedLayout.DockIndex = 2;
        Controls.Add(_tabbedLayout);
        _tabbedLayout.AddTabButton(
            "+",
            () =>
            {
                var applicationDataPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData
                );
                var directoryPath = Path.Combine(applicationDataPath, "draft-files");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                var draftFilesCount = Directory.GetFiles(directoryPath).Length;
                var newFileName = $"Draft-{draftFilesCount + 1}.txt";
                var newFilePath = Path.Combine(directoryPath, newFileName);
                File.WriteAllText(newFilePath, string.Empty);
                var dataProvider = GetDataProvider();
                dataProvider.LoadFile(newFilePath, newFileName);
                _tabbedLayout.AddTab(newFileName, typeof(TextEditTab));
                _tabbedLayout.SelectTab(newFileName);
                var settings = SettingsHelpers.GetSettings();
                settings.Files = dataProvider.GetOpenFiles();
                SettingsHelpers.SaveSettings(settings);
            }
        );
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

        var btn1 = new Button();
        btn1.Text = "Open File";
        btn1.AutoSize = true;
        btn1.CornerRadius = 0;
        btn1.MinHeight = menuBarHeight;
        btn1.Click += OpenFileButton_Click;
        menuBar.Add(btn1);
    }

    void OpenFileButton_Click(object? sender, EventArgs e)
    {
        _filesPicker = new FilesPicker();
        _filesPicker.FilesSelected += OpenFileSelected;
        _filesPicker.Disposing += FilesPickerDisposing;
        _filesPicker.Show();
    }

    void FilesPickerDisposing(object? sender, EventArgs e)
    {
        if (_filesPicker is null)
            return;

        _filesPicker.FilesSelected -= OpenFileSelected;
        _filesPicker.Disposing -= FilesPickerDisposing;
    }

    void OpenFileSelected(object? sender, FilesSelectedEventArgs e)
    {
        if (e.SelectedFiles.Length > 0)
        {
            var dataProvider = GetDataProvider();
            var hasChange = false;
            foreach (var file in e.SelectedFiles)
            {
                if (dataProvider.IsFileOpen(file))
                {
                    Notifications.Show("File Already Open", $"The file {file} is already open.");
                    continue;
                }
                hasChange = true;
                var fileName = Path.GetFileName(file);
                dataProvider.LoadFile(file, fileName);
                _tabbedLayout.AddTab(fileName, typeof(TextEditTab));
                _tabbedLayout.SelectTab(fileName);
            }
            _filesPicker?.Close();

            if (hasChange)
            {
                var settings = SettingsHelpers.GetSettings();
                settings.Files = dataProvider.GetOpenFiles();
                SettingsHelpers.SaveSettings(settings);
            }
        }
        else
        {
            Notifications.Show("No File Selected", "You did not select any file.");
        }
    }
}
