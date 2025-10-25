using System;

namespace Shop.VIP
{
	public interface IVIPInfo : IHasDisplayName
	{
		public TimeSpan RemainingTime { get; }
		
		public int RemainingSeconds { get; }
		
		public bool IsActive { get; }
	}
}