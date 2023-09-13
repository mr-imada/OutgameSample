using ScreenSystem.Page;

public class NextPageBuilder : PageBuilderBase<NextPageLifecycle, NextPageView>
{
    public NextPageBuilder(bool playAnimation = true, bool stack = true) : base(playAnimation, stack) { }
}