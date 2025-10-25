using UnityEngine;
using Zenject;
using Core;

namespace Location
{
	public class LocationInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataKey _currentLocationKey;
		[SerializeField] private LocationType _startLocation;
		
		public override void InstallBindings()
		{
			Container.Bind<Location>()
				.AsSingle()
				.WithArguments(_currentLocationKey, _startLocation)
				.NonLazy();
		}
	}
}