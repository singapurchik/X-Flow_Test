using UnityEngine;
using Core;

namespace Health
{
	[CreateAssetMenu(fileName = "Spend Health", menuName = "Health/Operations/Spend Health")]
	public sealed class SpendHealth : ConsumeOperation
	{
		[SerializeField] private PlayerDataKey _currentHealthKey;
		[Min(1)][SerializeField] private int _amount = 1;

		public override bool IsCanApply(IPlayerDataInfo data) => data.GetInt(_currentHealthKey) >= _amount;
		
		public override void Apply(PlayerData data)
		{
			data.SetInt(_currentHealthKey, Mathf.Max(0, data.GetInt(_currentHealthKey) - _amount));	
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_amount = Mathf.Max(1, _amount);
		}
#endif
	}
}