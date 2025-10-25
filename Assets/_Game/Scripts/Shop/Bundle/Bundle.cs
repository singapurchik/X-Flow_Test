using System.Collections.Generic;
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
			_costsRewardsText.text = _formatter.Build(data.Costs, data.Rewards);

			UpdateButtonState();
		}

		public void UpdateButtonState()
		{
			if (IsCanPay(_currentData.Costs, _playerData))
				_buyButton.Enable();
			else
				_buyButton.Disable();
		}

		private static bool IsCanPay(IReadOnlyList<ConsumeOperation> costs, IPlayerDataInfo playerData)
		{
			for (int i = 0; i < costs.Count; i++)
				if (!costs[i].IsCanApply(playerData))
					return false;
			return true;
		}
		
		private void BundleOutOfStock()
		{
			_buyButton.RemoveListenerOnClick(InvokeOnBuyButtonClicked);
			OnBundleOutOfStock?.Invoke(this);
		}
		
		private void InvokeOnBuyButtonClicked() => OnBuyButtonClicked?.Invoke(_currentData);
	}
}