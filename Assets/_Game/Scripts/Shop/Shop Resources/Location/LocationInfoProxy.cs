using Zenject;
using Core;

namespace Shop
{
	public class LocationInfoProxy : ResourceProxy, ILocationInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;

		private readonly PlayerDataKey _currentLocationKey;
		
		public string CurrentLocation => _playerDataInfo.GetString(_currentLocationKey);
		
		public LocationInfoProxy(ResourceDescriptor descriptor, PlayerDataKey currentLocationKey) : base(descriptor)
		{
			_currentLocationKey = currentLocationKey;
		}
	}
}