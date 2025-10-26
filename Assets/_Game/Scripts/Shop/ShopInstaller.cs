using System.Collections.Generic;
using UnityEngine;
using Core.Utils;
using Zenject;
using Core;

namespace Shop
{
	public sealed class ShopInstaller : MonoInstaller
	{
		[Header("Common")]
		[SerializeField] private StatsViewsPool _statsViewsPool;
		[SerializeField] private BundlesPool _bundlesPool;
		[SerializeField] private ShopEntry _entry;
		[SerializeField] private ShopView _view;
		[SerializeField] private List<PlayerDataValueInfo> _dataValueInfos = new();

		[Header("Bundles")]
		[SerializeField] private List<BundleData> _bundles = new();
		[SerializeField] private SelectedBundleDataReference _selectedReference;
		[SerializeField] private BundlesSourceMode _mode = BundlesSourceMode.FullList;
		
		[Header("Scenes")]
		[SerializeField] private SceneLoadingData _bundleDetailedScene;
		[SerializeField] private SceneLoadingData _shopScene;

		public override void InstallBindings()
		{
			var scenesLoader = new ShopScenesLoader(_bundleDetailedScene, _shopScene);
			var backend = new Backend();
			var shop = new Shop();

			Container.QueueForInject(shop);

			switch (_mode)
			{
				case BundlesSourceMode.FullList:
					Container.Bind<IBundleSource>()
						.To<ListBundleSource>()
						.AsSingle()
						.WithArguments((IReadOnlyList<BundleData>)_bundles);
					break;

				case BundlesSourceMode.SingleSelected: 
					
					if (_selectedReference.Data == null)
						_selectedReference.Set(_bundles[0]);

					Container.Bind<IBundleSource>()
						.To<SingleBundleSource>()
						.AsSingle()
						.WithArguments(_selectedReference.Data);
					break;
			}

			Container.Bind<IShopScenesLoader>().FromInstance(scenesLoader).WhenInjectedIntoInstance(shop);
			Container.Bind<IReadOnlyList<PlayerDataValueInfo>>().FromInstance(_dataValueInfos).AsSingle();

			Container.Bind<IShopEntryPoint>().FromInstance(shop).WhenInjectedIntoInstance(_entry);
			Container.BindInstance(_selectedReference).WhenInjectedIntoInstance(shop);

			Container.Bind<IShopView>().FromInstance(_view).WhenInjectedIntoInstance(shop);
			Container.BindInstance(_statsViewsPool).WhenInjectedIntoInstance(_view);
			Container.BindInstance(_bundlesPool).WhenInjectedIntoInstance(_view);

			Container.Bind<BundlePhraseFormatter>().WhenInjectedInto<Bundle>();

			Container.BindInstance(backend).WhenInjectedIntoInstance(shop);
			Container.Bind<IBackend>().FromInstance(backend).AsSingle();
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_dataValueInfos.MakeSlotsEmptyAndPreventDuplicates();
		}
#endif
	}
}