using Zenject;
using Core;

namespace Shop
{
	public sealed class HealthInfoProxy : ResourceProxy, IHealthInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;
		
		private readonly PlayerDataKey _currentKey;
		private readonly PlayerDataKey _maxKey;

		public int CurrentHealth => _playerDataInfo.GetInt(_currentKey);
		public int MaxHealth => _playerDataInfo.GetInt(_maxKey);
		
		public HealthInfoProxy(ResourceDescriptor descriptor, PlayerDataKey currentKey, PlayerDataKey maxKey)
			: base(descriptor)
		{
			_currentKey = currentKey;
			_maxKey = maxKey;
		}
	}
}