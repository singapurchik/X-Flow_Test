using UnityEngine;

namespace Core
{
	public abstract class PlayerDataValue : ScriptableObject
	{
		public abstract void Initialize(PlayerData data);
	}
}