using System.Drawing;
using System.Reflection.Emit;
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
        InitializeWindowTitle();
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

    void InitializeWindowTitle()
    {
        _titleLabel = new Label()
        {
            ParentHandle = _form.Controls.Handle,
            Text = _form.Title,
            Parent = _form,
        };
        _titleLabel.Initialize();
    }

    void InitializeWindowButtons()
    {
        _maximizeButton = new FormTitleBarMaximizeButton
        {
            ParentHandle = _form.Controls.Handle,
            Parent = _form,
            BackgroundColor = ColorRgba.Pink,
            Width = _applicationButtonWidth,
            Height = _height,
            Text = "M",
            OnClick = (s, e) => { },
        };
        _maximizeButton.Initialize();
        _closeButton = new Button
        {
            ParentHandle = _form.Controls.Handle,
            Parent = _form,
            BackgroundColor = ColorRgba.Red,
            Width = _applicationButtonWidth,
            Height = _height,
            Text = "X",
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
        _maximizeButton?.Show();
        _closeButton?.Show();
        _titleLabel?.Show();
    }

    void Invalidate()
    {
        IRelativeHandle parentHandle = _form;
        GtkHelpers.gtk_window_set_decorated(parentHandle.Handle, false);
        InvalidateMaximizeButton();
        InvalidateCloseButton();
        InvalidateTitleLabel();
    }

    void InvalidateMaximizeButton()
    {
        if (_maximizeButton is null)
            return;
        var desiredWidth = Width - 2 * _applicationButtonWidth;
        _maximizeButton.Location = new Point(Math.Max(desiredWidth, 0), 0);
    }

    void InvalidateCloseButton()
    {
        if (_closeButton is null)
            return;
        var desiredWidth = Width - _applicationButtonWidth;
        _closeButton.Location = new Point(Math.Max(desiredWidth, 0), 0);
    }

    void InvalidateTitleLabel()
    {
        if (_titleLabel is null)
            return;
        var titleLabelSize = ((ICenterPanelChild)_titleLabel).GetSize();
        _titleLabel.Location = new Point(
            (_width - titleLabelSize.Width) / 2,
            (_height - titleLabelSize.Height) / 2
        );
    }
}
