using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Shop
{
	[CreateAssetMenu(fileName = "Bundle Data", menuName = "Shop/Bundle Data")]
	public class BundleData : ScriptableObject
	{
		[Header("Costs")]   [SerializeField] private List<CostEntry> _costs   = new();
		[Header("Rewards")] [SerializeField] private List<RewardEntry> _rewards = new();

		public IReadOnlyList<CostEntry>   Costs   => _costs;
		public IReadOnlyList<RewardEntry> Rewards => _rewards;

#if UNITY_EDITOR
		private void OnValidate()
		{
			AutoEnsureParams(_costs);
			AutoEnsureParams(_rewards);

			EnsureParamsAreUnique(_costs);
			EnsureParamsAreUnique(_rewards);

			// NEW: защита от дублей операций
			EnsureNoDuplicateOperations(_costs, "Costs");
			EnsureNoDuplicateOperations(_rewards, "Rewards");
		}

		private static void AutoEnsureParams(List<CostEntry> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				var e = list[i];
				if (e.Operation is IOperationWithParam op)
				{
					if (e.Param == null || !op.Supports(e.Param))
						e.Param = op.CreateDefaultParam();
				}
				list[i] = e;
			}
		}

		private static void AutoEnsureParams(List<RewardEntry> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				var e = list[i];
				if (e.Operation is IOperationWithParam op)
				{
					if (e.Param == null || !op.Supports(e.Param))
						e.Param = op.CreateDefaultParam();
				}
				list[i] = e;
			}
		}

		private static void EnsureParamsAreUnique(List<CostEntry> list)
		{
			var seen = new HashSet<IOperationParam>(ReferenceEqualityComparer<IOperationParam>.Instance);
			for (int i = 0; i < list.Count; i++)
			{
				var e = list[i];
				if (e.Param != null && !seen.Add(e.Param))
					e.Param = CloneParam(e.Param);
				list[i] = e;
			}
		}

		private static void EnsureParamsAreUnique(List<RewardEntry> list)
		{
			var seen = new HashSet<IOperationParam>(ReferenceEqualityComparer<IOperationParam>.Instance);
			for (int i = 0; i < list.Count; i++)
			{
				var e = list[i];
				if (e.Param != null && !seen.Add(e.Param))
					e.Param = CloneParam(e.Param);
				list[i] = e;
			}
		}

		// NEW: защита от дублей по ссылке на Operation (одна и та же операция второй раз в списке)
		private static void EnsureNoDuplicateOperations(List<CostEntry> list, string listName)
		{
			var seen = new HashSet<int>(); // InstanceID операции
			for (int i = 0; i < list.Count; i++)
			{
				var e = list[i];
				if (!e?.Operation) continue;

				int id = e.Operation.GetInstanceID();
				if (!seen.Add(id))
				{
					Debug.LogWarning($"[BundleData] Duplicate operation in {listName} at index {i}: {e.Operation.name}. Slot cleared.");
					e.Operation = null;
					e.Param = null;
					list[i] = e;
				}
			}
		}

		private static void EnsureNoDuplicateOperations(List<RewardEntry> list, string listName)
		{
			var seen = new HashSet<int>();
			for (int i = 0; i < list.Count; i++)
			{
				var e = list[i];
				if (!e?.Operation) continue;

				int id = e.Operation.GetInstanceID();
				if (!seen.Add(id))
				{
					Debug.LogWarning($"[BundleData] Duplicate operation in {listName} at index {i}: {e.Operation.name}. Slot cleared.");
					e.Operation = null;
					e.Param = null;
					list[i] = e;
				}
			}
		}

		private static IOperationParam CloneParam(IOperationParam src)
		{
			var json = JsonUtility.ToJson(src);
			return (IOperationParam)JsonUtility.FromJson(json, src.GetType());
		}

		private sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
		{
			public static readonly ReferenceEqualityComparer<T> Instance = new();
			public bool Equals(T x, T y) => ReferenceEquals(x, y);
			public int GetHashCode(T obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
		}
#endif
	}
}
