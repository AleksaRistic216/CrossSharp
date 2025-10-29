using CrossSharp.Utils.Interfaces;
using Tests.Common;

namespace Tests.Demo.Utils;

public abstract class BaseDemosTest<TMainForm> : ApplicationTestBase<TMainForm>
    where TMainForm : IForm;
