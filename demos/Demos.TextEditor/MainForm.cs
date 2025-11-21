using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace Demos.TextEditor;

public class MainForm : Form
{
    FilesPicker? _filesPicker;
    IStackedLayout _menuBar;
    readonly TabbedLayout _tabbedLayout = new();
    IStackedLayout _editorBar;

    static ITheme Theme => Services.GetSingleton<ITheme>();

    public MainForm()
    {
        // Registers service which provides data for TextEditTab instances
        Services.AddSingleton(new TextEditTabDataProvider());
        InitializeMenuBar();
        InitializeTabbedLayout();
        LoadInitialFiles();
        InitializeEditorBar();
        ThemePerformed += MainForm_ThemePerformed;
    }

    void MainForm_ThemePerformed(object? sender, EventArgs e)
    {
        _menuBar.BackgroundColor = Theme.SecondaryColor;
        _editorBar.BackgroundColor = Theme.SecondaryColor;
        _menuBar.Padding = new Padding(2);
        _editorBar.Padding = new Padding(2);
        foreach (var btn in _menuBar.OfType<IButton>())
            btn.CornerRadius = 4;
        foreach (var btn in _editorBar.OfType<IButton>())
            btn.CornerRadius = 4;
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

    void InitializeEditorBar()
    {
        var editorBarHeight = 30;
        _editorBar = new StackedLayout();
        _editorBar.Height = editorBarHeight;
        _editorBar.Orientation = Orientation.Horizontal;
        _editorBar.Dock = DockStyle.Top;
        _editorBar.DockIndex = 1;
        Controls.Add(_editorBar);

        var btn = new Button();
        btn.Image = EfficientImage.Get(nameof(Constants.SaveFileIconKind));
        btn.Width = editorBarHeight;
        btn.Click += (_, _) => {
            // if (string.IsNullOrWhiteSpace(_title))
            // {
            //     Notifications.Show("Cannot save file: no title specified.");
            //     return;
            // }
            // var dataProvider = GetDataProvider();
            // dataProvider.SaveFileContents(_title, _input.Text);
        };
        _editorBar.Add(btn);
    }

    TextEditTabDataProvider GetDataProvider() => Services.GetSingleton<TextEditTabDataProvider>();

    void InitializeTabbedLayout()
    {
        _tabbedLayout.Dock = DockStyle.Fill;
        _tabbedLayout.DockIndex = 2;
        Controls.Add(_tabbedLayout);
        var btn = _tabbedLayout.CreateTabButton();
        btn.AutoSize = false;
        btn.Width = Constants.IMAGE_SIZE;
        btn.Image = EfficientImage.Get(nameof(Constants.AddFileIconKind));
        btn.Click += (_, _) =>
        {
            var applicationDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CrossSharp",
                "Demos",
                "TextEditor"
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
        };
    }

    void InitializeMenuBar()
    {
        var menuBarHeight = 30;
        _menuBar = new StackedLayout();
        _menuBar.Height = menuBarHeight;
        _menuBar.Orientation = Orientation.Horizontal;
        _menuBar.Dock = DockStyle.Top;
        _menuBar.DockIndex = 0;
        Controls.Add(_menuBar);

        var btn1 = new Button();
        btn1.Image = EfficientImage.Get(nameof(Constants.OpenFileIconKind));
        btn1.Width = Constants.IMAGE_SIZE + 4;
        btn1.CornerRadius = 0;
        btn1.MinHeight = menuBarHeight;
        btn1.Click += OpenFileButton_Click;
        _menuBar.Add(btn1);
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
