using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Accordion
{
    ITheme Theme => Services.GetSingleton<ITheme>();
    IStackedLayout _headerArea;
    IStackedLayout _itemsArea;
    IButton _hamburgButton;
    int _lastWidth;
    int _lastHeight;
    Orientation _accordionOrientation = Orientation.Vertical;
    public new Orientation Orientation // new because underlying StackedLayout.Orientation should always be vertical within accordion
    {
        get => _accordionOrientation;
        set => _accordionOrientation = value;
    }
    AccordionState _state = AccordionState.Expanded;
    public AccordionState State
    {
        get => _state;
        set
        {
            if (_state == value)
                return;
            _state = value;
            OnStateChanged();
        }
    }
}
