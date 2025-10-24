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

		public void CreateBundle()
		{
			var statsView = _bundlesPool.Get();
			statsView.transform.SetParent(_bundlesContainer, false);
		}

		public IStatsView CreateStatsView()
		{
			var statsView = _statsPool.Get();
			statsView.transform.SetParent(_statsViewContainer, false);
			return statsView;
		}
	}
}