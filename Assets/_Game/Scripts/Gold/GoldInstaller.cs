using UnityEngine;
using Zenject;
using Core;

namespace Gold
{
	public class GoldInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataKey _currentGoldKey;
		
		public override void InstallBindings()
		{
			Container.Bind<Gold>()
				.AsSingle()
				.WithArguments(_currentGoldKey)
				.NonLazy();
		}
	}
}