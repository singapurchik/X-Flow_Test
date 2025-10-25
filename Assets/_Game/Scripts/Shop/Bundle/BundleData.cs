using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Shop
{
	[CreateAssetMenu(fileName = "Bundle Data", menuName = "Shop/Bundle Data", order = 0)]
	public class BundleData : ScriptableObject
	{
		[SerializeField] private List<ConsumeOperation> _costs = new();
		[SerializeField] private List<ProvideOperation> _rewards = new();

		public IReadOnlyList<ProvideOperation> Rewards => _rewards;
		public IReadOnlyList<ConsumeOperation> Costs   => _costs;

#if UNITY_EDITOR
		private void OnValidate()
		{
			MakeSlotsEmptyAndPreventDuplicates(_costs);
			MakeSlotsEmptyAndPreventDuplicates(_rewards);
		}

		private static void MakeSlotsEmptyAndPreventDuplicates<T>(List<T> list) where T : ScriptableObject
		{
			if (list == null || list.Count == 0) return;

			var seen = new HashSet<int>();
			for (int i = 0; i < list.Count; i++)
			{
				var so = list[i];

				if (!so) continue;

				int id = so.GetInstanceID();
				if (!seen.Add(id))
				{
					Debug.Log($"Duplicate asset prevented at index {i}. Slot cleared.");
					list[i] = null;
				}
			}
		}
#endif
	}
}