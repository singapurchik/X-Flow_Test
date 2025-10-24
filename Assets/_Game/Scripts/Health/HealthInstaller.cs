using UnityEngine;
using Zenject;
using Core;

namespace Health
{
	public class HealthInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataKey _currentHealthKey;
		[SerializeField] private PlayerDataKey _maxHealthKey;
		[SerializeField] private int _maxHealth = 100;

		public override void InstallBindings()
		{
			Container.Bind<Health>()
				.AsSingle()
				.WithArguments(_currentHealthKey, _maxHealthKey, _maxHealth)
				.NonLazy(); 
		}
	}
}