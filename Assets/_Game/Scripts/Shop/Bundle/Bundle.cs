using UnityEngine;
using Zenject;
using System;
using Core;
using TMPro;

namespace Shop
{
	public class Bundle : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _costsRewardsText;
		[SerializeField] private BundleBuyButton _buyButton;

		[Inject] private BundlePhraseFormatter _formatter;
		[Inject] private IPlayerDataInfo _playerData;

		private BundleData _currentData;

		public event Action<BundleData> OnBuyButtonClicked;
		public event Action<Bundle> OnBundleOutOfStock;

		public void Initialize(BundleData data)
		{
			_buyButton.AddListenerOnClick(InvokeOnBuyButtonClicked);
			
			_currentData = data;
			_costsRewardsText.text =
				_formatter.Build(new CostOpsView(data.Costs), new RewardOpsView(data.Rewards));

			UpdateButtonState();
		}

		public void UpdateButtonState()
		{
			if (CanPay(_currentData, _playerData))
				_buyButton.Enable();
			else
				_buyButton.Disable();
		}

		private static bool CanPay(BundleData data, IPlayerDataInfo info)
		{
			foreach (var e in data.Costs)
			{
				if (!CanApplyConsumeEntry(e, info))
					return false;
			}
			return true;
		}
		
		private static bool CanApplyConsumeEntry(CostEntry entry, IPlayerDataInfo info)
		{
			if (!entry.Operation)
				return false;

			if (entry.Operation is IOperationWithParameter opWithParam && entry.Param != null)
			{
				if (!opWithParam.IsSupports(entry.Param))
				{
					Debug.LogWarning($"[Shop] Param of type {entry.Param.GetType().Name} " +
					                 $"is not supported by {entry.Operation.name}. Fallback to default.");
					return entry.Operation.IsCanApply(info);
				}

				return opWithParam.IsCanApply(info, entry.Param);
			}

			return entry.Operation.IsCanApply(info);
		}
		
		private void BundleOutOfStock()
		{
			_buyButton.RemoveListenerOnClick(InvokeOnBuyButtonClicked);
			OnBundleOutOfStock?.Invoke(this);
		}
		
		private void InvokeOnBuyButtonClicked() => OnBuyButtonClicked?.Invoke(_currentData);
	}
}