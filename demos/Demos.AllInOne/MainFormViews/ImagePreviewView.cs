using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using SkiaSharp;

namespace Demos.AllInOne.MainFormViews;

public class ImagePreviewView : FlowLayout
{
    public ImagePreviewView()
    {
        Dock = DockStyle.Fill;
        InitializeImagePreviews();
    }

    void InitializeImagePreviews()
    {
        var imagePreview = new ImagePreview();
        imagePreview.Width = 100;
        imagePreview.Height = 100;
        imagePreview.Image = EfficientImage.GetIcon(Icon.Home, SKColors.Black);
        imagePreview.BackgroundColor = ColorRgba.Yellow;
        Add(imagePreview);

        var imagePreview1 = new ImagePreview();
        imagePreview1.Width = 100;
        imagePreview1.Height = 100;
        imagePreview1.Image = EfficientImage.GetIcon(Icon.DataGrid, SKColors.Black);
        imagePreview1.BorderColor = ColorRgba.Black;
        imagePreview1.BorderWidth = 2;
        Add(imagePreview1);

        var imagePreview2 = new ImagePreview();
        imagePreview2.Width = 100;
        imagePreview2.Height = 100;
        imagePreview2.Image = EfficientImage.GetIcon(Icon.Home, SKColors.Orange);
        imagePreview2.BorderColor = ColorRgba.Red;
        imagePreview2.BorderWidth = 2;
        Add(imagePreview2);

        var imagePreview3 = new ImagePreview();
        imagePreview3.Width = 100;
        imagePreview3.Height = 100;
        imagePreview3.Image = EfficientImage.GetIcon(Icon.DataGrid, SKColors.Red);
        imagePreview3.BorderColor = ColorRgba.Blue;
        imagePreview3.BorderWidth = 2;
        imagePreview3.CornerRadius = 10;
        Add(imagePreview3);

        var imagePreview4 = new ImagePreview();
        imagePreview4.Width = 100;
        imagePreview4.Height = 100;
        imagePreview4.Image = EfficientImage.GetIcon(Icon.Home, SKColors.Green);
        Add(imagePreview4);
    }
}
