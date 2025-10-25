using System.Collections.Generic;
using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private IReadOnlyList<BundleData> _bundlesData;
		[Inject] private ILocationInfo _location;
		[Inject] private IHealthInfo _health;
		[Inject] private IGoldInfo _gold;
		[Inject] private IShopView _view;
		[Inject] private IVIPInfo _vip;

		public void Initialize()
		{
			if (_bundlesData != null && _bundlesData.Count > 0)
				for (var i = 0; i < _bundlesData.Count; i++)
					_view.CreateBundle(_bundlesData[i]);

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