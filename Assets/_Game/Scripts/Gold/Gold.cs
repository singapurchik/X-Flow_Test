using Core;

namespace Gold
{
	public class Gold
	{
		private readonly PlayerDataKey _currentGoldKey;

		public Gold(PlayerDataKey currentGoldKey, int startGold)
		{
			_currentGoldKey = currentGoldKey;
			
			PlayerData.SetInt(_currentGoldKey, startGold);
		}
	}
}