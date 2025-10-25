using UnityEngine;
using Core;

namespace Gold
{
	[CreateAssetMenu(fileName = "Spend Gold", menuName = "Gold/Operations/Spend")]
	public sealed class SpendGold : ConsumeOperation
	{
		[SerializeField] private PlayerDataKey _currentGoldKey;
		[Min(1)][SerializeField] private int _amount = 1;

		public override bool IsCanApply(IPlayerDataInfo data) => data.GetInt(_currentGoldKey) >= _amount;
		
		public override void Apply(PlayerData data)
			=> data.SetInt(_currentGoldKey, data.GetInt(_currentGoldKey) - _amount);
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			_amount = Mathf.Max(1, _amount);
		}
#endif
	}
}