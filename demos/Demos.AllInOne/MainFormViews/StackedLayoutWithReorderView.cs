using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne.MainFormViews;

public sealed class StackedLayoutWithReorderView : StackedLayout
{
    const int LABEL_HEIGHT = 24;
    const int ITEM_HEIGHT = 50;

    readonly Label _statusLabel;

    public StackedLayoutWithReorderView()
    {
        Dock = DockStyle.Fill;
        Scrollable = ScrollableMode.Vertical;

        var titleLabel = new Label { Text = "Drag-and-Drop Reordering Demo", Height = LABEL_HEIGHT };
        Add(titleLabel);

        var instructionLabel = new Label
        {
            Text = "Drag the grabber handles on the left to reorder items:",
            Height = LABEL_HEIGHT,
        };
        Add(instructionLabel);

        // Create a reorderable stacked layout
        var reorderableLayout = new StackedLayout
        {
            ReorderEnabled = true,
            GrabberSize = 28,
            Height = 350,
            Orientation = Orientation.Vertical,
        };

        // Add colorful task items
        var tasks = new[]
        {
            (ColorRgba.Crimson, "Task 1: Review pull requests"),
            (ColorRgba.ForestGreen, "Task 2: Write documentation"),
            (ColorRgba.DodgerBlue, "Task 3: Fix reported bugs"),
            (ColorRgba.DarkOrange, "Task 4: Update dependencies"),
            (ColorRgba.MediumPurple, "Task 5: Deploy to staging"),
        };

        for (var i = 0; i < tasks.Length; i++)
        {
            var (color, text) = tasks[i];
            var item = new Button
            {
                Text = text,
                Height = ITEM_HEIGHT,
                Index = i,
                BackgroundColor = color,
            };
            reorderableLayout.Add(item);
        }

        // Listen for reorder events
        reorderableLayout.ControlsReordered += (_, e) =>
        {
            if (_statusLabel is not null)
                _statusLabel.Text = $"Moved item from position {e.OldIndex + 1} to position {e.NewIndex + 1}";
        };

        Add(reorderableLayout);

        // Status label to show reorder feedback
        _statusLabel = new Label { Text = "Drag items to reorder them", Height = LABEL_HEIGHT };
        Add(_statusLabel);

        // Add horizontal reorder demo
        var horizontalLabel = new Label
        {
            Text = "Horizontal layout (drag grabbers above items):",
            Height = LABEL_HEIGHT,
        };
        Add(horizontalLabel);

        var horizontalLayout = new StackedLayout
        {
            ReorderEnabled = true,
            GrabberSize = 20,
            Height = 80,
            Orientation = Orientation.Horizontal,
        };

        var horizontalColors = new[] { ColorRgba.Teal, ColorRgba.Coral, ColorRgba.SlateBlue, ColorRgba.Gold };
        for (var i = 0; i < horizontalColors.Length; i++)
        {
            var button = new Button
            {
                Text = $"Item {i + 1}",
                BackgroundColor = horizontalColors[i],
                Width = 80,
                Index = i,
            };
            horizontalLayout.Add(button);
        }

        Add(horizontalLayout);
    }
}
