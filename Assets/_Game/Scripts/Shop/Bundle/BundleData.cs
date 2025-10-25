using System.Collections.Generic;
using UnityEngine;
using Core;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shop
{
	[CreateAssetMenu(fileName = "Bundle Data", menuName = "Shop/Bundle Data")]
	public class BundleData : ScriptableObject
	{
		[SerializeField] private int _amount = 3;
		
		[Header("Costs")]
		[SerializeField] private List<CostEntry> _costs = new();

		[Header("Rewards")]
		[SerializeField] private List<RewardEntry> _rewards = new();

		public IReadOnlyList<RewardEntry> Rewards => _rewards;
		public IReadOnlyList<CostEntry> Costs => _costs;
		
		public int Amount => _amount;
		
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (SerializationUtility.HasManagedReferencesWithMissingTypes(this))
            {
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(this);
                EditorUtility.SetDirty(this);
            }

            var changed = false;

            changed |= AutoEnsureParams(_costs);
            changed |= AutoEnsureParams(_rewards);

            changed |= EnsureParamsAreUnique(_costs);
            changed |= EnsureParamsAreUnique(_rewards);

            changed |= EnsureNoDuplicateOperations(_costs, "Costs");
            changed |= EnsureNoDuplicateOperations(_rewards, "Rewards");

            if (changed)
	            EditorUtility.SetDirty(this);
        }

        private static bool AutoEnsureParams(List<CostEntry> list)
        {
            bool changed = false;
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e == null) continue;

                if (!e.Operation)
                {
                    if (e.Parameter != null) { e.Parameter = null; changed = true; }
                }
                else if (e.Operation is IOperationWithParameter op)
                {
                    if (e.Parameter == null || !op.IsSupports(e.Parameter))
                    {
                        e.Parameter = op.CreateDefaultParam();
                        changed = true;
                    }
                }
                else
                {
                    if (e.Parameter != null) { e.Parameter = null; changed = true; }
                }
                list[i] = e;
            }
            return changed;
        }

        private static bool AutoEnsureParams(List<RewardEntry> list)
        {
            bool changed = false;
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e == null) continue;

                if (!e.Operation)
                {
                    if (e.Parameter != null) { e.Parameter = null; changed = true; }
                }
                else if (e.Operation is IOperationWithParameter op)
                {
                    if (e.Parameter == null || !op.IsSupports(e.Parameter))
                    {
                        e.Parameter = op.CreateDefaultParam();
                        changed = true;
                    }
                }
                else
                {
                    if (e.Parameter != null) { e.Parameter = null; changed = true; }
                }
                list[i] = e;
            }
            return changed;
        }

        private static bool EnsureParamsAreUnique(List<CostEntry> list)
        {
            bool changed = false;
            var seen = new HashSet<IOperationParameter>(ReferenceEqualityComparer<IOperationParameter>.Instance);
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e?.Parameter != null && !seen.Add(e.Parameter))
                {
                    e.Parameter = CloneParam(e.Parameter);
                    changed = true;
                }
                list[i] = e;
            }
            return changed;
        }

        private static bool EnsureParamsAreUnique(List<RewardEntry> list)
        {
            bool changed = false;
            var seen = new HashSet<IOperationParameter>(ReferenceEqualityComparer<IOperationParameter>.Instance);
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e?.Parameter != null && !seen.Add(e.Parameter))
                {
                    e.Parameter = CloneParam(e.Parameter);
                    changed = true;
                }
                list[i] = e;
            }
            return changed;
        }

        private static bool EnsureNoDuplicateOperations(List<CostEntry> list, string listName)
        {
            bool changed = false;
            var seen = new HashSet<int>();
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (!e?.Operation) continue;

                int id = e.Operation.GetInstanceID();
                if (!seen.Add(id))
                {
                    Debug.LogWarning($"[BundleData] Duplicate operation in {listName}" +
                                     $" at index {i}: {e.Operation.name}. Slot cleared.");
                    e.Operation = null;
                    e.Parameter = null;
                    list[i] = e;
                    changed = true;
                }
            }
            return changed;
        }

        private static bool EnsureNoDuplicateOperations(List<RewardEntry> list, string listName)
        {
            bool changed = false;
            var seen = new HashSet<int>();
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (!e?.Operation) continue;

                int id = e.Operation.GetInstanceID();
                if (!seen.Add(id))
                {
                    Debug.LogWarning($"[BundleData] Duplicate operation in {listName}" +
                                     $" at index {i}: {e.Operation.name}. Slot cleared.");
                    e.Operation = null;
                    e.Parameter = null;
                    list[i] = e;
                    changed = true;
                }
            }
            return changed;
        }

        private static IOperationParameter CloneParam(IOperationParameter src)
			=> (IOperationParameter)JsonUtility.FromJson(JsonUtility.ToJson(src), src.GetType());
#endif
    }
}
