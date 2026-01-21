using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

/// <summary>
/// Static registry for storing the main form type on Android.
/// On Android, Program.cs runs before the Activity is created.
/// The ApplicationBuilder stores the form type here, and the framework's
/// MainActivity retrieves it to create the UI.
/// </summary>
public static class AndroidFormRegistry
{
    /// <summary>
    /// The type of the main form to create on Android.
    /// </summary>
    public static Type? MainFormType { get; set; }

    /// <summary>
    /// Factory function to create the main form instance.
    /// </summary>
    public static Func<IForm>? MainFormFactory { get; set; }

    /// <summary>
    /// Indicates whether the Android main form has been registered.
    /// </summary>
    public static bool IsRegistered => MainFormType != null || MainFormFactory != null;

    /// <summary>
    /// Creates an instance of the main form.
    /// </summary>
    public static IForm? CreateMainForm()
    {
        if (MainFormFactory != null)
            return MainFormFactory();

        if (MainFormType != null)
            return Activator.CreateInstance(MainFormType) as IForm;

        return null;
    }
}
