using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
	[CreateAssetMenu(fileName = "Bundle Data", menuName = "Shop/Bundle Data")]
	public class BundleData : ScriptableObject
	{
		[Header("Rewards")]
		[SerializeField] private List<RewardEntry> _rewards = new();
		
		[Header("Costs")]
		[SerializeField] private List<CostEntry> _costs = new();

		public IReadOnlyList<RewardEntry> Rewards => _rewards;
		public IReadOnlyList<CostEntry> Costs => _costs;
    }
}
