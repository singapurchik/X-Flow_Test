using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "Spend VIP Time", menuName = "VIP/Operations/Spend VIP Time")]
	public sealed class SpendVipTime : ConsumeOperation
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		[Min(1)][SerializeField] private int _seconds = 1;

		public override bool IsCanApply(IPlayerDataInfo data)
		{
			var timeString = data.GetString(_vipRemainingTimeKey, "0");
			
			if (long.TryParse(timeString, out var untilTicks))
			{
				var leftTicks = untilTicks - DateTime.UtcNow.Ticks;
				if (leftTicks > 0)
				{
					var needTicks = _seconds * TimeSpan.TicksPerSecond;
					return leftTicks >= needTicks;		
				}
			}
			return false;
		}

		public override void Apply(PlayerData data)
		{
			var timeString = data.GetString(_vipRemainingTimeKey, "0");
			
			if (long.TryParse(timeString, out var untilTicks))
			{
				var needTicks = _seconds * TimeSpan.TicksPerSecond;
				var newUntil = untilTicks - needTicks;

				if (newUntil <= DateTime.UtcNow.Ticks)
					data.SetString(_vipRemainingTimeKey, "0");
				else
					data.SetString(_vipRemainingTimeKey, newUntil.ToString());	
			}
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_seconds = Mathf.Max(1, _seconds);
		}
#endif
	}
}