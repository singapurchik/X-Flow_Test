using UnityEngine;
using Zenject;
using Core;

namespace Shop
{
	public sealed class ShopInstaller : MonoInstaller
	{
		[SerializeField] private BundlesPool _bundlesPool;
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
			
			Container.BindInstance(_bundlesPool);
			Container.BindInstance(_view);

			Container.Bind<Shop>()
				.AsSingle()
				.NonLazy();
		}
	}
}