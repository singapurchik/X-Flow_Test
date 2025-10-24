using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private IHealthInfo _health;
		[Inject] private IGoldInfo _gold;
		[Inject] private IShopView _view;

		public void Initialize(int bundlesCount)
		{
			for (var i = 0; i < bundlesCount; i++)
				_view.CreateBundle();

			CreateStatsViews();
		}

		private void CreateStatsViews()
		{
			CreateStatsView(_health.Name, _health.CurrentHealth.ToString());
			CreateStatsView(_gold.Name, _gold.CurrentGold.ToString());
		}

		private void CreateStatsView(string label, string value)
		{
			var view = _view.CreateStatsView();
			view.SetLabel(label);
			view.SetValue(value);
		}
	}
}