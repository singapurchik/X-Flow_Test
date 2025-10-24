using Zenject;

namespace Core
{
	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<PlayerData>().AsSingle().NonLazy();
		}
	}
}