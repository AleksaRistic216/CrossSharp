using Android.App;
using Android.Runtime;
using CrossSharp.Utils;
using CrossSharp.Utils.Helpers;
using System.Reflection;

namespace CrossSharp.Application.Android;

/// <summary>
/// Base Application class for CrossSharp Android applications.
/// This class is automatically registered as the Application class and ensures
/// that Program.Main() is called during app initialization, which sets up
/// the DI container and registers the main form type before any Activity starts.
/// </summary>
[Application]
public class CrossSharpApplication : global::Android.App.Application
{
    public CrossSharpApplication(IntPtr handle, JniHandleOwnership ownerShip)
        : base(handle, ownerShip)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();

        Debug.Log(LogCategory.App, "CrossSharpApplication.OnCreate - Initializing app");

        // Find and invoke the entry point (Program.Main) to register the form type
        if (!AndroidFormRegistry.IsRegistered)
        {
            Debug.Log(LogCategory.App, "Form not registered, searching for Program.Main entry point");
            InvokeEntryPoint();
        }

        if (AndroidFormRegistry.IsRegistered)
        {
            Debug.Log(LogCategory.App, $"Main form registered: {AndroidFormRegistry.MainFormType?.Name ?? "via factory"}");
        }
        else
        {
            Debug.LogError("Failed to register main form. Make sure Program.Main() calls ApplicationBuilder.Run<T>()");
        }
    }

    private void InvokeEntryPoint()
    {
        try
        {
            // Search all assemblies for a Program class with Main method
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Skip system assemblies
                var name = assembly.GetName().Name;
                if (name == null ||
                    name.StartsWith("System") ||
                    name.StartsWith("Microsoft") ||
                    name.StartsWith("Mono") ||
                    name.StartsWith("Java") ||
                    name.StartsWith("Android") ||
                    name.StartsWith("SkiaSharp") ||
                    name.StartsWith("CrossSharp.Application") ||
                    name.StartsWith("CrossSharp.Utils") ||
                    name.StartsWith("CrossSharp.Ui") ||
                    name.StartsWith("CrossSharp.Themes") ||
                    name.StartsWith("CrossSharp.Icons") ||
                    name.StartsWith("CrossSharp.Dynamic"))
                    continue;

                Debug.Log(LogCategory.App, $"Searching assembly: {name}");

                var programType = assembly.GetType($"{name}.Program")
                                  ?? assembly.GetTypes().FirstOrDefault(t => t.Name == "Program");

                if (programType != null)
                {
                    var mainMethod = programType.GetMethod("Main",
                        BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    if (mainMethod != null)
                    {
                        Debug.Log(LogCategory.App, $"Found entry point: {programType.FullName}.Main");

                        var parameters = mainMethod.GetParameters();
                        if (parameters.Length == 0)
                        {
                            mainMethod.Invoke(null, null);
                        }
                        else if (parameters.Length == 1 && parameters[0].ParameterType == typeof(string[]))
                        {
                            mainMethod.Invoke(null, new object[] { Array.Empty<string>() });
                        }

                        Debug.Log(LogCategory.App, "Entry point invoked successfully");
                        return;
                    }
                }
            }

            Debug.LogError("Could not find Program.Main() entry point in any loaded assembly");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error invoking entry point: {ex.Message}");
            if (ex.InnerException != null)
            {
                Debug.LogError($"Inner exception: {ex.InnerException.Message}");
            }
        }
    }
}
