using UnityEngine;
using System;
using Core;

namespace VIP
{
	[CreateAssetMenu(fileName = "VIP", menuName = "VIP/VIP")]
	public class VIP : PlayerDataValue
	{
		[SerializeField] private VIPInfo _info;
		[Min(0)] [SerializeField] private int _startDurationSeconds = 30;
		
		public VIPInfo Info => _info;

		public override void Initialize(PlayerData data)
		{
			var startingTime = TimeSpan.FromSeconds(_startDurationSeconds);
			
			if (startingTime > TimeSpan.Zero)
			{
				var until = DateTime.UtcNow + startingTime;
				data.SetString(_info.VipRemainingTimeKey, until.Ticks.ToString());
			}
		}
		
		public void SetUntilTicks(PlayerData data, long ticks)
			=> data.SetString(_info.VipRemainingTimeKey, ticks <= 0 ? "0" : ticks.ToString());
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			_startDurationSeconds = Mathf.Max(0, _startDurationSeconds);
		}
#endif
	}
}