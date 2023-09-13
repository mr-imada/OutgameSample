using ScreenSystem.Page;

public class TestPageBuilder : PageBuilderBase<TestPageLifecycle, TestPageView>
{
    public TestPageBuilder(bool playAnimation = true, bool stack = true) : base(playAnimation, stack) { }
}