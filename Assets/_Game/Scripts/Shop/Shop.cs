using Zenject;

namespace Shop
{
	public sealed class Shop
	{
		[Inject] private BundlesPool _bundlesPool;
		[Inject] private IHealthInfo _health;
		[Inject] private ShopView _view;

		[Inject]
		private void Construct()
		{
			var bundle = _bundlesPool.Get();
			bundle.transform.SetParent(_view.BundlesContainer, false);
		}
	}
}