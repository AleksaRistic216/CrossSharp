using CrossSharp.Utils.Gtk;
namespace CrossSharp.Ui;

public partial class Label {
    public override IntPtr Handle { get; } = GtkHelpers.gtk_label_new("asd");
    public float FontSize { get; set; }
    public IntPtr ContainerHandle { get; set; }
}