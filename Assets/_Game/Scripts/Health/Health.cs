using UnityEngine;
using Core;

namespace Health
{
	public sealed class Health
	{
		private readonly PlayerDataKey _currentHealthKey;
		private readonly PlayerDataKey _maxHealthKey;
		
		public Health(PlayerDataKey currentHealthKey, PlayerDataKey maxHealthKey, int defaultMax)
		{
			_currentHealthKey = currentHealthKey;
			_maxHealthKey = maxHealthKey;

			PlayerData.SetInt(_currentHealthKey, defaultMax);
			PlayerData.SetInt(_maxHealthKey, defaultMax);
		}
	}
}