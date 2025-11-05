using CrossSharp.Application;
using CrossSharp.Utils.Enums;
using Demos.AllInOne;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.AllInOne",
    CompanyName = "CrossSharp",
    FormsStyle = FormStyle.CrossSharp,
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
