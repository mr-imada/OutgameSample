using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;
using VContainer;
using VContainer.Unity;

namespace ScreenSystem.Modal
{
	public class ModalManager
	{
		class ModalTransitionScope : IDisposable
		{
			public static bool IsModalTransition
			{
				get;
				private set;
			}

			public static IDisposable Transition()
			{
				return new ModalTransitionScope();
			}

			private ModalTransitionScope()
			{
				IsModalTransition = true;
			}

			public void Dispose()
			{
				IsModalTransition = false;
			}

			public static UniTask WaitTransition(CancellationToken token)
			{
				return UniTask.WaitUntil(() => !IsModalTransition, cancellationToken: token);
			}
		}
		
		private readonly ModalContainer _modalContainer;
		private readonly LifetimeScope _lifetimeScope;
		private IDisposable _disposable;


		[Inject]
		public ModalManager(ModalContainer modalContainer, LifetimeScope lifetimeScope)
		{
			_modalContainer = modalContainer;
			_lifetimeScope = lifetimeScope;
		}

		public async UniTask<IModal> Push(IModalBuilder builder, CancellationToken cancellationToken)
		{
			if (ModalTransitionScope.IsModalTransition)
			{
				await ModalTransitionScope.WaitTransition(cancellationToken);
			}
			
			using var scope = ModalTransitionScope.Transition();	
			var page = await builder.Build(_modalContainer, _lifetimeScope, cancellationToken);
			return page;
		}

		public async UniTask Pop(bool playAnimation, CancellationToken cancellationToken)
		{
			if (ModalTransitionScope.IsModalTransition)
			{
				await ModalTransitionScope.WaitTransition(cancellationToken);
			}
			
			using var scope = ModalTransitionScope.Transition();
			if (_modalContainer.Modals.Any())
			{
				var handle = _modalContainer.Pop(playAnimation);
				await handle.WithCancellation(cancellationToken);
			}
		}

		public async UniTask AllPop(bool animation, CancellationToken cancellationToken)
		{
			while (_modalContainer.Modals.Any())
			{
				await Pop(animation, cancellationToken);
			}
		}
	}
}