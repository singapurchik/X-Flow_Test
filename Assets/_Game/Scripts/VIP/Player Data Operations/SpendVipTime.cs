using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "Spend VIP Time", menuName = "VIP/Operations/Spend VIP Time")]
	public sealed class SpendVipTime : ConsumeOperation, IOperationWithParam
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		[Min(1)] [SerializeField] private int _defaultSeconds = 1;

		public override bool IsCanApply(IPlayerDataInfo data)
		{
			int sec = Mathf.Max(1, _defaultSeconds);
			return HasEnoughSeconds(data, sec);
		}

		public override void Apply(PlayerData data)
		{
			int sec = Mathf.Max(1, _defaultSeconds);
			SpendSeconds(data, sec);
		}

		public bool Supports(IOperationParam param) => param is IntAmountParam;

		public bool CanApply(IPlayerDataInfo data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int sec = Mathf.Max(1, p?.Amount ?? _defaultSeconds);
			return HasEnoughSeconds(data, sec);
		}

		public void Apply(PlayerData data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int sec = Mathf.Max(1, p?.Amount ?? _defaultSeconds);
			SpendSeconds(data, sec);
		}

		public IOperationParam CreateDefaultParam() => new IntAmountParam { Amount = _defaultSeconds };

#if UNITY_EDITOR
		private void OnValidate() => _defaultSeconds = Mathf.Max(1, _defaultSeconds);
#endif

		// --- Helpers ---
		private bool HasEnoughSeconds(IPlayerDataInfo data, int seconds)
		{
			long now = DateTime.UtcNow.Ticks;
			long until = ReadUntilTicks(data, _vipRemainingTimeKey);
			if (until <= now) return false;

			long left = until - now;
			long need = (long)seconds * TimeSpan.TicksPerSecond;
			return left >= need;
		}

		private void SpendSeconds(PlayerData data, int seconds)
		{
			long now = DateTime.UtcNow.Ticks;
			long until = ReadUntilTicks(data, _vipRemainingTimeKey);
			if (until <= now) { WriteUntilTicks(data, _vipRemainingTimeKey, 0L); return; }

			long need = (long)seconds * TimeSpan.TicksPerSecond;
			long next = until - need;

			if (next <= now) // истёк
				WriteUntilTicks(data, _vipRemainingTimeKey, 0L);
			else
				WriteUntilTicks(data, _vipRemainingTimeKey, next);
		}

		private static long ReadUntilTicks(IPlayerDataInfo data, PlayerDataKey key)
		{
			var s = data.GetString(key, "0");
			return long.TryParse(s, out var t) ? t : 0L;
		}

		private static void WriteUntilTicks(PlayerData data, PlayerDataKey key, long ticks)
		{
			data.SetString(key, ticks <= 0 ? "0" : ticks.ToString());
		}
	}
}
