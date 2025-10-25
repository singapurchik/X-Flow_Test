using System.Collections.Generic;
using System.Collections;
using Zenject;
using Core;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private IReadOnlyList<BundleData> _bundlesData;
		[Inject] private ICoroutineRunner _coroutineRunner;
		[Inject] private PlayerData _playerData;
		[Inject] private Backend _backend;
		[Inject] private IShopView _view;

		public void Initialize()
		{
			if (_bundlesData != null && _bundlesData.Count > 0)
			{
				for (var i = 0; i < _bundlesData.Count; i++)
				{
					var bundle = _view.CreateBundle(_bundlesData[i]);
					bundle.OnBundleOutOfStock += OnBundleOutOfStock;
					bundle.OnBuyButtonClicked += OnBuyBundle;
				}
			}

			_view.CreateStatsViews();
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

		private void OnBundleOutOfStock(Bundle bundle)
		{
			bundle.OnBundleOutOfStock -= OnBundleOutOfStock;
			bundle.OnBuyButtonClicked -= OnBuyBundle;
		}
	}
}