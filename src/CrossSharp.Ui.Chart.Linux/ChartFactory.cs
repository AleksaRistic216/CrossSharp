using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class ChartFactory : IChartFactory
{
    public IChart Create()
    {
        var chart = new Chart();
        return chart;
    }
}
