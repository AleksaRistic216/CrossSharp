namespace CrossSharp.Utils.Interfaces;

public interface IWindowContainerProvider
{
    int Column { get; set; }
    int Row { get; set; }
    IWindowContainer Controls { get; }
}
