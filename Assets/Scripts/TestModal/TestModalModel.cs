public class TestModalModel
{
    public readonly string TestMessage;

    public TestModalModel(TestModalLifecycle.NetworkParameter parameter)
    {
        TestMessage = parameter.Message;
    }
}