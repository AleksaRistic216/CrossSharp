using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class FormLinuxFactory : IFormFactory
{
    public IForm Create() => new FormLinux.FormLinux();
}
