using Core;
using Zenject;

namespace Health
{
	public sealed class Health
	{
		private readonly PlayerDataKey _currentHealthKey;
		private readonly PlayerDataKey _maxHealthKey;
		private readonly PlayerData _playerData;
		
		public Health(PlayerData playerData, PlayerDataKey currentHealthKey, PlayerDataKey maxHealthKey, int defaultMax)
		{
			_currentHealthKey = currentHealthKey;
			_maxHealthKey = maxHealthKey;
			_playerData = playerData;

			_playerData.SetInt(_currentHealthKey, defaultMax);
			_playerData.SetInt(_maxHealthKey, defaultMax);
		}
	}
}