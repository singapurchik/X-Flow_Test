using UnityEngine;

namespace Core
{
	public abstract class PlayerDataOperation : ScriptableObject, IPlayerDataOperationInfo
	{
		[SerializeField] private PlayerDataValueInfo _info;

		public PlayerDataValueInfo Info => _info;
		
		public abstract bool IsCanApply(IPlayerDataInfo data);
		
		public abstract void Apply(PlayerData data);
	}
}