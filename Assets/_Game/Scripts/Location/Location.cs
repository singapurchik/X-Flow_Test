using Core;

namespace Location
{
	public class Location
	{
		private readonly PlayerDataKey _currentLocationKey;
		private readonly PlayerData _playerData;

		public Location(PlayerData playerData, PlayerDataKey currentLocationKey, LocationType startLocation)
		{
			_currentLocationKey = currentLocationKey;
			_playerData = playerData;
			
			_playerData.SetString(_currentLocationKey, startLocation.ToString());
		}
	}
}