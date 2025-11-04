using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class ButtonFactory : IButtonFactory
{
    public IButton Create() => new Button();
}
