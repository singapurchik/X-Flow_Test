using Core;

namespace Shop.Gold
{
	public class GoldInfoProxy : IGoldInfo
	{
		private readonly PlayerDataKey _currentGoldKey;
		
		public int CurrentGold { get; }
	}
}