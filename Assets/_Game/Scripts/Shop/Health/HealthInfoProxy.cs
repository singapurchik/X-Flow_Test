using Core;

namespace Shop
{
	public sealed class HealthInfoProxy : IHealthInfo
	{
		private readonly PlayerDataKey _currentKey;
		private readonly PlayerDataKey _maxKey;

		public HealthInfoProxy(PlayerDataKey currentKey, PlayerDataKey maxKey)
		{
			_currentKey = currentKey;
			_maxKey = maxKey;
		}

		public int CurrentHealth => PlayerData.GetInt(_currentKey);
		public int MaxHealth => PlayerData.GetInt(_maxKey);
	}
}