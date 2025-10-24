namespace Shop
{
	public interface IShopView
	{
		public void CreateBundle();

		public IStatsView CreateStatsView();
	}
}