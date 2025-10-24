using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private IHealthInfo _health;
		[Inject] private IShopView _view;

		public void Initialize(int bundlesCount)
		{
			for (var i = 0; i < bundlesCount; i++)
				_view.CreateBundle();

			CreateStatsView();
		}

		private void CreateStatsView()
		{
			var healthStatsView = _view.CreateStatsView();
			healthStatsView.SetLabel(_health.Name);
			healthStatsView.SetValue(_health.CurrentHealth.ToString());
		}
	}
}