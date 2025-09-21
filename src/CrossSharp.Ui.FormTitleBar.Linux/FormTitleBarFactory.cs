using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FormTitleBarFactory : IFormTitleBarFactory
{
    public IFormTitleBar Create(IForm form) => new FormTitleBar(form);
}
