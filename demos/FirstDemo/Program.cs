using CrossSharp.Application;
using CrossSharp.Themes;
using CrossSharp.Utils.DI;
using FirstDemo;

Builder builder = new(
    new BaseConfiguration() { ApplicationName = "MyApp", CompanyName = "MyCompany" }
);

// builder.EnableDevelopersMode();
// builder.SetTheme(some custom theme);
builder.Run<MainForm>();
