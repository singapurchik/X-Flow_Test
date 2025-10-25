using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Shop
{
	[Serializable]
	public class BundleBuyButton
	{
		[SerializeField] private TextMeshProUGUI _buyButtonText;
		[SerializeField] private Button _button;
		
		private const string BUY_TEXT = "Buy";
		
		public void Enable()
		{
			_button.interactable = true;
			_buyButtonText.text = BUY_TEXT;
		}

		public void Disable()
		{
			_button.interactable = false;
		}
		
		public void RemoveListenerOnClick(UnityAction caller) => _button.onClick.RemoveListener(caller);
		
		public void AddListenerOnClick(UnityAction caller) => _button.onClick.AddListener(caller);
	}
}