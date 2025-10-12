using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Structs;

namespace Demos.Button;

public class MainForm : Form
{
    StackedLayout _stackedLayout = new StackedLayout();
    int _buttonHeight = 50;

    public MainForm()
    {
        InitializeStackedLayout();
        InitializeContainedButtons();
        InitializeFlatButton();
        InitializeOutlinedButton();
        InitializeDefaultButton();
        InitializeLeftAlignedButton();
        InitializeRightAlignedButton();
    }

    void InitializeRightAlignedButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Text = "I am right aligned button";
        button.TextAlignment = Alignment.Right;
        _stackedLayout.Add(button);
    }

    void InitializeLeftAlignedButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Text = "I am left aligned button";
        button.TextAlignment = Alignment.Left;
        _stackedLayout.Add(button);
    }

    void InitializeStackedLayout()
    {
        _stackedLayout.Dock = DockPosition.Fill;
        _stackedLayout.ItemsSpacing = 10;
        _stackedLayout.Padding = new Padding(10);
        Controls.Add(_stackedLayout);
    }

    void InitializeFlatButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Flat;
        button.Text = "I am flat button";
        _stackedLayout.Add(button);
    }

    void InitializeContainedButtons()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Contained;
        button.Text = "I am contained button";
        _stackedLayout.Add(button);
    }

    void InitializeOutlinedButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Outlined;
        button.Text = "I am outlined button";
        _stackedLayout.Add(button);
    }

    void InitializeDefaultButton()
    {
        var button = new CrossSharp.Ui.Button();
        button.Height = _buttonHeight;
        button.Style = RenderStyle.Default;
        button.Text = "I am default button (I follow theme style - flat)";
        _stackedLayout.Add(button);
    }
}
