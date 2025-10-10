using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class LabelFactory : ILabelFactory
{
    public ILabel Create() => new Label();
}
