using CrossSharp.Utils.Interfaces;

namespace Demos.AllInOne.Models;

public class User : IDataGridDataItem
{
    public string Name { get; set; }

    public int Age { get; set; }

    public string Country { get; set; }
}
