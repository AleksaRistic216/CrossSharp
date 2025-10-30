using CrossSharp.Desktop;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FilesPicker : Form, IFilesPicker
{
    StackedLayout _leftRow;
    StackedLayout _rightRow;
    ITheme Theme => Services.GetSingleton<ITheme>();
    HashSet<string> _drives = [];

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
        rootButton.Click = (s, e) =>
        {
            Notifications.Show("Going to location", "/");
        };
        _leftRow.Add(rootButton);
    }

    void InitializeHomeButton()
    {
        var homeButton = new Button();
        homeButton.Text = "Home (~)";
        homeButton.TextAlignment = Alignment.Left;
        homeButton.Height = 40;
        homeButton.Click = (s, e) =>
        {
            Notifications.Show(
                "Going to location",
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            );
        };
        _leftRow.Add(homeButton);
    }

    void InitializeDocumentsButton()
    {
        var documentsButton = new Button();
        documentsButton.Text = "Documents";
        documentsButton.TextAlignment = Alignment.Left;
        documentsButton.Height = 40;
        documentsButton.Click = (s, e) =>
        {
            Notifications.Show(
                "Going to location",
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            );
        };
        _leftRow.Add(documentsButton);
    }

    void InitializeMusicButton()
    {
        var musicButton = new Button();
        musicButton.Text = "Music";
        musicButton.TextAlignment = Alignment.Left;
        musicButton.Height = 40;
        musicButton.Click = (s, e) =>
        {
            Notifications.Show(
                "Going to location",
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
            );
        };
        _leftRow.Add(musicButton);
    }

    void InitializePicturesButton()
    {
        var picturesButton = new Button();
        picturesButton.Text = "Pictures";
        picturesButton.TextAlignment = Alignment.Left;
        picturesButton.Height = 40;
        picturesButton.Click = (s, e) =>
        {
            Notifications.Show(
                "Going to location",
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            );
        };
        _leftRow.Add(picturesButton);
    }

    void InitializeVideosButton()
    {
        var videosButton = new Button();
        videosButton.Text = "Videos";
        videosButton.TextAlignment = Alignment.Left;
        videosButton.Height = 40;
        videosButton.Click = (s, e) =>
        {
            Notifications.Show(
                "Going to location",
                Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
            );
        };
        _leftRow.Add(videosButton);
    }

    void InitializeDownloadsButton()
    {
        var downloadsButton = new Button();
        downloadsButton.Text = "Downloads";
        downloadsButton.TextAlignment = Alignment.Left;
        downloadsButton.Height = 40;
        downloadsButton.Click = (s, e) =>
        {
            Notifications.Show(
                "Going to location",
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads"
            );
        };
        _leftRow.Add(downloadsButton);
    }

    void InitializeRightRow()
    {
        _rightRow = new StackedLayout();
        _rightRow.Dock = DockStyle.Fill;
        _rightRow.BackgroundColor = Theme.BackgroundColor;
        _rightRow.DockIndex = 1;
        Controls.Add(_rightRow);
    }
}
