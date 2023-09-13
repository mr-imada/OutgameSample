using Cysharp.Threading.Tasks;
using ScreenSystem.Page.Messages;

namespace ScreenSystem.Page
{
    public interface IPageEventSubscriber
    {
        IUniTaskAsyncEnumerable<PagePushMessage> OnPagePushAsyncEnumerable();

        IUniTaskAsyncEnumerable<PagePopMessage> OnPagePopAsyncEnumerable();
    }
}