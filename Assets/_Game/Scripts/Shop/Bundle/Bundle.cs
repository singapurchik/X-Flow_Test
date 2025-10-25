using UnityEngine;
using Zenject;
using System;
using TMPro;

namespace Shop
{
	public class Bundle : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _costsRewardsText;

		[Inject] private BundlePhraseFormatter _formatter;

		private BundleData _currentData;

		public event Action<Bundle> OnBundleOutOfStock;

		public void Initialize(BundleData data)
		{
			_currentData = data;
			_costsRewardsText.text = _formatter.Build(data.Costs, data.Rewards);
		}
	}
}