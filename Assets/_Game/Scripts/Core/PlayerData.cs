using System.Collections.Generic;
using System;

namespace Core
{
	public static class PlayerData
	{
		private static readonly Dictionary<string, string> _strings = new(64);
		private static readonly Dictionary<string, float> _floats = new(64);
		private static readonly Dictionary<string, bool> _bools = new(64);
		private static readonly Dictionary<string, int> _ints = new(64);

		public static event Action<PlayerDataKey> OnValueChanged;

		public static int GetInt(PlayerDataKey key, int def = 0) => _ints.GetValueOrDefault(key.Id, def);

		public static void SetInt(PlayerDataKey key, int value)
		{
			_ints[key.Id] = value;
			OnValueChanged?.Invoke(key);
		}

		public static float GetFloat(PlayerDataKey key, float def = 0f) => _floats.GetValueOrDefault(key.Id, def);

		public static void SetFloat(PlayerDataKey key, float v)
		{
			_floats[key.Id] = v;
			OnValueChanged?.Invoke(key);
		}

		public static bool GetBool(PlayerDataKey key, bool def = false) => _bools.TryGetValue(key.Id, out var v) && v || def;

		public static void SetBool(PlayerDataKey key, bool v)
		{
			_bools[key.Id] = v;
			OnValueChanged?.Invoke(key);
		}

		public static string GetString(PlayerDataKey key, string def = "") => _strings.GetValueOrDefault(key.Id, def);

		public static void SetString(PlayerDataKey key, string v)
		{
			_strings[key.Id] = v;
			OnValueChanged?.Invoke(key);
		}
	}
}