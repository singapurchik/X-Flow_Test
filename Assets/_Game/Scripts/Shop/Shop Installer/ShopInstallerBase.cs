using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core;

namespace Shop
{
	public abstract class ShopInstallerBase : MonoInstaller
	{
		[SerializeField] private StatsViewsPool _statsViewsPool;
		[SerializeField] private BundlesPool _bundlesPool;
		[SerializeField] private ShopEntry _entry;
		[SerializeField] private ShopView _view;
		[SerializeField] private List<PlayerDataValueInfo> _dataValueInfos = new ();
		
		[SerializeField] protected List<BundleData> Bundles = new ();
		[SerializeField] protected SelectedBundleDataReference SelectedBundleDataReference;
		
		[Header("Scenes")]
		[SerializeField] private SceneLoadingData _shopBundleDetailedSceneLoadingData;
		[SerializeField] private SceneLoadingData _shopSceneLoadingData;

		public override void InstallBindings()
		{
			var scenesLoader = new ShopScenesLoader(_shopBundleDetailedSceneLoadingData, _shopSceneLoadingData);
			var backend = new Backend();
			var shop = new Shop();
			
			Container.QueueForInject(shop);
			
			Container.Bind<IShopScenesLoader>().FromInstance(scenesLoader).WhenInjectedIntoInstance(shop);
			Container.Bind<IReadOnlyList<PlayerDataValueInfo>>().FromInstance(_dataValueInfos).AsSingle();
			Container.Bind<IShopEntryPoint>().FromInstance(shop).WhenInjectedIntoInstance(_entry);
			Container.BindInstance(SelectedBundleDataReference).WhenInjectedIntoInstance(shop);
			
			Container.Bind<IShopView>()
				.FromInstance(_view)
				.WhenInjectedIntoInstance(shop);
			
			Container.BindInstance(_statsViewsPool)
				.WhenInjectedIntoInstance(_view);
			
			Container.BindInstance(_bundlesPool)
				.WhenInjectedIntoInstance(_view);
			
			Container.Bind<BundlePhraseFormatter>()
				.WhenInjectedInto<Bundle>();

			Container.BindInstance(backend).WhenInjectedIntoInstance(shop);
			Container.Bind<IBackend>().FromInstance(backend).AsSingle();
			
			InstallBundles();
		}

		protected abstract void InstallBundles();
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			MakeSlotsEmptyAndPreventDuplicates(_dataValueInfos);
		}

		private static void MakeSlotsEmptyAndPreventDuplicates<T>(List<T> list) where T : ScriptableObject
		{
			if (list != null && list.Count > 0)
			{
				var seen = new HashSet<int>();
				
				for (int i = 0; i < list.Count; i++)
				{
					var scriptableObject = list[i];

					if (scriptableObject)
					{
						int id = scriptableObject.GetInstanceID();
						if (!seen.Add(id))
						{
							Debug.Log($"Duplicate asset prevented at index {i}. Slot cleared.");
							list[i] = null;
						}	
					}
				}	
			}
		}
#endif
	}
}