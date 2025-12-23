using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class ChartFactory : IChartFactory
{
    public IChart Create()
    {
        var chart = new Chart();
        return chart;
    }
}
