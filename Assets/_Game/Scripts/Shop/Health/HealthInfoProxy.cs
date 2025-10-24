using Zenject;
using Core;

namespace Shop
{
	public sealed class HealthInfoProxy : IHealthInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;
		
		private readonly PlayerDataKey _currentKey;
		private readonly PlayerDataKey _maxKey;

		public HealthInfoProxy(PlayerDataKey currentKey, PlayerDataKey maxKey)
		{
			_currentKey = currentKey;
			_maxKey = maxKey;
		}

		public int CurrentHealth => _playerDataInfo.GetInt(_currentKey);
		public int MaxHealth => _playerDataInfo.GetInt(_maxKey);

		public string Name => "Health";
	}
}