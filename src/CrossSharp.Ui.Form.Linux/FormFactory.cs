using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FormFactory : IFormFactory
{
    public IForm Create()
    {
        var form = new Form();
        form.Initialize();
        return form;
    }
}
