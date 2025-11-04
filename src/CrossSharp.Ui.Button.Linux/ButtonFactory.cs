using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class ButtonFactory : IButtonFactory
{
    public IButton Create() => new Button();
}
