using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "Add VIP Time", menuName = "VIP/Operations/Add VIP Time")]
	public sealed class AddVipTime : ProvideOperation, IOperationWithParameter
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		[Min(1)] [SerializeField] private int _defaultSeconds = 30;

		public IOperationParameter CreateDefaultParam() => new IntAmountParameter { Amount = _defaultSeconds };
		
		public bool IsSupports(IOperationParameter parameter) => parameter is IntAmountParameter;
		
		public bool IsCanApply(IPlayerDataInfo data, IOperationParameter parameter) => IsCanApply(data);
		
		public void Apply(PlayerData data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			AddSeconds(data, Mathf.Max(1, intParam?.Amount ?? _defaultSeconds));
		}

		public override void Apply(PlayerData data) => AddSeconds(data, Mathf.Max(1, _defaultSeconds));

		private void AddSeconds(PlayerData data, int seconds)
		{
			var now = DateTime.UtcNow.Ticks;
			var currentUntil = ReadUntilTicks(data, _vipRemainingTimeKey);

			if (currentUntil < now)
				currentUntil = now;

			var delta = seconds * TimeSpan.TicksPerSecond;
			var maxAdd = long.MaxValue - currentUntil;
			var newUntil = delta > maxAdd ? long.MaxValue : currentUntil + delta;

			WriteUntilTicks(data, _vipRemainingTimeKey, newUntil);
		}

		private static long ReadUntilTicks(IPlayerDataInfo data, PlayerDataKey key)
			=> long.TryParse(data.GetString(key, "0"), out var t) ? t : 0L;

		private static void WriteUntilTicks(PlayerData data, PlayerDataKey key, long ticks)
			=> data.SetString(key, ticks <= 0 ? "0" : ticks.ToString());
		
#if UNITY_EDITOR
		private void OnValidate() => _defaultSeconds = Mathf.Max(1, _defaultSeconds);
#endif
	}
}