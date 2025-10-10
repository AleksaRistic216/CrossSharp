using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

public static class ControlsHelpers
{
    public static IForm? GetForm(this IControl control)
    {
        return control.Parent switch
        {
            IForm form => form,
            IControl parentControl => parentControl.GetForm(),
            _ => null,
        };
    }
}
