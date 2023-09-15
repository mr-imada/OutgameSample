using System.Threading;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer.Unity;

namespace ScreenSystem.Page
{
    public interface IPageBuilder
    {
        UniTask<IPage> Build(PageContainer pageContainer, LifetimeScope parent, CancellationToken cancellationToken);
    }
}