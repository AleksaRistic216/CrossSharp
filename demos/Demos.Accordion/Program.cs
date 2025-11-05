using CrossSharp.Application;
using CrossSharp.Utils.Enums;
using Demos.Accordion;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.Accordion",
    CompanyName = "CrossSharp",
    FormsStyle = FormStyle.CrossSharp,
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
