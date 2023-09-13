public class TestPageModel
{
    public readonly string TestMessage;

    public TestPageModel(TestPageLifecycle.NetworkParameter parameter)
    {
        TestMessage = parameter.Message;
    }
}