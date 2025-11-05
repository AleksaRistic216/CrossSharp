using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class DropdownFactory : IDropdownFactory
{
    public IDropdown Create() => new Dropdown();
}
