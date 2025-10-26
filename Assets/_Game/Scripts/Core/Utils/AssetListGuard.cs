using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
	public static class AssetListGuard
	{
		public static bool MakeSlotsEmptyAndPreventDuplicates<T>(this IList<T> list, string logPrefix = null)
			where T : ScriptableObject
		{
			if (list == null || list.Count == 0) return false;

			bool changed = false;
			var seen = new HashSet<int>();

			for (int i = 0; i < list.Count; i++)
			{
				var so = list[i];
				if (!so) continue;

				int id = so.GetInstanceID();
				if (!seen.Add(id))
				{
					if (!string.IsNullOrEmpty(logPrefix))
						Debug.Log($"{logPrefix}: duplicate asset prevented at index {i}. Slot cleared.");
					else
						Debug.Log($"Duplicate asset prevented at index {i}. Slot cleared.");

					list[i] = null;
					changed = true;
				}
			}

			return changed;
		}
	}
}