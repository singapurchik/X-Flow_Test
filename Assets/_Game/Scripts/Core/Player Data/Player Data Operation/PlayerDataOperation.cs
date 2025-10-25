using UnityEngine;

namespace Core
{
	public abstract class PlayerDataOperation : ScriptableObject, IPlayerDataOperationInfo
	{
		[SerializeField] private ResourceDescriptor _descriptor;

		public ResourceDescriptor Descriptor => _descriptor;
		
		public abstract bool CanApply(IPlayerDataInfo data);
		
		public abstract void Apply(PlayerData data);
	}
}