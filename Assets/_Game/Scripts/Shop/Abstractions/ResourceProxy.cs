using Core;

namespace Shop
{
	public abstract class ResourceProxy : IHasDisplayName
	{
		private ResourceDescriptor _descriptor;

		public string DisplayName => _descriptor.DisplayName;

		public ResourceProxy(ResourceDescriptor descriptor)
		{
			_descriptor = descriptor;
		}
	}
}