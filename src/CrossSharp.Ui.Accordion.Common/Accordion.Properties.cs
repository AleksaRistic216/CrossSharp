using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Accordion
{
    ITheme Theme => Services.GetSingleton<ITheme>();
}
