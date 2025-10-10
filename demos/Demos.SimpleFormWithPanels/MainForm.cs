using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;

namespace Demos.SimpleFormWithPanels;

public class MainForm : Form
{
    public MainForm()
    {
        Width = 800;
        Height = 600;
        var panel = new Panel();
        panel.Width = Width / 2;
        panel.Height = Height / 2;
        panel.Location = new Point(0, 0);
        panel.BackgroundColor = ColorRgba.LightGray;
        Controls.Add(panel);

        var panel1 = new Panel();
        panel1.Width = Width / 2;
        panel1.Height = Height / 2;
        panel1.Location = new Point(panel1.Width, 0);
        panel1.BackgroundColor = ColorRgba.Orange;
        Controls.Add(panel1);

        var panel2 = new Panel();
        panel2.Width = Width / 2;
        panel2.Height = Height / 2;
        panel2.Location = new Point(0, panel2.Height);
        panel2.BackgroundColor = ColorRgba.LightBlue;
        Controls.Add(panel2);

        var panel3 = new Panel();
        panel3.Width = Width / 2;
        panel3.Height = Height / 2;
        panel3.Location = new Point(panel3.Width, panel3.Height);
        panel3.BackgroundColor = ColorRgba.LightGreen;
        Controls.Add(panel3);
    }
}
