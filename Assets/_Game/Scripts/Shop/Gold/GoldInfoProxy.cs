using Zenject;
using Core;

namespace Shop
{
	public class GoldInfoProxy : IGoldInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;
		
		private readonly PlayerDataKey _currentGoldKey;
		
		public int CurrentGold => _playerDataInfo.GetInt(_currentGoldKey);
		public string DisplayName => "Gold";

		public GoldInfoProxy(PlayerDataKey currentGoldKey)
		{
			_currentGoldKey = currentGoldKey;
		}
	}
}