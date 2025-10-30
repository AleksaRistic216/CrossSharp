namespace CrossSharp.Utils.Interfaces;

public interface IMarginProvider
{
    int MarginTop { get; set; }
    int MarginBottom { get; set; }
    int MarginLeft { get; set; }
    int MarginRight { get; set; }
    EventHandler? MarginChanged { get; set; }
}
