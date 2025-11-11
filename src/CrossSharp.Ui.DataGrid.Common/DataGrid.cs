using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class DataGrid : ControlBase, IDataGrid
{
    public override void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor.Darkened;
        CornerRadius = Services.GetSingleton<ITheme>().DefaultCornerRadius;
        BorderWidth = 1;
        BorderColor = BackgroundColor.Darkened;
    }

    public override void Invalidate() { }
}
