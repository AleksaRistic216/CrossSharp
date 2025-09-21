using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class FormTitleBar : IFormTitleBar
{
    public FormTitleBar(IForm form)
    {
        _form = form;
        SubscribeToInputEvents();
        InitializeMainPanel();
        InitializeWindowButtons();
        form.OnSizeChanged += (s, e) =>
        {
            Width = e.Width;
            Invalidate();
        };
        Invalidate();
    }

    void InitializeMainPanel()
    {
        _mainPanel = new Panel
        {
            ParentHandle = _form.Controls.Handle,
            Parent = _form,
            BackgroundColor = ColorRgba.Gray,
            Width = _form.Width,
            Height = _height,
        };
        _mainPanel.Initialize();
    }

    void InitializeWindowTitle() { }

    void InitializeWindowButtons()
    {
        _closeButton = new Button
        {
            ParentHandle = _form.Controls.Handle,
            Parent = _form,
            BackgroundColor = ColorRgba.Red,
            Width = _applicationButtonWidth,
            Height = _height,
            OnClick = (s, e) =>
            {
                _form.Close();
            },
        };
        _closeButton.Initialize();
    }

    public void Show()
    {
        _mainPanel.Show();
        _closeButton.Show();
    }

    void Invalidate()
    {
        IRelativeHandle parentHandle = _form;
        GtkHelpers.gtk_window_set_decorated(parentHandle.Handle, false);
        _closeButton.Location = new Point(Width - _applicationButtonWidth, 0);
    }
}
