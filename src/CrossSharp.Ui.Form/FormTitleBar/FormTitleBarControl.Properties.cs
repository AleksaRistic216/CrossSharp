using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl
{
    #region private
    TitleBarType _type = TitleBarType.CrossSharp;
    #endregion
    public TitleBarType Type
    {
        get => _type;
        set
        {
            if (_type == value)
                return;
            _type = value;
            RaiseTypeChanged();
        }
    }
    public IntPtr Handle { get; set; }
    public IntPtr ParentHandle { get; set; }
    public bool Visible { get; set; }
    public Color BackgroundColor { get; set; } = Color.Green;
}
