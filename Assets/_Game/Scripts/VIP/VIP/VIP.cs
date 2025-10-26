using UnityEngine;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "VIP", menuName = "VIP/VIP")]
	public class VIP : PlayerDataValue
	{
		[SerializeField] private VIPInfo _info;
		[Min(0)] [SerializeField] private int _startDurationSeconds = 30;

		public override PlayerDataValueInfo Info => _info;

		public override void Initialize(PlayerData data)
		{
			if (_startDurationSeconds <= 0) return;

			var now = VipTime.NowTicks();
			var add = VipTime.SecondsToTicks(_startDurationSeconds);
			var until = now + add;
			VipTime.WriteUntil(data, _info.VipRemainingTimeKey, until);
		}

		public long GetUntilTicks(IPlayerDataInfo data) => _info.GetUntilTicks(data);

		public void SetUntilTicks(PlayerData data, long ticks)
			=> VipTime.WriteUntil(data, _info.VipRemainingTimeKey, ticks);

		public int GetRemainingSeconds(IPlayerDataInfo data) => _info.GetRemainingSeconds(data);
		public bool IsActive(IPlayerDataInfo data) => _info.IsActive(data);

#if UNITY_EDITOR
		private void OnValidate() => _startDurationSeconds = Mathf.Max(0, _startDurationSeconds);
#endif
	}
}