using System;

namespace ScreenSystem.Attributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class AssetNameAttribute : Attribute
	{
		public string PrefabName { get; }

		public AssetNameAttribute(string prefabName)
		{
			this.PrefabName = prefabName;
		}
	}
}