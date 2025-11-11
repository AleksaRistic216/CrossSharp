namespace CrossSharp.Utils.Interfaces;

public interface IDataGridColumnConfigurationCollection
{
    IDataGridColumnConfiguration this[string columnName] { get; }
}
