using Android.App;
using Android.OS;
using Android.Views;
using CrossSharp.Utils;
using CrossSharp.Utils.Android;
using CrossSharp.Utils.Interfaces;
using Debug = CrossSharp.Utils.Helpers.Debug;
using LogCategory = CrossSharp.Utils.Helpers.LogCategory;

namespace CrossSharp.Application.Android;

/// <summary>
/// Main launcher Activity for CrossSharp Android applications.
/// This Activity is automatically used as the entry point when targeting Android.
/// It reads the form type from the AndroidFormRegistry and creates the UI.
/// </summary>
[Activity(
    MainLauncher = true,
    Exported = true,
    Theme = "@android:style/Theme.Material.Light.NoActionBar")]
public class CrossSharpMainActivity : Activity
{
    private CrossSharpView? _crossSharpView;
    private IForm? _mainForm;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        Debug.Log(LogCategory.App, "CrossSharpMainActivity.OnCreate starting");

        // Create the form from the registry
        _mainForm = AndroidFormRegistry.CreateMainForm();

        if (_mainForm == null)
        {
            Debug.LogError("No main form registered. Make sure ApplicationBuilder.Run<T>() was called in Program.cs");
            Finish();
            return;
        }

        Debug.Log(LogCategory.App, $"Created main form: {_mainForm.GetType().Name}");

        // Create the CrossSharp rendering view
        _crossSharpView = new CrossSharpView(this);

        // Get the controls from the form and set as root
        if (_mainForm.Controls != null)
        {
            // The form's Controls is an IControlsContainer which is also an IControl
            _crossSharpView.RootControl = _mainForm.Controls as IControl;
        }

        // Set the content view
        SetContentView(_crossSharpView);

        Debug.Log(LogCategory.App, "CrossSharpMainActivity.OnCreate completed");
    }

    protected override void OnResume()
    {
        base.OnResume();
        _crossSharpView?.RequestRedraw();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _mainForm?.Dispose();
    }
}
