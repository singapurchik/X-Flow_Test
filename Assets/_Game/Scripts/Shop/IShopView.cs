namespace Shop
{
	public interface IShopView
	{
		public void CreateBundle(BundleData data);

		public IStatsView CreateStatsView();
	}
}