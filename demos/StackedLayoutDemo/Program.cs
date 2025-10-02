using CrossSharp.Application;
using StackedLayoutDemo;

Builder builder = new(
    new BaseConfiguration() { ApplicationName = "MyApp", CompanyName = "MyCompany" }
);

builder.Run<MainForm>();
