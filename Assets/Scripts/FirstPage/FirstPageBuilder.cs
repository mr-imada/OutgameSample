using ScreenSystem.Page;

public class FirstPageBuilder : PageBuilderBase<FirstPageLifecycle, FirstPageView>
{
    public FirstPageBuilder(bool playAnimation = true, bool stack = true) : base(playAnimation, stack) { }
}