using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core;

namespace Shop
{
	public sealed class ShopInstaller : MonoInstaller
	{
		[SerializeField] private StatsViewsPool _statsViewsPool;
		[SerializeField] private BundlesPool _bundlesPool;
		[SerializeField] private ShopEntry _entry;
		[SerializeField] private ShopView _view;
		[SerializeField] private List<BundleData> _bundles = new ();
		[SerializeField] private List<PlayerDataValueInfo> _dataValueInfos = new ();

		public override void InstallBindings()
		{
			var backend = new Backend();
			var shop = new Shop();
			
			Container.Bind<IReadOnlyList<PlayerDataValueInfo>>().FromInstance(_dataValueInfos).AsSingle();
			Container.Bind<IShopEntryPoint>().FromInstance(shop).WhenInjectedIntoInstance(_entry);
			
			Container.Bind<IShopView>()
				.FromInstance(_view)
				.WhenInjectedIntoInstance(shop);
			
			Container.BindInstance(_statsViewsPool)
				.WhenInjectedIntoInstance(_view);
			
			Container.BindInstance(_bundlesPool)
				.WhenInjectedIntoInstance(_view);
			
			Container.Bind<IReadOnlyList<BundleData>>()
				.FromInstance(_bundles)
				.WhenInjectedIntoInstance(shop);

			Container.Bind<BundlePhraseFormatter>()
				.WhenInjectedInto<Bundle>();

			Container.BindInstance(backend).WhenInjectedIntoInstance(shop);
			Container.Bind<IBackend>().FromInstance(backend).AsSingle();
			
			Container.Inject(shop);
		}
		
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