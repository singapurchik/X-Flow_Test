#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Shop
{
	[CreateAssetMenu(fileName = "Bundle Data", menuName = "Shop/Bundle Data")]
	public class BundleData : ScriptableObject
	{
		[Header("Costs")]
		[SerializeField] private List<CostEntry> _costs = new();

		[Header("Rewards")]
		[SerializeField] private List<RewardEntry> _rewards = new();

		public IReadOnlyList<CostEntry> Costs => _costs;
		public IReadOnlyList<RewardEntry> Rewards => _rewards;
		
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (SerializationUtility.HasManagedReferencesWithMissingTypes(this))
            {
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(this);
                EditorUtility.SetDirty(this);
            }

            bool changed = false;

            changed |= AutoEnsureParams(_costs);
            changed |= AutoEnsureParams(_rewards);

            changed |= EnsureParamsAreUnique(_costs);
            changed |= EnsureParamsAreUnique(_rewards);

            changed |= EnsureNoDuplicateOperations(_costs, "Costs");
            changed |= EnsureNoDuplicateOperations(_rewards, "Rewards");

            if (changed) EditorUtility.SetDirty(this);
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
                    if (e.Param != null) { e.Param = null; changed = true; }
                }
                else if (e.Operation is IOperationWithParameter op)
                {
                    if (e.Param == null || !op.IsSupports(e.Param))
                    {
                        e.Param = op.CreateDefaultParam(); // новый экземпляр
                        changed = true;
                    }
                }
                else
                {
                    if (e.Param != null) { e.Param = null; changed = true; }
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
                    if (e.Param != null) { e.Param = null; changed = true; }
                }
                else if (e.Operation is IOperationWithParameter op)
                {
                    if (e.Param == null || !op.IsSupports(e.Param))
                    {
                        e.Param = op.CreateDefaultParam();
                        changed = true;
                    }
                }
                else
                {
                    if (e.Param != null) { e.Param = null; changed = true; }
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
                if (e?.Param != null && !seen.Add(e.Param))
                {
                    e.Param = CloneParam(e.Param);
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
                if (e?.Param != null && !seen.Add(e.Param))
                {
                    e.Param = CloneParam(e.Param);
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
                    Debug.LogWarning($"[BundleData] Duplicate operation in {listName} at index {i}: {e.Operation.name}. Slot cleared.");
                    e.Operation = null;
                    e.Param = null;
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
                    Debug.LogWarning($"[BundleData] Duplicate operation in {listName} at index {i}: {e.Operation.name}. Slot cleared.");
                    e.Operation = null;
                    e.Param = null;
                    list[i] = e;
                    changed = true;
                }
            }
            return changed;
        }

        private static IOperationParameter CloneParam(IOperationParameter src)
        {
            var json = JsonUtility.ToJson(src);
            return (IOperationParameter)JsonUtility.FromJson(json, src.GetType());
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
