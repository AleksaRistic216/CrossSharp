using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class DropdownFactory : IDropdownFactory
{
    public IDropdown Create() => new Dropdown();
}
