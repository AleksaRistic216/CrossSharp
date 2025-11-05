using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar : StackedLayout, IMouseTargetable
{
    internal FormSDLTitleBar(FormSDL form)
    {
        Parent = form;
        Orientation = Utils.Enums.Orientation.Horizontal;
        BackgroundColor = Services.GetSingleton<ITheme>().SecondaryBackgroundColor.Darkened;
        Height = 35;
        Width = Form.Width;
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;
        InputHandler.MouseDragged += OnMouseDragged;
        InputHandler.MouseReleased += OnMouseReleased;

        var closeButton = new Button();
        closeButton.Text = "X";
        closeButton.Width = 50;
        closeButton.Height = Height;
        closeButton.BackgroundColor = BackgroundColor.Darkened;
        closeButton.Click += (s, e) =>
        {
            Form.Close();
        };
        Add(closeButton);
    }

    void StartMovingForm()
    {
        _formDragCancellationTokenSource = new CancellationTokenSource();
        _formDragTask = Task.Run(
            () =>
            {
                while (!_formDragCancellationTokenSource.IsCancellationRequested)
                {
                    if (_formDragDestination is null)
                        continue;
                    int targetDelay = (int)(1000f / CoreFps);
                    int timeSinceLastDrag = _lastFormDragTime is null
                        ? int.MaxValue
                        : (int)(DateTime.Now - _lastFormDragTime.Value).TotalMilliseconds;
                    if (timeSinceLastDrag < targetDelay)
                        continue;
                    _lastFormDragTime = DateTime.Now;
                    Form.Move(_formDragDestination.Value);
                }
            },
            _formDragCancellationTokenSource.Token
        );
    }

    public override void Dispose()
    {
        base.Dispose();
        InputHandler.MouseMoved -= OnMouseMoved;
        InputHandler.MousePressed -= OnMousePressed;
        InputHandler.MouseDragged -= OnMouseDragged;
        InputHandler.MouseReleased -= OnMouseReleased;
        _formDragCancellationTokenSource?.Cancel();
        _formDragTask = null;
    }
}
