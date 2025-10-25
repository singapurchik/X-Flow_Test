using UnityEngine;
using Zenject;

namespace Core
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private CoroutineRunner _coroutineRunner;
		
		public override void InstallBindings()
		{
			Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
			Container.BindInterfacesAndSelfTo<PlayerData>().AsSingle().NonLazy();
		}
	}
}