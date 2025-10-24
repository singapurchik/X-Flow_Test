using Core;

namespace Gold
{
	public class Gold
	{
		private readonly PlayerDataKey _currentGoldKey;
		private readonly PlayerData _playerData;

		public Gold(PlayerData playerData, PlayerDataKey currentGoldKey, int startGold)
		{
			_currentGoldKey = currentGoldKey;
			_playerData = playerData;
			
			_playerData.SetInt(_currentGoldKey, startGold);
		}
	}
}