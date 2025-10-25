namespace Shop
{
	public interface IShopView
	{
		public Bundle CreateBundle(BundleData data);

		public IStatsView CreateStatsView();

		public void CreateStatsViews();
		public void UpdateView();
	}
}