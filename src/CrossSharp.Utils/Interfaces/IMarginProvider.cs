using CrossSharp.Utils.Structs;

namespace CrossSharp.Utils.Interfaces;

public interface IMarginProvider
{
    Margin Margin { get; set; }
    EventHandler? MarginChanged { get; set; }
}
