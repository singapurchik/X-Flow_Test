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
		
		[Header("Assign the SAME keys used by Health")]
		[SerializeField] private PlayerDataKey _healthCurrentKey;
		[SerializeField] private PlayerDataKey _healthMaxKey;

		public override void InstallBindings()
		{
			Container.Bind<IHealthInfo>()
				.To<HealthInfoProxy>()
				.AsSingle()
				.WithArguments(_healthCurrentKey, _healthMaxKey);
			
			Container.BindInstance(_statsViewsPool).WhenInjectedIntoInstance(_view);
			Container.BindInstance(_bundlesPool);
			Container.BindInstance(_view);

			Container.BindInterfacesAndSelfTo<Shop>()
				.AsSingle()
				.NonLazy();
		}
	}
}