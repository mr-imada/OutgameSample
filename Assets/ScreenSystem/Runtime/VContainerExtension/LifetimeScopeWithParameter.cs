using VContainer;
using VContainer.Unity;

namespace ScreenSystem.VContainerExtension
{
	public class LifetimeScopeWithParameter<T> : LifetimeScope
	{
		protected T Parameter;

		protected override void Configure(IContainerBuilder builder)
		{
			base.Configure(builder);
			builder.RegisterInstance(Parameter);
		}

		public void SetParameter(T parameter)
		{
			Parameter = parameter;
		}
	}
}
