using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace Demos.Button;

public class MainForm : Form
{
    StackedLayout _stackedLayout = new StackedLayout();
    int _buttonHeight = 50;
    string _imageId = "smiley";

    public MainForm()
    {
        InitializeStackedLayout();
        InitializeContainedButtons();
        InitializeFlatButton();
        InitializeOutlinedButton();
        InitializeDefaultButton();
        InitializeLeftAlignedButton();
        InitializeRightAlignedButton();

        var imagesCache = Services.GetSingleton<IEfficientImagesCache>();
        imagesCache.AddImage(_imageId, "Assets/happy.png");

        InitializeButtonWithBeforeImage();
        InitializeButtonWithAfterImage();
        InitializeButtonWithImageWithoutText();
        InitializeButtonLeftAlignedWithImage();
        InitializeButtonRightAlignedWithImage();
    }

    void InitializeButtonRightAlignedWithImage()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(_imageId);
        button.Text = "I am right aligned button with image";
        button.TextAlignment = Alignment.Right;
        _stackedLayout.Add(button);
    }

    void InitializeButtonLeftAlignedWithImage()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(_imageId);
        button.Text = "I am left aligned button with image";
        button.TextAlignment = Alignment.Left;
        _stackedLayout.Add(button);
    }

    void InitializeButtonWithImageWithoutText()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(_imageId);
        _stackedLayout.Add(button);
    }

    void InitializeButtonWithBeforeImage()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(_imageId);
        button.Text = "I have image";
        _stackedLayout.Add(button);
    }

    void InitializeButtonWithAfterImage()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(_imageId);
        button.ImagePlacement = ButtonImagePlacement.AfterText;
        button.Text = "I have image too";
        _stackedLayout.Add(button);
    }

    void InitializeRightAlignedButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Text = "I am right aligned button";
        button.TextAlignment = Alignment.Right;
        _stackedLayout.Add(button);
    }

    void InitializeLeftAlignedButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Text = "I am left aligned button";
        button.TextAlignment = Alignment.Left;
        _stackedLayout.Add(button);
    }

    void InitializeStackedLayout()
    {
        _stackedLayout.Dock = DockPosition.Fill;
        _stackedLayout.Scrollable = ScrollableMode.Vertical;
        _stackedLayout.ItemsSpacing = 10;
        _stackedLayout.Padding = new Padding(10);
        Controls.Add(_stackedLayout);
    }

    void InitializeFlatButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Flat;
        button.Text = "I am flat button";
        _stackedLayout.Add(button);
    }

    void InitializeContainedButtons()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Text = "I am contained button";
        _stackedLayout.Add(button);
    }

    void InitializeOutlinedButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Outlined;
        button.Text = "I am outlined button";
        _stackedLayout.Add(button);
    }

    void InitializeDefaultButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Default;
        button.Text = "I am default button (I follow theme style - flat)";
        _stackedLayout.Add(button);
    }
}
