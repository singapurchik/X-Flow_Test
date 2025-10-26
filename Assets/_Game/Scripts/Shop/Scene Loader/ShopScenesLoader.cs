using Core;

namespace Shop
{
	public class ShopScenesLoader : SceneLoaderBase, IShopScenesLoader
	{
		private readonly SceneLoadingData _bundleDetailed;
		private readonly SceneLoadingData _shop;

		public ShopScenesLoader(SceneLoadingData bundleDetailed, SceneLoadingData shop)
		{
			_bundleDetailed = bundleDetailed;
			_shop = shop;
		}
		
		public void LoadBundleDetailedScene() => LoadScene(_bundleDetailed);
		
		public void LoadShopScene() => LoadScene(_shop);
	}
}