using Core;
using Zenject;

namespace Health
{
	public sealed class Health
	{
		private readonly PlayerDataKey _currentHealthKey;
		private readonly PlayerData _playerData;
		
		public Health(PlayerData playerData, PlayerDataKey currentHealthKey, int startHealth)
		{
			_currentHealthKey = currentHealthKey;
			_playerData = playerData;

			_playerData.SetInt(_currentHealthKey, startHealth);
		}
	}
}