using System.Collections.Generic;
using Core;
using UnityEngine;
using Zenject;

namespace Shop
{
	public sealed class Shop : IShopEntryPoint
	{
		[Inject] private IReadOnlyList<BundleData> _bundlesData;
		[Inject] private PlayerData _playerData;
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
			foreach (var e in data.Costs)
				ApplyConsumeEntry(e, _playerData);

			foreach (var e in data.Rewards)
				ApplyProvideEntry(e, _playerData);

			_view.UpdateView();
		}
		
		private static void ApplyConsumeEntry(CostEntry entry, PlayerData data)
		{
			if (!entry.Operation)
				return;

			if (entry.Operation is IOperationWithParam opWithParam && entry.Param != null)
			{
				if (opWithParam.Supports(entry.Param))
				{
					opWithParam.Apply(data, entry.Param);
					return;
				}

				Debug.LogWarning($"[Shop] Param of type {entry.Param.GetType().Name} " +
				                 $"is not supported by {entry.Operation.name}. Fallback to default.");
			}

			entry.Operation.Apply(data);
		}

		private static void ApplyProvideEntry(RewardEntry entry, PlayerData data)
		{
			if (!entry.Operation)
				return;

			if (entry.Operation is IOperationWithParam opWithParam && entry.Param != null)
			{
				if (opWithParam.Supports(entry.Param))
				{
					opWithParam.Apply(data, entry.Param);
					return;
				}

				Debug.LogWarning($"[Shop] Param of type {entry.Param.GetType().Name} " +
				                 $"is not supported by {entry.Operation.name}. Fallback to default.");
			}

			entry.Operation.Apply(data);
		}

		private void OnBundleOutOfStock(Bundle bundle)
		{
			bundle.OnBundleOutOfStock -= OnBundleOutOfStock;
			bundle.OnBuyButtonClicked -= OnBuyBundle;
		}
	}
}