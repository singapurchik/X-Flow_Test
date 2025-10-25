using UnityEngine;
using Core;

namespace Location
{
	[CreateAssetMenu(fileName = "Set Location", menuName = "Location/Operations/Set Location")]
	public sealed class SetLocation : ProvideOperation
	{
		[SerializeField] private PlayerDataKey _currentLocationKey;
		
		[Tooltip("Target location to set. Lobby (enum value 0) is not allowed and will be auto-corrected.")]
		[SerializeField] private LocationType _target;

		public override bool CanApply(IPlayerDataInfo data) => true;

		public override void Apply(PlayerData data)
		{
			if (IsLobby(_target))
			{
				var fixedValue = GetFirstNonLobby();
				_target = fixedValue;
			}

			data.SetString(_currentLocationKey, _target.ToString());
		}
		
		private static bool IsLobby(LocationType value) => value.Equals(default(LocationType));

		private static LocationType GetFirstNonLobby()
		{
			var values = (LocationType[])System.Enum.GetValues(typeof(LocationType));
			
			for (int i = 0; i < values.Length; i++)
				if (!IsLobby(values[i]))
					return values[i];
			
			return default;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (IsLobby(_target))
			{
				var fixedValue = GetFirstNonLobby();
				
				if (IsLobby(fixedValue))
				{
					Debug.Log("[SetLocation] No non-Lobby enum values found. Please add at least one non-zero LocationType.");
				}
				else
				{
					_target = fixedValue;
					Debug.Log($"[SetLocation] Lobby (0) is not allowed in the asset. Auto-corrected to '{_target}'.");
				}
			}
		}
#endif
	}
}