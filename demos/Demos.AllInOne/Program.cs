using CrossSharp.Application;
using CrossSharp.Icons.Providers;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new BaseConfiguration()
        {
            ApplicationName = "Demos.AllInOne",
            CompanyName = "CrossSharp",
            FormsStyle = FormStyle.CrossSharp,
        };
        var builder = new ApplicationBuilder(configuration);
        builder.SetIconProvider(new CrossSharp2026IconProvider());
        builder.Run<MainForm>();
    }
}
