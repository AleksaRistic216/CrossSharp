using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

public class ButtonFactory : IButtonFactory
{
    public IButton Create() => new Button();
}
