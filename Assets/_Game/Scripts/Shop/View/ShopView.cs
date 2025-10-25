using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Shop
{
	public sealed class ShopView : MonoBehaviour, IShopView
	{
		[SerializeField] private RectTransform _bundlesContainer;
		[SerializeField] private RectTransform _statsViewContainer;

		[Inject] private StatsViewsPool _statsPool;
		[Inject] private BundlesPool _bundlesPool;
		[Inject] private ILocationInfo _location;
		[Inject] private IHealthInfo _health;
		[Inject] private IGoldInfo _gold;
		[Inject] private IShopView _view;
		[Inject] private IVIPInfo _vip;
		
		private readonly HashSet<StatsView> _statsView = new (10);
		private readonly HashSet<Bundle> _bundles = new (10);
		
		public void CreateStatsViews()
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

		public Bundle CreateBundle(BundleData data)
		{
			var bundle = _bundlesPool.Get();
			bundle.transform.SetParent(_bundlesContainer, false);
			bundle.Initialize(data);
			_bundles.Add(bundle);
			return bundle;
		}

		public IStatsView CreateStatsView()
		{
			var statsView = _statsPool.Get();
			statsView.transform.SetParent(_statsViewContainer, false);
			_statsView.Add(statsView);
			return statsView;
		}
		
		public void UpdateView()
		{
			foreach (var bundle in _bundles)
				bundle.UpdateButtonState();
		}
	}
}