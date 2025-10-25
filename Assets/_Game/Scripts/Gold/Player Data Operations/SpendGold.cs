using UnityEngine;
using Core;

namespace Gold
{
	[CreateAssetMenu(fileName = "Spend Gold", menuName = "Gold/Operations/Spend")]
	public sealed class SpendGold : ConsumeOperation, IOperationWithParameter
	{
		[SerializeField] private PlayerDataKey _currentGoldKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public IOperationParameter CreateDefaultParam() => new IntAmountParameter { Amount = _defaultAmount };
		
		public bool IsSupports(IOperationParameter parameter) => parameter is IntAmountParameter;
		
		public bool IsCanApply(IPlayerDataInfo data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			return data.GetInt(_currentGoldKey) >= Mathf.Max(1, intParam?.Amount ?? _defaultAmount);
		}
		public override bool IsCanApply(IPlayerDataInfo data) => data.GetInt(_currentGoldKey) >= _defaultAmount;

		public void Apply(PlayerData data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			data.SetInt(_currentGoldKey,
				data.GetInt(_currentGoldKey) - Mathf.Max(1, intParam?.Amount ?? _defaultAmount));
		}
		
		public override void Apply(PlayerData data)
			=> data.SetInt(_currentGoldKey, data.GetInt(_currentGoldKey) - _defaultAmount);

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}