using System.Collections;
using Zenject;
using Core;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private SelectedBundleDataReference _selectedBundleDataReference;
		[Inject] private ICoroutineRunner _coroutineRunner;
		[Inject] private IShopScenesLoader _scenesLoader;
		[Inject] private IBundleSource _bundleSource;
		[Inject] private PlayerData _playerData;
		[Inject] private Backend _backend;
		[Inject] private IShopView _view;

		public void Initialize()
		{
			var bundles = _bundleSource.GetBundles();
			if (bundles != null && bundles.Count > 0)
			{
				for (var i = 0; i < bundles.Count; i++)
				{
					var bundle = _view.CreateBundle(bundles[i]);
					bundle.OnInfoButtonClicked += OnBundleInfoButtonClicked;
					bundle.OnBundleOutOfStock += OnBundleOutOfStock;
					bundle.OnBuyButtonClicked += OnBuyBundle;
				}
			}

			_view.CreateStatsViews();
		}
		
		private void OnBundleInfoButtonClicked(BundleData data)
		{
			_view.DisableInput();
			_selectedBundleDataReference.Set(data);
			_scenesLoader.LoadBindleDetailedScene();
		}

		private void OnBundleOutOfStock(Bundle bundle)
		{
			bundle.OnInfoButtonClicked -= OnBundleInfoButtonClicked;
			bundle.OnBundleOutOfStock -= OnBundleOutOfStock;
			bundle.OnBuyButtonClicked -= OnBuyBundle;
		}
		
		private void OnBuyBundle(BundleData data)
		{
			if (!_backend.IsBusy)
				_coroutineRunner.Run(BuyRoutine(data));
		}
		
		private IEnumerator BuyRoutine(BundleData data)
		{
			_view.DisableInput();
			
			yield return _backend.SendRequestRoutine();

			foreach (var costEntry in data.Costs)
				ApplyConsumeEntry(costEntry, _playerData);

			foreach (var rewardEntry in data.Rewards)
				ApplyProvideEntry(rewardEntry, _playerData);

			_view.UpdateView();
			_view.EnableInput();
		}
		
		private static void ApplyConsumeEntry(CostEntry entry, PlayerData data)
		{
			if (entry.Operation)
			{
				if (entry.Operation is IOperationWithParameter operationWithParameter && entry.Parameter != null)
				{
					if (operationWithParameter.IsSupports(entry.Parameter))
					{
						operationWithParameter.Apply(data, entry.Parameter);
						return;
					}
				}

				entry.Operation.Apply(data);	
			}
		}

		private static void ApplyProvideEntry(RewardEntry entry, PlayerData data)
		{
			if (entry.Operation)
			{
				if (entry.Operation is IOperationWithParameter operationWithParameter && entry.Parameter != null)
				{
					if (operationWithParameter.IsSupports(entry.Parameter))
					{
						operationWithParameter.Apply(data, entry.Parameter);
						return;
					}
				}

				entry.Operation.Apply(data);	
			}
		}
	}
}