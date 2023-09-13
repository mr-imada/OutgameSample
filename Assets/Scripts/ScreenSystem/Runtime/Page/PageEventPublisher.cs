using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScreenSystem.Page.Messages;
using VContainer;

namespace ScreenSystem.Page
{
	public class PageEventPublisher : IPageEventSubscriber, IDisposable
	{
		private readonly Channel<PagePushMessage> _pagePushChannel;
		private readonly Channel<PagePopMessage> _pagePopChannel;
		
		[Inject]
		public PageEventPublisher()
		{
			_pagePushChannel = Channel.CreateSingleConsumerUnbounded<PagePushMessage>();
			_pagePopChannel = Channel.CreateSingleConsumerUnbounded<PagePopMessage>();
		}

		public void SendPushEvent(IPageBuilder builder)
		{
			_pagePushChannel.Writer.TryWrite(new PagePushMessage(builder));
		}

		public void SendPopEvent(bool playAnimation = true)
		{
			_pagePopChannel.Writer.TryWrite(new PagePopMessage(playAnimation));
		}

		IUniTaskAsyncEnumerable<PagePushMessage> IPageEventSubscriber.OnPagePushAsyncEnumerable()
			=> _pagePushChannel.Reader.ReadAllAsync();

		IUniTaskAsyncEnumerable<PagePopMessage> IPageEventSubscriber.OnPagePopAsyncEnumerable()
			=> _pagePopChannel.Reader.ReadAllAsync();

		public void Dispose()
		{
			_pagePushChannel.Writer.TryComplete();
			_pagePopChannel.Writer.TryComplete();
		}
	}
}