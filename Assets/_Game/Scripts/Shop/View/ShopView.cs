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

		public void CreateBundle(BundleData data)
		{
			var bundle = _bundlesPool.Get();
			bundle.transform.SetParent(_bundlesContainer, false);
			bundle.Initialize(data);
		}

		public IStatsView CreateStatsView()
		{
			var statsView = _statsPool.Get();
			statsView.transform.SetParent(_statsViewContainer, false);
			return statsView;
		}
	}
}