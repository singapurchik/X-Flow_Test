using System.Collections.Generic;
using Core;
using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private IReadOnlyList<BundleData> _bundlesData;
		[Inject] private PlayerData _playerData;
		[Inject] private IShopView _view;

		public void Initialize()
		{
			if (_bundlesData != null && _bundlesData.Count > 0)
			{
				for (var i = 0; i < _bundlesData.Count; i++)
				{
					var bundle = _view.CreateBundle(_bundlesData[i]);
					bundle.OnBundleOutOfStock += OnBundleOutOfStock;
					bundle.OnBuyButtonClicked += OnBuyBundle;
				}
			}

			_view.CreateStatsViews();
		}

		private void OnBuyBundle(BundleData data)
		{
			foreach (var cost in data.Costs)
				cost.Apply(_playerData);
			
			foreach (var reward in data.Rewards)
				reward.Apply(_playerData);
			
			_view.UpdateView();
		}

		private void OnBundleOutOfStock(Bundle bundle)
		{
			bundle.OnBundleOutOfStock -= OnBundleOutOfStock;
			bundle.OnBuyButtonClicked -= OnBuyBundle;
		}
	}
}