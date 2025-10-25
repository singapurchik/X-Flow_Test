using UnityEngine;
using Zenject;
using Core;
using Shop.VIP;

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
		
		[Header("Assign the SAME keys used by Gold")]
		[SerializeField] private PlayerDataKey _goldCurrentKey;
		
		[Header("Assign the SAME keys used by Location")]
		[SerializeField] private PlayerDataKey _currentLocationKey;
		
		[Header("Assign the SAME keys used by VIP")]
		[SerializeField] private PlayerDataKey _vipRemainingTime;

		public override void InstallBindings()
		{
			Container.Bind<IHealthInfo>()
				.To<HealthInfoProxy>()
				.AsSingle()
				.WithArguments(_healthCurrentKey, _healthMaxKey);
			
			Container.Bind<IGoldInfo>()
				.To<GoldInfoProxy>()
				.AsSingle()
				.WithArguments(_goldCurrentKey);
			
			Container.Bind<ILocationInfo>()
				.To<LocationInfoProxy>()
				.AsSingle()
				.WithArguments(_currentLocationKey);
			
			Container.Bind<IVIPInfo>()
				.To<VIPInfoProxy>()
				.AsSingle()
				.WithArguments(_vipRemainingTime);
			
			Container.Bind<IShopView>()
				.FromInstance(_view);
			
			Container.BindInstance(_statsViewsPool)
				.WhenInjectedIntoInstance(_view);
			
			Container.BindInstance(_bundlesPool)
				.WhenInjectedIntoInstance(_view);

			Container.BindInterfacesAndSelfTo<Shop>()
				.AsSingle()
				.NonLazy();
		}
	}
}