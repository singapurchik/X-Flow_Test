namespace Shop
{
	public sealed class ShopBundleDetailedInstaller : ShopInstallerBase
	{
		protected override void InstallBundles()
		{
			if (SelectedBundleDataReference.Data == null)
				SelectedBundleDataReference.Set(Bundles[0]);
			
			Container.Bind<IBundleSource>()
				.To<SingleBundleSource>()
				.AsSingle()
				.WithArguments(SelectedBundleDataReference.Data);
		}
	}
}