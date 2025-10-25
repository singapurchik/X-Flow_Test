using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "Add VIP Time", menuName = "VIP/Operations/Add VIP Time")]
	public sealed class AddVipTime : ProvideOperation, IOperationWithParam
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		[Min(1)] [SerializeField] private int _defaultSeconds = 30;

		public override bool IsCanApply(IPlayerDataInfo data) => true;

		public override void Apply(PlayerData data)
		{
			int sec = Mathf.Max(1, _defaultSeconds);
			AddSeconds(data, sec);
		}

		public bool Supports(IOperationParam param) => param is IntAmountParam;

		public bool CanApply(IPlayerDataInfo data, IOperationParam param) => true;

		public void Apply(PlayerData data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int sec = Mathf.Max(1, p?.Amount ?? _defaultSeconds);
			AddSeconds(data, sec);
		}

		public IOperationParam CreateDefaultParam() => new IntAmountParam { Amount = _defaultSeconds };

#if UNITY_EDITOR
		private void OnValidate() => _defaultSeconds = Mathf.Max(1, _defaultSeconds);
#endif

		private void AddSeconds(PlayerData data, int seconds)
		{
			long now = DateTime.UtcNow.Ticks;
			long currentUntil = ReadUntilTicks(data, _vipRemainingTimeKey);

			if (currentUntil < now) currentUntil = now;

			long delta = (long)seconds * TimeSpan.TicksPerSecond;

			long maxAdd = long.MaxValue - currentUntil;
			long newUntil = delta > maxAdd ? long.MaxValue : currentUntil + delta;

			WriteUntilTicks(data, _vipRemainingTimeKey, newUntil);
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