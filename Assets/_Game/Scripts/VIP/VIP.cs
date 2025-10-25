using System;
using Core;

namespace VIP
{
	public class VIP
	{
		private readonly PlayerDataKey _vipRemainingTime;
		private readonly PlayerData _playerData;

		public VIP(PlayerData playerData, PlayerDataKey vipRemainingTime, TimeSpan startingTime)
		{
			_vipRemainingTime = vipRemainingTime;
			_playerData = playerData;

			if (startingTime > TimeSpan.Zero)
			{
				var until = DateTime.UtcNow + startingTime;
				_playerData.SetString(_vipRemainingTime, until.Ticks.ToString());
			}
		}
	}
}