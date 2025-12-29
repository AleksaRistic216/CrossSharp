using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Attributes;

/// <summary>
/// Used to mark class as control.
/// It is used by designer toolbox when scanning for controls
/// </summary>
/// <param name="name"></param>
/// <param name="groups"></param>
/// <param name="icon"></param>
public class ControlAttribute(string name, ControlGroup[] groups, ControlIcon icon) : Attribute
{
    public string Name { get; set; } = name;
    public ControlGroup[] Groups { get; set; } = groups;
    public ControlIcon Icon { get; set; } = icon;
}
