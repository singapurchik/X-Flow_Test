using UnityEngine;
using Core;

namespace Location
{
	[CreateAssetMenu(fileName = "Reset Location To Default", menuName = "Location/Operations/Reset Location To Default")]
	public sealed class ResetLocationToDefault : ConsumeOperation
	{
		[SerializeField] private PlayerDataKey _currentLocationKey;

		public override bool IsCanApply(IPlayerDataInfo data)
		{
			var current = data.GetString(_currentLocationKey, string.Empty);
			var defaultLocation = default(LocationType).ToString();
			Debug.Log(current != defaultLocation);
			return current != defaultLocation;
		}

		public override void Apply(PlayerData data)
			=> data.SetString(_currentLocationKey, default(LocationType).ToString());
	}
}