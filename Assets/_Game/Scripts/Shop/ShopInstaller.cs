using UnityEngine;
using Zenject;
using Core;

namespace Shop
{
	public sealed class ShopInstaller : MonoInstaller
	{
		[Header("Assign the SAME keys used by Health")]
		[SerializeField] private PlayerDataKey _healthCurrentKey;
		[SerializeField] private PlayerDataKey _healthMaxKey;

		public override void InstallBindings()
		{
			Container.Bind<IHealthInfo>()
				.To<HealthInfoProxy>()
				.AsSingle()
				.WithArguments(_healthCurrentKey, _healthMaxKey);

			Container.Bind<Shop>()
				.AsSingle()
				.NonLazy();
		}
	}
}