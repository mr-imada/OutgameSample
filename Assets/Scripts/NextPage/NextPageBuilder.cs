using ScreenSystem.Page;

public class NextPageBuilder : PageBuilderBase<NextPageLifecycle, NextPageView, NextPageLifecycle.NetworkParameter>
{
    public NextPageBuilder(NextPageLifecycle.NetworkParameter parameter, bool playAnimation = true, bool stack = true) : base(parameter, playAnimation, stack) { }
}