using System.Drawing;
using CrossSharp.Ui;
namespace FirstDemo;

public class MainForm : Form {
    public MainForm() {
        this.Width = 800;
        this.Height = 800;
        this.OnShow += MainForm_OnShow;
    }
    void MainForm_OnShow(object? sender, EventArgs e) {
        var panel = new PanelControl() {
            Width = 100,
            Height = 100,
            Location = new Point(50, 50)
        };
        this.Controls.Add(panel);
        Task.Run(() => {
            Thread.Sleep(2000);
            panel.Location = new Point(80, 80);
        });
    }
}