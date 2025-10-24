using UnityEngine;
using Zenject;

namespace Shop
{
	public sealed class ShopView : MonoBehaviour
	{
		[SerializeField] private RectTransform _bundlesContainer;
		
		[Inject] private StatsViewsPool _statsPool;

		public void AddBundle(Transform bundle) => bundle.SetParent(_bundlesContainer, false);

		public IStatsView CreateStatsView() => _statsPool.Get();
	}
}