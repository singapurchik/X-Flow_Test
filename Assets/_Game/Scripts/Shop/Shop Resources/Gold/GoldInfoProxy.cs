using Zenject;
using Core;

namespace Shop
{
	public class GoldInfoProxy : ResourceProxy, IGoldInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;
		
		private readonly PlayerDataKey _currentGoldKey;

		public int CurrentGold => _playerDataInfo.GetInt(_currentGoldKey);
		
		public GoldInfoProxy(ResourceDescriptor descriptor, PlayerDataKey currentGoldKey) : base(descriptor)
		{
			_currentGoldKey = currentGoldKey;
		}
	}
}