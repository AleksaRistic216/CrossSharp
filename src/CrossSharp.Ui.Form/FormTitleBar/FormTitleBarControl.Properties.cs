using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl
{
    #region private
    TitleBarType _type = TitleBarType.CrossSharp;
    PanelControl _panelControl;
    ITitleBarProvider _titleBarProvider;
    int _height = 36;
    #endregion

    #region exposed
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
    public int Height { get; set; }
    #endregion
}
