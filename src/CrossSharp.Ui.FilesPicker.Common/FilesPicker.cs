using System.Drawing;
using CrossSharp.Desktop;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

class FilesPicker : Form, IFilesPicker
{
    StackedLayout _leftRow = null!;
    StackedLayout _rightRow = null!;
    StackedLayout _actionBar = null!;
    StackedLayout _contentsList = null!;
    Input _locationInput = null!;
    ITheme Theme => Services.GetSingleton<ITheme>();
    readonly string _currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    const int LEFT_ROW_WIDTH = 150;
    const int BLOCK_HEIGHT = 40;

    public EventHandler<FilesSelectedEventArgs>? FilesSelected { get; set; }

    internal FilesPicker()
    {
        InitializeLeftRow();
        InitializeRightRow();
        LoadDirectoryContents(_currentDirectory);
        SubscribeToEvents();
    }

    public override void Dispose()
    {
        UnsubscribeFromEvents();
        base.Dispose();
    }

    void SubscribeToEvents()
    {
        ThemePerformed += OnThemePerformed;
    }

    void UnsubscribeFromEvents()
    {
        ThemePerformed -= OnThemePerformed;
    }

    void OnThemePerformed(object? sender, EventArgs e)
    {
        foreach (var btn in _contentsList.OfType<IButton>())
        {
            if (btn.Tag is not string key)
                continue;
            var isDir = Directory.Exists(key);
            btn.BackgroundColor = isDir ? ColorRgba.Orange.Darkened : Theme.PrimaryColor;
        }
    }

    void InitializeLeftRow()
    {
        _leftRow = new StackedLayout();
        _leftRow.Dock = DockStyle.Left;
        _leftRow.Width = LEFT_ROW_WIDTH;
        _leftRow.DockIndex = 0;
        Controls.Add(_leftRow);

        InitializeRootButton();
        InitializeHomeButton();
        InitializeDownloadsButton();
        InitializeDocumentsButton();
        InitializeMusicButton();
        InitializePicturesButton();
        InitializeVideosButton();
    }

    void InitializeRootButton()
    {
        var rootButton = new Button();
        rootButton.Text = "Root (/)";
        rootButton.TextAlignment = Alignment.Left;
        rootButton.CornerRadius = 0;
        rootButton.Height = BLOCK_HEIGHT;
        rootButton.Tag = "/";
        rootButton.Click = OnLocationButtonClicked;
        _leftRow.Add(rootButton);
    }

    void InitializeHomeButton()
    {
        var homeButton = new Button();
        homeButton.Text = "Home (~)";
        homeButton.TextAlignment = Alignment.Left;
        homeButton.CornerRadius = 0;
        homeButton.Height = BLOCK_HEIGHT;
        homeButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        homeButton.Click = OnLocationButtonClicked;
        _leftRow.Add(homeButton);
    }

    void InitializeDocumentsButton()
    {
        var documentsButton = new Button();
        documentsButton.Text = "Documents";
        documentsButton.TextAlignment = Alignment.Left;
        documentsButton.CornerRadius = 0;
        documentsButton.Height = BLOCK_HEIGHT;
        documentsButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        documentsButton.Click = OnLocationButtonClicked;
        _leftRow.Add(documentsButton);
    }

    void InitializeMusicButton()
    {
        var musicButton = new Button();
        musicButton.Text = "Music";
        musicButton.TextAlignment = Alignment.Left;
        musicButton.CornerRadius = 0;
        musicButton.Height = BLOCK_HEIGHT;
        musicButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        musicButton.Click = OnLocationButtonClicked;
        _leftRow.Add(musicButton);
    }

    void InitializePicturesButton()
    {
        var picturesButton = new Button();
        picturesButton.Text = "Pictures";
        picturesButton.TextAlignment = Alignment.Left;
        picturesButton.CornerRadius = 0;
        picturesButton.Height = BLOCK_HEIGHT;
        picturesButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        picturesButton.Click = OnLocationButtonClicked;
        _leftRow.Add(picturesButton);
    }

    void InitializeVideosButton()
    {
        var videosButton = new Button();
        videosButton.Text = "Videos";
        videosButton.TextAlignment = Alignment.Left;
        videosButton.CornerRadius = 0;
        videosButton.Height = BLOCK_HEIGHT;
        videosButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        videosButton.Click = OnLocationButtonClicked;
        _leftRow.Add(videosButton);
    }

    void InitializeDownloadsButton()
    {
        var downloadsButton = new Button();
        downloadsButton.Text = "Downloads";
        downloadsButton.TextAlignment = Alignment.Left;
        downloadsButton.CornerRadius = 0;
        downloadsButton.Height = BLOCK_HEIGHT;
        downloadsButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads";
        downloadsButton.Click = OnLocationButtonClicked;
        _leftRow.Add(downloadsButton);
    }

    void InitializeRightRow()
    {
        _rightRow = new StackedLayout();
        _rightRow.Dock = DockStyle.Fill;
        _rightRow.BackgroundColor = Theme.PrimaryColor;
        _rightRow.DockIndex = 1;
        Controls.Add(_rightRow);
        InitializeActionBar();
        InitializeContentsList();
    }

    void InitializeActionBar()
    {
        _actionBar = new StackedLayout();
        _actionBar.Height = BLOCK_HEIGHT;
        _actionBar.Dock = DockStyle.Top;
        _actionBar.DockIndex = 0;
        _actionBar.Orientation = Orientation.Horizontal;
        _rightRow.Add(_actionBar);
        InitializeLocationInput();
    }

    void InitializeLocationInput()
    {
        _locationInput = new Input();
        _locationInput.Dock = DockStyle.Fill;
        _locationInput.Height = BLOCK_HEIGHT;
        _locationInput.CornerRadius = 16;
        _locationInput.BorderWidth = 2;
        _locationInput.SetMargin(4);
        _actionBar.Add(_locationInput);
    }

    void InitializeContentsList()
    {
        _contentsList = new StackedLayout();
        _contentsList.DockIndex = 1;
        _contentsList.Dock = DockStyle.Fill;
        _contentsList.Scrollable = ScrollableMode.Vertical;
        _rightRow.Add(_contentsList);
    }

    void OnLocationButtonClicked(object? sender, EventArgs e)
    {
        if (sender is not IButton button)
            return;
        var path = button.Tag as string;
        if (string.IsNullOrEmpty(path))
            return;
        LoadDirectoryContents(path);
    }

    void LoadDirectoryContents(string path)
    {
        _locationInput.Placeholder = path;
        _contentsList.Clear();
        _contentsList.Viewport = Rectangle.Empty with
        {
            Width = _contentsList.Viewport.Width,
            Height = _contentsList.Viewport.Height,
        };

        try
        {
            var entries = Directory.GetFileSystemEntries(path);
            Dictionary<string, int> entriesList = [];
            foreach (var entry in entries)
            {
                var isFile = File.Exists(entry);
                var isDir = Directory.Exists(entry);
                if (!isFile && !isDir)
                    continue;
                entriesList.Add(entry, isDir ? 0 : 1);
            }
            entriesList = entriesList
                .OrderBy(kv => kv.Value) // directories first
                .ThenBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            foreach (var kv in entriesList)
            {
                var entry = kv.Key;
                var isDir = kv.Value == 0;
                var entryButton = new Button();
                entryButton.Text = Path.GetFileName(entry);
                entryButton.Tag = entry;
                entryButton.TextAlignment = Alignment.Left;
                entryButton.Height = BLOCK_HEIGHT;
                entryButton.BackgroundColor = isDir ? ColorRgba.Orange.Darkened : Theme.PrimaryColor;
                entryButton.Style = RenderStyle.Contained;
                entryButton.Click = (s, _) =>
                {
                    var tag = (s as IButton)?.Tag as string;
                    if (entriesList[tag!] == 0)
                    {
                        LoadDirectoryContents(entry);
                    }
                    else
                    {
                        OnFilesSelected([entry]);
                    }
                };
                _contentsList.Add(entryButton);
            }
        }
        catch (Exception ex)
        {
            Notifications.Show("Error loading directory", ex.Message);
        }
    }

    void OnFilesSelected(string[] selectedFiles)
    {
        FilesSelected?.Invoke(this, new FilesSelectedEventArgs(selectedFiles));
    }
}
