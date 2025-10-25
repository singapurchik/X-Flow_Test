using Zenject;
using System;
using Core;

namespace Shop
{
	public class VIPInfoProxy : ResourceProxy, IVIPInfo
	{
		[Inject] private IPlayerDataInfo _playerDataInfo;
		
		private readonly PlayerDataKey _vipRemainingTime;

		public TimeSpan RemainingTime
		{
			get
			{
				var remainingTimeString = _playerDataInfo.GetString(_vipRemainingTime, "0");
				return long.TryParse(remainingTimeString, out var time) && time > DateTime.UtcNow.Ticks
					? TimeSpan.FromTicks(time - DateTime.UtcNow.Ticks)
					: TimeSpan.Zero;
			}
		}

		public int RemainingSeconds
		{
			get
			{
				var remainingTimeString = _playerDataInfo.GetString(_vipRemainingTime, "0");
				if (!long.TryParse(remainingTimeString, out var untilTicks))return 0;
				var left = untilTicks - DateTime.UtcNow.Ticks;
				return left > 0 ? (int)TimeSpan.FromTicks(left).TotalSeconds : 0;
			}
		}

		public bool IsActive => RemainingTime > TimeSpan.Zero;

		public VIPInfoProxy(ResourceDescriptor descriptor, PlayerDataKey vipRemainingTime) : base(descriptor)
		{
			_vipRemainingTime = vipRemainingTime;
		}
	}
}