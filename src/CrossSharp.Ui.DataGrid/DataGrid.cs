using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class DataGrid() : CrossControl<IDataGrid>(Services.GetSingleton<IDataGridFactory>().Create()), IDataGrid { }
