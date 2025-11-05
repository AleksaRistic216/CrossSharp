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
