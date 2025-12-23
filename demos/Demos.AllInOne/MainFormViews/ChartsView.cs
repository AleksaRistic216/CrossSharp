using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne.MainFormViews;

public class ChartsView : StaticLayout
{
    public ChartsView()
    {
        Dock = DockStyle.Fill;
        InitializeChart();
    }

    void InitializeChart()
    {
        var chart = new Chart();
        chart.Dock = DockStyle.Fill;
        chart.BackgroundColor = ColorRgba.Red;
        Add(chart);
    }
}
