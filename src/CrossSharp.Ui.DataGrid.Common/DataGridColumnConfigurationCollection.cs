using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

public class DataGridColumnConfigurationCollection : IDataGridColumnConfigurationCollection
{
    private readonly Dictionary<string, IDataGridColumnConfiguration> _columns = new();

    public IDataGridColumnConfiguration this[string columnName]
    {
        get
        {
            if (_columns.TryGetValue(columnName, out IDataGridColumnConfiguration? value))
                return value;
            value = new DataGridColumnConfiguration();
            _columns[columnName] = value;
            return value;
        }
    }
}
