using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class LabelFactory : ILabelFactory
{
    public ILabel Create()
    {
        var label = new Label();
        return label;
    }
}
