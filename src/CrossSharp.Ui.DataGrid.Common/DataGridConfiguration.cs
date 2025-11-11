using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

public class DataGridConfiguration : IDataGridConfiguration
{
    public IDataGridColumnConfigurationCollection Columns { get; } = new DataGridColumnConfigurationCollection();
}
