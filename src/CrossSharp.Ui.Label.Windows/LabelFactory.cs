using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class LabelFactory : ILabelFactory
{
    public ILabel Create()
    {
        var label = new Label();
        return label;
    }
}
