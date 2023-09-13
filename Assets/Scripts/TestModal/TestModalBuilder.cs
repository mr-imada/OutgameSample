using ScreenSystem.Modal;
public class TestModalBuilder : ModalBuilderBase<TestModalLifecycle, TestModalView>
{
    public TestModalBuilder(bool playAnimation = true) : base(playAnimation) { }
}