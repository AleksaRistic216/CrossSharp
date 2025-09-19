using System.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl : IBoundsProvider, ITitleBar, IMouseTargetable
{
    public FormTitleBarControl(IForm form)
    {
        InitializeInputHandler();
        SubscribeToInputEvents();
        _titleBarProvider = form;
        _mainPanel = new PanelControl
        {
            ParentHandle = form.Controls.Handle,
            Parent = form.Controls.Handle,
            BackgroundColor = Color.Gray,
            Width = form.Width,
            Height = _height,
        };
        _mainPanel.Initialize();
        _applicationButtonsPanel = new PanelControl()
        {
            ParentHandle = form.Controls.Handle,
            Parent = form.Controls.Handle,
            BackgroundColor = Color.Orange,
            Width = 150,
            Height = _height,
            Location = new Point(form.Width - 150, 0),
        };
        _applicationButtonsPanel.Initialize();
        form.OnSizeChanged += (s, e) =>
        {
            Width = e.Width;
            _applicationButtonsPanel.Location = new Point(e.Width - 150, 0);
            Invalidate();
        };
        Invalidate();
    }

    public void Show()
    {
        _mainPanel.Show();
        _applicationButtonsPanel.Show();
    }

    void Invalidate()
    {
        GtkHelpers.gtk_window_set_decorated(_titleBarProvider.Handle, false);
    }
}
