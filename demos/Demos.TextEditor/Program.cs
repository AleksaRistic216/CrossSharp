using CrossSharp.Application;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using Demos.TextEditor;
using Material.Icons;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.TextEditor",
    CompanyName = "CrossSharp",
    FormsStyle = FormStyle.CrossSharp,
};
var builder = new ApplicationBuilder(configuration);
builder.SetTheme(new CustomTheme());
LoadImages();
builder.Run<MainForm>();

return;

void LoadImages()
{
    var imagesCache = Services.GetSingleton<IEfficientImagesCache>();
    imagesCache.AddImage(
        nameof(Constants.AddFileIconKind),
        ImageHelpers.FromSvgPath(
            MaterialIconDataProvider.GetData(Constants.AddFileIconKind),
            Constants.IMAGE_SIZE,
            Constants.IMAGE_SIZE
        )
    );
    imagesCache.AddImage(
        nameof(Constants.OpenFileIconKind),
        ImageHelpers.FromSvgPath(
            MaterialIconDataProvider.GetData(Constants.OpenFileIconKind),
            Constants.IMAGE_SIZE,
            Constants.IMAGE_SIZE
        )
    );
    imagesCache.AddImage(
        nameof(Constants.SaveFileIconKind),
        ImageHelpers.FromSvgPath(
            MaterialIconDataProvider.GetData(Constants.SaveFileIconKind),
            Constants.IMAGE_SIZE,
            Constants.IMAGE_SIZE
        )
    );
}
