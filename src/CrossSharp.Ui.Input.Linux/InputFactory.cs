using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class InputFactory : IInputFactory
{
    public IInput Create()
    {
        var input = new Input();
        input.Initialize();
        return input;
    }
}
