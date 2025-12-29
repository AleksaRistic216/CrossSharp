namespace CrossSharp.Utils.Attributes;

/// <summary>
/// Used to mark class as control.
/// It is used by designer toolbox when scanning for controls
/// </summary>
/// <param name="name"></param>
public class ControlAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}
