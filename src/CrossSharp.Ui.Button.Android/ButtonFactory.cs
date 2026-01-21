using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class ButtonFactory : IButtonFactory
{
    public IButton Create() => new Button();
}
