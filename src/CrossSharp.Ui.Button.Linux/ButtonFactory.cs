using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class ButtonFactory : IButtonFactory
{
    public IButton Create() => new Ui.Linux.Button();
}
