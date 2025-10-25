using UnityEngine;
using Zenject;
using Core;

namespace Health
{
	public class HealthInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataKey _currentHealthKey;
		[SerializeField] private int _startHealth = 100;

		public override void InstallBindings()
		{
			Container.Bind<Health>()
				.AsSingle()
				.WithArguments(_currentHealthKey, _startHealth)
				.NonLazy(); 
		}
	}
}