using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "VIP Info", menuName = "VIP/VIP Info")]
	public class VIPInfo : PlayerDataValueInfo
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		
		public PlayerDataKey VipRemainingTimeKey => _vipRemainingTimeKey;

		public TimeSpan GetRemainingTime(IPlayerDataInfo data)
		{
			var remainingTimeString = data.GetString(_vipRemainingTimeKey, "0");
			return long.TryParse(remainingTimeString, out var time) && time > DateTime.UtcNow.Ticks
				? TimeSpan.FromTicks(time - DateTime.UtcNow.Ticks)
				: TimeSpan.Zero;
		}

		public int GetRemainingSeconds(IPlayerDataInfo data)
		{
			var remainingTimeString = data.GetString(_vipRemainingTimeKey, "0");
			if (!long.TryParse(remainingTimeString, out var untilTicks))return 0;
			var left = untilTicks - DateTime.UtcNow.Ticks;
			return left > 0 ? (int)TimeSpan.FromTicks(left).TotalSeconds : 0;
		}
		
		public long GetUntilTicks(IPlayerDataInfo data)
			=> long.TryParse(data.GetString(_vipRemainingTimeKey, "0"), out var t) ? t : 0L;

		public bool IsActive(IPlayerDataInfo data) => GetRemainingTime(data) > TimeSpan.Zero;

		public override string ReadCurrentValueAsString(IPlayerDataInfo data) => GetRemainingSeconds(data).ToString();
	}
}