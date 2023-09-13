using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
using VContainer.Unity;

namespace ScreenSystem.Page
{
	public class PageManager : IInitializable, IDisposable
	{
		class PageTransitionScope : IDisposable
		{
			public static bool IsPageTransition
			{
				get;
				private set;
			}

			public static IDisposable Transition()
			{
				return new PageTransitionScope();
			}

			private PageTransitionScope()
			{
				IsPageTransition = true;
			}

			public void Dispose()
			{
				IsPageTransition = false;
			}
			
			public static UniTask WaitTransition(CancellationToken token)
			{
				return UniTask.WaitUntil(() => !IsPageTransition, cancellationToken: token);
			}
		}
		
		private readonly PageContainer _pageContainer;
		private readonly LifetimeScope _lifetimeScope;
		private readonly CancellationTokenSource _cancellationTokenSource;
		private readonly IPageEventSubscriber _pageEventSubscriber;
		private IDisposable _disposable;


		[Inject]
		public PageManager(PageContainer pageContainer, 
			LifetimeScope lifetimeScope,
			IPageEventSubscriber pageEventSubscriber)
		{
			_pageContainer = pageContainer;
			_lifetimeScope = lifetimeScope;
			_pageEventSubscriber = pageEventSubscriber;
			_cancellationTokenSource = new CancellationTokenSource();
		}

		private async UniTask<IPage> Push(IPageBuilder builder, CancellationToken cancellationToken)
		{
			if (PageTransitionScope.IsPageTransition)
			{
				await PageTransitionScope.WaitTransition(cancellationToken);
			}
			
			using var scope = PageTransitionScope.Transition();	
			var page = await builder.Build(_pageContainer, _lifetimeScope, cancellationToken);
			return page;
		}

		private async UniTask Pop(bool playAnimation, CancellationToken cancellationToken)
		{
			if (PageTransitionScope.IsPageTransition)
			{
				await PageTransitionScope.WaitTransition(cancellationToken);
			}
			
			using var scope = PageTransitionScope.Transition();
			if (_pageContainer.Pages.Count < 1)
			{
				return;
			}
			var handle = _pageContainer.Pop(playAnimation);
			await handle.WithCancellation(cancellationToken);
		}

		public void Initialize()
		{
			_pageEventSubscriber.OnPagePushAsyncEnumerable()
				.ForEachAwaitAsync(message => Push(message.Builder, _cancellationTokenSource.Token).SuppressCancellationThrow(), 
					_cancellationTokenSource.Token);

			_pageEventSubscriber.OnPagePopAsyncEnumerable()
				.ForEachAwaitAsync(message => Pop(message.PlayAnimation, _cancellationTokenSource.Token).SuppressCancellationThrow(),
					_cancellationTokenSource.Token);
		}

		public void Dispose()
		{
			_cancellationTokenSource?.Cancel();
		}
	}
}