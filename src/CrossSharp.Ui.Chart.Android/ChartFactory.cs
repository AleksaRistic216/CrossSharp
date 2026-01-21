using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class ChartFactory : IChartFactory
{
    public IChart Create() => new Chart();
}
