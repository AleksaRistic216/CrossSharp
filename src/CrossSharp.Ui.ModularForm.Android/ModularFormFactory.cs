using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class ModularFormFactory : IModularFormFactory
{
    public IForm Create()
    {
        var form = new ModularForm();
        form.Initialize();
        return form;
    }
}
