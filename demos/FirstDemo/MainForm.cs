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
            Height = 100
        };
        this.Controls.Add(panel);
    }
}