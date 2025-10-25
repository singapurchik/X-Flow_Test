using Zenject;
using Core;

namespace Shop
{
	public class LocationInfoProxy : ILocationInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;

		private readonly PlayerDataKey _currentLocationKey;

		public string CurrentLocation => _playerDataInfo.GetString(_currentLocationKey);
		public string DisplayName => "Location";
		
		public LocationInfoProxy(PlayerDataKey currentLocationKey)
		{
			_currentLocationKey = currentLocationKey;
		}
	}
}