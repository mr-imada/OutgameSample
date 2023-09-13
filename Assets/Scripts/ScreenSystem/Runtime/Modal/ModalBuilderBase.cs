using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ScreenSystem.Attributes;
using ScreenSystem.VContainerExtension;
using UnityScreenNavigator.Runtime.Core.Modal;
using VContainer;
using VContainer.Unity;

namespace ScreenSystem.Modal
{
    public abstract class ModalBuilderBase<TModal, TModalView> : IModalBuilder
        where TModal : IModal
        where TModalView : ModalViewBase
    {
        private readonly bool _playAnimation;
        public ModalBuilderBase(bool playAnimation = true)
        {
            _playAnimation = playAnimation;
        }

        public async UniTask<IModal> Build(ModalContainer modalContainer, LifetimeScope parent, CancellationToken cancellationToken)
        {
            var nameAttr = Attribute.GetCustomAttribute(typeof(TModal), typeof(AssetNameAttribute)) as AssetNameAttribute;
            var source = new UniTaskCompletionSource<IModal>();
            using (LifetimeScope.EnqueueParent(parent))
            {
                var modalTask = modalContainer.Push(nameAttr.PrefabName, playAnimation: _playAnimation, onLoad: modal =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        source.TrySetCanceled(cancellationToken);
                        return;
                    }
                    var modalView = modal.modal as TModalView;
                    var lts = modalView.gameObject.GetComponentInChildren<LifetimeScope>();
                    SetUpParameter(lts);
                    lts.Build();
                    var pageInstance = lts.Container.Resolve<TModal>();
                    source.TrySetResult(pageInstance);
                });

                var modal = await source.Task;
                await modalTask.Task;
                cancellationToken.ThrowIfCancellationRequested();
                return modal;
            }
        }

        protected virtual void SetUpParameter(LifetimeScope lifetimeScope)
        {
        }
    }

    public abstract class ModalBuilderBase<TModal, TModalView, TParameter> : ModalBuilderBase<TModal, TModalView>
        where TModal : IModal
        where TModalView : ModalViewBase
    {
        private readonly TParameter _parameter;

        public ModalBuilderBase(TParameter parameter, bool playAnimation = true) : base(playAnimation)
        {
            _parameter = parameter;
        }

        protected override void SetUpParameter(LifetimeScope lifetimeScope)
        {
            if (lifetimeScope is LifetimeScopeWithParameter<TParameter> withParameter)
            {
                withParameter.SetParameter(_parameter);
            }
        }
    }
}