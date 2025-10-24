using Zenject;

namespace Core
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<PlayerData>().AsSingle().NonLazy();
		}
	}
}