using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shop
{
	[CreateAssetMenu(fileName = "Bundle Data", menuName = "Shop/Bundle Data", order = 0)]
	public class BundleData : ScriptableObject
	{
		[SerializeField] private List<BundleCurrencyType> _costs = new();
		[SerializeField] private List<BundleCurrencyType> _rewards = new();

		public IReadOnlyList<BundleCurrencyType> Rewards => _rewards;
		public IReadOnlyList<BundleCurrencyType> Costs => _costs;

#if UNITY_EDITOR
		private void OnValidate()
		{
			FixUniqueEnumList(_costs);
			FixUniqueEnumList(_rewards);
		}

		private static void FixUniqueEnumList(List<BundleCurrencyType> list)
		{
			if (list != null && list.Count > 0)
			{
				var allValues = (BundleCurrencyType[])Enum.GetValues(typeof(BundleCurrencyType));
				var used = new HashSet<BundleCurrencyType>(list.Count);

				for (int i = 0; i < list.Count; i++)
				{
					var value = list[i];

					if (!used.Add(value))
					{
						bool assigned = false;
						
						for (int j = 0; j < allValues.Length; j++)
						{
							var candidate = allValues[j];
							if (used.Contains(candidate)) continue;

							list[i] = candidate;
							used.Add(candidate);
							assigned = true;
							break;
						}

						if (!assigned)
						{
							list.RemoveRange(i, list.Count - i);
							break;
						}	
					}
				}
			}
		}
#endif
	}
}