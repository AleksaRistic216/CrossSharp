using CrossSharp.Ui.Common;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class ModularFormFactory : IModularFormFactory
{
    public IForm Create()
    {
        var form = new ModularForm();
        form.Initialize();
        return form;
    }
}
