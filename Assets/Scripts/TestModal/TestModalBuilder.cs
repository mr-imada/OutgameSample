using ScreenSystem.Modal;
public class TestModalBuilder : ModalBuilderBase<TestModalLifecycle, TestModalView, TestModalLifecycle.NetworkParameter>
{
    public TestModalBuilder(TestModalLifecycle.NetworkParameter parameter, bool playAnimation = true) : base(parameter, playAnimation) { }
}