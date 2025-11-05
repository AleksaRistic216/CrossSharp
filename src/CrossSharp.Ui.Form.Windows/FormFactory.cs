using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class FormFactory : IFormFactory
{
    public IForm Create()
    {
        var form = new Form();
        form.Initialize();
        return form;
    }
}
