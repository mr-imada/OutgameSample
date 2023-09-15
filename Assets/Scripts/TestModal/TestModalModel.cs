public class TestModalModel
{
    public readonly string TestMessage;

    public TestModalModel(TestModalLifecycle.CountParameter parameter)
    {
        TestMessage = $"Test Modal {parameter.ModalCount}";
    }
}