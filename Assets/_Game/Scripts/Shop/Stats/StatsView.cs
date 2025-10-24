using UnityEngine.UI;
using UnityEngine;
using System;

namespace Shop
{
	public class StatsView : MonoBehaviour, IStatsView
	{
		[SerializeField] private Text _text;

		private string _label;
		
		public event Action<StatsView> OnHide;

		public void SetLabel(string name)
		{
			_label = name + ": ";
		}

		public void SetValue(string value)
		{
			_text.text = _label + value;
		}
	}
}