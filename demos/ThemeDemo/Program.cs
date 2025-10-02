using CrossSharp.Application;
using ThemeDemo;

Builder builder = new(
    new BaseConfiguration() { ApplicationName = "MyApp", CompanyName = "MyCompany" }
);

builder.SetTheme(new MyCustomTheme());
builder.Run<MainForm>();
