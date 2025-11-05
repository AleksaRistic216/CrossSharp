using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class ButtonFactory : IButtonFactory
{
    public IButton Create()
    {
        var button = new Button();
        return button;
    }
}
