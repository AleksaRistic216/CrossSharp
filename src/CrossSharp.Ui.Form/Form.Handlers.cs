using System.Drawing;
using CrossSharp.Utils.Gtk;
namespace CrossSharp.Ui;

public partial class Form {
    public  EventHandler? OnShow { get; set; }
    public  EventHandler? OnClose { get; set; }
    public EventHandler<Size> OnSizeChanged { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }
    void RaiseOnShow() {
        OnShow?.Invoke(this, EventArgs.Empty);
    }
    void RaiseOnClose() {
        OnClose?.Invoke(this, EventArgs.Empty);
    }
    void RaiseSizeChanged(Size newSize) {
        OnSizeChangedInternal(newSize);
        OnSizeChanged?.Invoke(this, newSize);
    }
    void OnSizeChangedInternal(Size newSize) {
        GtkHelpers.gtk_widget_set_size_request(Handle, newSize.Width, newSize.Height);
    }
}