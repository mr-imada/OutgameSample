using System.Threading;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;
using VContainer.Unity;

namespace ScreenSystem.Modal
{
    public interface IModalBuilder
    {
        UniTask<IModal> Build(ModalContainer modalContainer, LifetimeScope parent, CancellationToken cancellationToken);
    }
}