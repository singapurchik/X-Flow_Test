namespace Shop
{
	public interface IShopView
	{
		public Bundle CreateBundle(BundleData data);

		public void CreateStatsViews();
		public void DisableInput();
		public void EnableInput();
		public void UpdateView();
	}
}