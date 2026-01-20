using CrossSharp.Application;
using CrossSharp.Icons.Providers;
using CrossSharp.Themes;
using CrossSharp.Utils.Enums;
using FormsApp;

var configuration = new BaseConfiguration
{
    ApplicationName = "FormsApp",
    CompanyName = "YourCompany",
    FormsStyle = FormStyle.CrossSharp
};
var builder = new ApplicationBuilder(configuration);
builder.SetTheme(new DefaultTheme());
builder.SetIconProvider(new CrossSharp2026IconProvider());
builder.Run<MainForm>();
