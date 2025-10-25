using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "Spend VIP Time", menuName = "VIP/Operations/Spend VIP Time")]
	public sealed class SpendVipTime : ConsumeOperation, IOperationWithParameter
	{
		[SerializeField] private PlayerDataKey _vipRemainingTimeKey;
		[Min(1)] [SerializeField] private int _defaultSeconds = 1;

		public IOperationParameter CreateDefaultParam() => new IntAmountParameter { Amount = _defaultSeconds };
		
		public bool IsSupports(IOperationParameter parameter) => parameter is IntAmountParameter;
		
		public bool IsCanApply(IPlayerDataInfo data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			return IsHasEnoughSeconds(data, Mathf.Max(1, intParam?.Amount ?? _defaultSeconds));
		}
		
		public override bool IsCanApply(IPlayerDataInfo data)
			=> IsHasEnoughSeconds(data, Mathf.Max(1, _defaultSeconds));

		public override void Apply(PlayerData data) => SpendSeconds(data,  Mathf.Max(1, _defaultSeconds));

		public void Apply(PlayerData data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			SpendSeconds(data, Mathf.Max(1, intParam?.Amount ?? _defaultSeconds));
		}

		private bool IsHasEnoughSeconds(IPlayerDataInfo data, int seconds)
		{
			var now = DateTime.UtcNow.Ticks;
			var until = ReadUntilTicks(data, _vipRemainingTimeKey);
			
			if (until > now)
				return until - now >= seconds * TimeSpan.TicksPerSecond;
			
			return false;
		}

		private void SpendSeconds(PlayerData data, int seconds)
		{
			var now = DateTime.UtcNow.Ticks;
			var until = ReadUntilTicks(data, _vipRemainingTimeKey);
			
			if (until > now)
			{
				var next = until - (seconds * TimeSpan.TicksPerSecond);
				WriteUntilTicks(data, _vipRemainingTimeKey, next <= now ? 0L : next);
			}
			else
			{
				WriteUntilTicks(data, _vipRemainingTimeKey, 0L);
			}
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
