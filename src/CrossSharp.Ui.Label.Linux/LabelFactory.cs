using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class LabelFactory : ILabelFactory
{
    public ILabel Create() => new Label();
}
