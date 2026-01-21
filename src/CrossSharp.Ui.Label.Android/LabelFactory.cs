using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class LabelFactory : ILabelFactory
{
    public ILabel Create() => new Label();
}
