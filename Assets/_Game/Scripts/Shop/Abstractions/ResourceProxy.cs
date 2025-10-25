using Core;

namespace Shop
{
	public abstract class ResourceProxy : IHasDisplayName
	{
		private PlayerDataValueInfo _descriptor;

		public string DisplayName => _descriptor.DisplayName;

		public ResourceProxy(PlayerDataValueInfo descriptor)
		{
			_descriptor = descriptor;
		}
	}
}