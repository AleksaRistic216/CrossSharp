namespace CrossSharp.Utils.Interfaces;

public interface IControlsContainerProvider
{
    int Column { get; set; }
    int Row { get; set; }
    IControlsContainer Controls { get; }
}
