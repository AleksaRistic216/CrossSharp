using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Dropdown() : CrossControl<IDropdown>(Services.GetSingleton<IDropdownFactory>().Create()), IDropdown;
