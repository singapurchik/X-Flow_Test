using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public sealed class PlayerData : IPlayerDataInfo
	{
		private readonly Dictionary<string, string> _strings = new(64);
		private readonly Dictionary<string, float> _floats = new(64);
		private readonly Dictionary<string, bool> _bools = new(64);
		private readonly Dictionary<string, int> _ints = new(64);

		public int GetInt(PlayerDataKey key, int def = 0) => _ints.GetValueOrDefault(key.Id, def);

		public void SetInt(PlayerDataKey key, int value) => _ints[key.Id] = value;

		public float GetFloat(PlayerDataKey key, float def = 0f) => _floats.GetValueOrDefault(key.Id, def);

		public void SetFloat(PlayerDataKey key, float v) => _floats[key.Id] = v;

		public bool GetBool(PlayerDataKey key, bool def = false) => _bools.TryGetValue(key.Id, out var v) && v || def;

		public void SetBool(PlayerDataKey key, bool v) => _bools[key.Id] = v;

		public string GetString(PlayerDataKey key, string def = "")
		{
			Debug.Log($"key: {key.name} id: {key.Id} value: {_strings.GetValueOrDefault(key.Id, def)}");
			return _strings.GetValueOrDefault(key.Id, def);
		}

		public void SetString(PlayerDataKey key, string v) => _strings[key.Id] = v;
	}
}