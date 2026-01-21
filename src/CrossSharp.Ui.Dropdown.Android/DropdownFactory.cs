using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class DropdownFactory : IDropdownFactory
{
    public IDropdown Create() => new Dropdown();
}
