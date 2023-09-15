namespace ScreenSystem.Page.Messages
{
	public class PagePushMessage
	{
		public readonly IPageBuilder Builder;

		public PagePushMessage(IPageBuilder builder)
		{
			Builder = builder;
		}
	}
}