using UnityEngine;
using System;
using TMPro;

namespace Shop
{
	public class StatsView : MonoBehaviour, IStatsView
	{
		[SerializeField] private TextMeshProUGUI _text;

		private string _label;
		
		public event Action<StatsView> OnHide;

		public void SetLabel(string label) => _label = label + ": ";

		public void SetValue(string value) => _text.text = _label + value;
	}
}