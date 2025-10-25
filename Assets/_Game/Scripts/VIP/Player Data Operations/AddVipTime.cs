using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "Add VIP Time", menuName = "VIP/Operations/Add VIP Time")]
	public sealed class AddVipTime : ProvideOperation
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		[Min(1)][SerializeField] private int _seconds = 30;

		public override bool IsCanApply(IPlayerDataInfo data) => true;

		public override void Apply(PlayerData data)
		{
			var nowTicks = DateTime.UtcNow.Ticks;
			var deltaTicks = _seconds * TimeSpan.TicksPerSecond;

			var timeString = data.GetString(_vipRemainingTimeKey, "0");

			if (!long.TryParse(timeString, out var currentUntil) || currentUntil < nowTicks)
				currentUntil = nowTicks;

			long newUntil;
			long maxAdd = long.MaxValue - currentUntil;
			if (deltaTicks > maxAdd)
				newUntil = long.MaxValue;
			else
				newUntil = currentUntil + deltaTicks;

			data.SetString(_vipRemainingTimeKey, newUntil.ToString());
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_seconds = Mathf.Max(1, _seconds);
		}
#endif
	}
}