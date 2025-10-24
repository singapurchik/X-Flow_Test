using UnityEngine;
using Zenject;
using Core;

namespace Gold
{
	public class GoldInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataKey _currentGoldKey;
		[SerializeField] private int _startGold = 100;
		
		public override void InstallBindings()
		{
			Container.Bind<Gold>()
				.AsSingle()
				.WithArguments(_currentGoldKey, _startGold)
				.NonLazy();
		}
	}
}