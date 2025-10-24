using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private StatsViewsPool _statsViewsPool;
		[Inject] private BundlesPool _bundlesPool;
		[Inject] private IHealthInfo _health;
		[Inject] private ShopView _view;

		public void Initialize(int bundlesCount)
		{
			for (int i = 0; i < bundlesCount; i++)
				_view.AddBundle(_bundlesPool.Get().transform);

			CreateStatsView();
		}

		private void CreateStatsView()
		{
			var healthStats = _view.CreateStatsView();
			healthStats.SetLabel(_health.Name);
			healthStats.SetValue(_health.CurrentHealth.ToString());
		}
	}
}