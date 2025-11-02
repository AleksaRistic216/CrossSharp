using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;
using Material.Icons;

namespace Demos.Button;

public class MainForm : Form
{
    StackedLayout _stackedLayout = new StackedLayout();
    int _buttonHeight = 50;
    int imageSize = 128;

    public MainForm()
    {
        InitializeStackedLayout();
        InitializeContainedButtons();
        InitializeFlatButton();
        InitializeOutlinedButton();
        InitializeDefaultButton();
        InitializeLeftAlignedButton();
        InitializeRightAlignedButton();

        LoadImages();

        InitializeButtonWithBeforeImage();
        InitializeButtonWithAfterImage();
        InitializeButtonWithImageWithoutText();
        InitializeButtonLeftAlignedWithImageBefore();
        InitializeButtonRightAlignedWithImageBefore();
        InitializeButtonLeftAlignedWithImageAfter();
        InitializeButtonRightAlignedWithImageAfter();
    }

    void LoadImages()
    {
        var imagesCache = Services.GetSingleton<IEfficientImagesCache>();
        imagesCache.AddImage(
            nameof(MaterialIconKind.About),
            ImageHelpers.FromSvgPath(
                MaterialIconDataProvider.GetData(MaterialIconKind.About),
                imageSize,
                imageSize
            )
        );
        imagesCache.AddImage(
            nameof(MaterialIconKind.Home),
            ImageHelpers.FromSvgPath(
                MaterialIconDataProvider.GetData(MaterialIconKind.Home),
                imageSize,
                imageSize
            )
        );
        imagesCache.AddImage(
            nameof(MaterialIconKind.Person),
            ImageHelpers.FromSvgPath(
                MaterialIconDataProvider.GetData(MaterialIconKind.Person),
                imageSize,
                imageSize
            )
        );
        imagesCache.AddImage(
            nameof(MaterialIconKind.Money),
            ImageHelpers.FromSvgPath(
                MaterialIconDataProvider.GetData(MaterialIconKind.Money),
                imageSize,
                imageSize
            )
        );
        imagesCache.AddImage(
            nameof(MaterialIconKind.City),
            ImageHelpers.FromSvgPath(
                MaterialIconDataProvider.GetData(MaterialIconKind.City),
                imageSize,
                imageSize
            )
        );
    }

    void InitializeButtonLeftAlignedWithImageAfter()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.City));
        button.ImagePlacement = ButtonImagePlacement.AfterText;
        button.Text = "I am left aligned button with image";
        button.TextAlignment = Alignment.Left;
        _stackedLayout.Add(button);
    }

    void InitializeButtonRightAlignedWithImageAfter()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.About));
        button.ImagePlacement = ButtonImagePlacement.AfterText;
        button.Text = "I am right aligned button with image";
        button.TextAlignment = Alignment.Right;
        _stackedLayout.Add(button);
    }

    void InitializeButtonRightAlignedWithImageBefore()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.Money));
        button.Text = "I am right aligned button with image";
        button.TextAlignment = Alignment.Right;
        _stackedLayout.Add(button);
    }

    void InitializeButtonLeftAlignedWithImageBefore()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.ImageScale = new SizeF(0.4f, 0.4f);
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.Person));
        button.Text = "I am left aligned button with image (scaled one)";
        button.TextAlignment = Alignment.Left;
        _stackedLayout.Add(button);
    }

    void InitializeButtonWithImageWithoutText()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.Home));
        _stackedLayout.Add(button);
    }

    void InitializeButtonWithBeforeImage()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.ImageScale = new SizeF(0.5f, 0.5f);
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.Person));
        button.Text = "I have image (scaled one)";
        _stackedLayout.Add(button);
    }

    void InitializeButtonWithAfterImage()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Image = EfficientImage.Get(nameof(MaterialIconKind.About));
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
        _stackedLayout.Dock = DockStyle.Fill;
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
        button.Text = "I am default button (I follow theme style)";
        _stackedLayout.Add(button);
    }
}
