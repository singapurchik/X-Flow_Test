using System.Collections.Generic;

namespace Shop
{
	public sealed class ShopFullInstaller : ShopInstallerBase
	{
		protected override void InstallBundles()
		{
			Container.Bind<IBundleSource>()
				.To<ListBundleSource>()
				.AsSingle()
				.WithArguments((IReadOnlyList<BundleData>)Bundles);
		}
	}
}