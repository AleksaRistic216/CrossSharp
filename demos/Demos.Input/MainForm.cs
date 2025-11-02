using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.Input;

public class MainForm : Form
{
    public MainForm()
    {
        var flowLayout = new FlowLayout();
        flowLayout.Dock = DockStyle.Fill;
        flowLayout.ItemsSpacing = 10;
        Controls.Add(flowLayout);

        var singleLineInput = new CrossSharp.Ui.Input();
        singleLineInput.Width = 200;
        singleLineInput.Height = 30;
        flowLayout.Add(singleLineInput);

        var multiLineInput = new CrossSharp.Ui.Input();
        multiLineInput.Width = 200;
        multiLineInput.Height = 100;
        multiLineInput.MultiLine = true;
        flowLayout.Add(multiLineInput);

        var singleLineInputWithRoundedCorners = new CrossSharp.Ui.Input();
        singleLineInputWithRoundedCorners.Width = 200;
        singleLineInputWithRoundedCorners.Height = 30;
        singleLineInputWithRoundedCorners.CornerRadius = 10;
        flowLayout.Add(singleLineInputWithRoundedCorners);

        var multiLineInputWithRoundedCorners = new CrossSharp.Ui.Input();
        multiLineInputWithRoundedCorners.Width = 200;
        multiLineInputWithRoundedCorners.Height = 100;
        multiLineInputWithRoundedCorners.MultiLine = true;
        multiLineInputWithRoundedCorners.CornerRadius = 10;
        flowLayout.Add(multiLineInputWithRoundedCorners);

        var singleLineInputWithPlaceholder = new CrossSharp.Ui.Input();
        singleLineInputWithPlaceholder.Width = 200;
        singleLineInputWithPlaceholder.Height = 30;
        singleLineInputWithPlaceholder.Placeholder = "Enter text here...";
        flowLayout.Add(singleLineInputWithPlaceholder);

        var multiLineInputWithPlaceholder = new CrossSharp.Ui.Input();
        multiLineInputWithPlaceholder.Width = 200;
        multiLineInputWithPlaceholder.Height = 100;
        multiLineInputWithPlaceholder.MultiLine = true;
        multiLineInputWithPlaceholder.Placeholder = "Enter text here...";
        flowLayout.Add(multiLineInputWithPlaceholder);
    }
}
