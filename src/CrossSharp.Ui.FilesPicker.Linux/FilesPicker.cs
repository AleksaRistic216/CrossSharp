using CrossSharp.Desktop;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FilesPicker : Form, IFilesPicker
{
    StackedLayout _leftRow;
    StackedLayout _rightRow;
    StackedLayout _actionBar;
    StackedLayout _contentsList;
    Input _locationInput;
    ITheme Theme => Services.GetSingleton<ITheme>();

    const int _leftRowWidth = 150;

    public FilesPicker()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitializeLeftRow();
        InitializeRightRow();
    }

    void InitializeLeftRow()
    {
        _leftRow = new StackedLayout();
        _leftRow.Dock = DockStyle.Left;
        _leftRow.BackgroundColor = Theme.BackgroundColor.Darkened;
        _leftRow.Width = _leftRowWidth;
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
        rootButton.Height = 40;
        rootButton.Tag = "/";
        rootButton.Click = OnLocationButtonClicked;
        _leftRow.Add(rootButton);
    }

    void InitializeHomeButton()
    {
        var homeButton = new Button();
        homeButton.Text = "Home (~)";
        homeButton.TextAlignment = Alignment.Left;
        homeButton.Height = 40;
        homeButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        homeButton.Click = OnLocationButtonClicked;
        _leftRow.Add(homeButton);
    }

    void InitializeDocumentsButton()
    {
        var documentsButton = new Button();
        documentsButton.Text = "Documents";
        documentsButton.TextAlignment = Alignment.Left;
        documentsButton.Height = 40;
        documentsButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        documentsButton.Click = OnLocationButtonClicked;
        _leftRow.Add(documentsButton);
    }

    void InitializeMusicButton()
    {
        var musicButton = new Button();
        musicButton.Text = "Music";
        musicButton.TextAlignment = Alignment.Left;
        musicButton.Height = 40;
        musicButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        musicButton.Click = OnLocationButtonClicked;
        _leftRow.Add(musicButton);
    }

    void InitializePicturesButton()
    {
        var picturesButton = new Button();
        picturesButton.Text = "Pictures";
        picturesButton.TextAlignment = Alignment.Left;
        picturesButton.Height = 40;
        picturesButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        picturesButton.Click = OnLocationButtonClicked;
        _leftRow.Add(picturesButton);
    }

    void InitializeVideosButton()
    {
        var videosButton = new Button();
        videosButton.Text = "Videos";
        videosButton.TextAlignment = Alignment.Left;
        videosButton.Height = 40;
        videosButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        videosButton.Click = OnLocationButtonClicked;
        _leftRow.Add(videosButton);
    }

    void InitializeDownloadsButton()
    {
        var downloadsButton = new Button();
        downloadsButton.Text = "Downloads";
        downloadsButton.TextAlignment = Alignment.Left;
        downloadsButton.Height = 40;
        downloadsButton.Tag = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        downloadsButton.Click = OnLocationButtonClicked;
        _leftRow.Add(downloadsButton);
    }

    void InitializeRightRow()
    {
        _rightRow = new StackedLayout();
        _rightRow.Dock = DockStyle.Fill;
        _rightRow.BackgroundColor = Theme.BackgroundColor;
        _rightRow.DockIndex = 1;
        Controls.Add(_rightRow);
        InitializeActionBar();
        InitializeContentsList();
    }

    void InitializeActionBar()
    {
        _actionBar = new StackedLayout();
        _actionBar.Height = 40;
        _actionBar.Dock = DockStyle.Top;
        _actionBar.DockIndex = 0;
        _actionBar.Direction = Direction.Horizontal;
        _rightRow.Add(_actionBar);

        InitializeLocationInput();
    }

    void InitializeLocationInput()
    {
        _locationInput = new Input();
        // _locationInput.PlaceholderText = "Location";
        _locationInput.Dock = DockStyle.Fill;
        _locationInput.Height = 40;
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
        _contentsList.Clear();

        try
        {
            var entries = Directory.GetFileSystemEntries(path);
            foreach (var entry in entries)
            {
                var entryButton = new Button();
                entryButton.Text = Path.GetFileName(entry);
                entryButton.TextAlignment = Alignment.Left;
                entryButton.Height = 30;
                entryButton.Click = (s, e) =>
                {
                    Notifications.Show("Clicked on", entry);
                };
                _contentsList.Add(entryButton);
            }
        }
        catch (Exception ex)
        {
            Notifications.Show("Error loading directory", ex.Message);
        }
    }
}
