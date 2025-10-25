using Shop.VIP;
using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private ILocationInfo _location;
		[Inject] private IHealthInfo _health;
		[Inject] private IGoldInfo _gold;
		[Inject] private IShopView _view;
		[Inject] private IVIPInfo _vip;

		public void Initialize(int bundlesCount)
		{
			for (var i = 0; i < bundlesCount; i++)
				_view.CreateBundle();

			CreateStatsViews();
		}

		private void CreateStatsViews()
		{
			CreateStatsView(_health.DisplayName, _health.CurrentHealth.ToString());
			CreateStatsView(_location.DisplayName, _location.CurrentLocation);
			CreateStatsView(_gold.DisplayName, _gold.CurrentGold.ToString());
			CreateStatsView(_vip.DisplayName, _vip.RemainingSeconds + " sec");
		}

		private void CreateStatsView(string label, string value)
		{
			var view = _view.CreateStatsView();
			view.SetLabel(label);
			view.SetValue(value);
		}
	}
}