using ScreenSystem.Modal;
public class TestModalBuilder : ModalBuilderBase<TestModalLifecycle, TestModalView, TestModalLifecycle.CountParameter>
{
    public TestModalBuilder(TestModalLifecycle.CountParameter parameter, bool playAnimation = true) : base(parameter, playAnimation) { }
}