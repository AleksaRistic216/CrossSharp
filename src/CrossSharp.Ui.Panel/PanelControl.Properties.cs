using System.Drawing;
namespace CrossSharp.Ui;

public partial class PanelControl {
    #region private
    Color _backgroundColor = Color.Transparent;
    #endregion
    #region exposed
    public Color BackgroundColor {
        get => _backgroundColor;
        set {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            RaiseBackgroundColorChanged();
            Redraw();
        }
    }
    #endregion
}