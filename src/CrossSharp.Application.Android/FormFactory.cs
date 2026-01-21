using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application.Android;

/// <summary>
/// Factory for creating Android-specific Form instances.
/// </summary>
internal class FormFactory : IFormFactory
{
    public IForm Create()
    {
        var form = new AndroidForm();
        form.Initialize();
        return form;
    }
}
