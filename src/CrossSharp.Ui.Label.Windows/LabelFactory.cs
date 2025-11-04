using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class LabelFactory : ILabelFactory
{
    public ILabel Create() => new Label();
}
