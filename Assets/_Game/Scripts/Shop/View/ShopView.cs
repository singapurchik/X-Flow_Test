using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core;

namespace Shop
{
	public sealed class ShopView : MonoBehaviour, IShopView
	{
		[SerializeField] private RectTransform _bundlesContainer;
		[SerializeField] private RectTransform _statsViewContainer;

		[Inject] private IReadOnlyList<PlayerDataValueInfo> _dataValueInfos;
		[Inject] private IPlayerDataInfo _dataInfo;
		[Inject] private StatsViewsPool _statsPool;
		[Inject] private BundlesPool _bundlesPool;
		
		private readonly Dictionary<PlayerDataValueInfo, StatsView> _statsView = new (10);
		private readonly HashSet<Bundle> _bundles = new (10);
		
		public void CreateStatsViews()
		{
			foreach (var valueInfo in _dataValueInfos)
				if (valueInfo != null)
					CreateStatsView(valueInfo);
		}
		
		private void CreateStatsView(PlayerDataValueInfo info)
		{
			var view = _statsPool.Get();
			view.transform.SetParent(_statsViewContainer, false);
			view.SetLabel(info.DisplayName);
			view.SetValue(info.ReadCurrentValueAsString(_dataInfo));
			_statsView.Add(info, view);
		}

		public Bundle CreateBundle(BundleData data)
		{
			var bundle = _bundlesPool.Get();
			bundle.transform.SetParent(_bundlesContainer, false);
			bundle.Initialize(data);
			_bundles.Add(bundle);
			return bundle;
		}
		
		public void UpdateView()
		{
			foreach (var bundle in _bundles)
				bundle.UpdateButtonState();

			foreach (var statsView in _statsView)
				statsView.Value.SetValue(statsView.Key.ReadCurrentValueAsString(_dataInfo));
		}
	}
}