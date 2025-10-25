using UnityEngine;
using Core;

namespace Gold
{
	[CreateAssetMenu(fileName = "Add Gold", menuName = "Gold/Operations/Add")]
	public sealed class AddGold : ProvideOperation, IOperationWithParameter
	{
		[SerializeField] private PlayerDataKey _currentGoldKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public IOperationParameter CreateDefaultParam() => new IntAmountParameter { Amount = _defaultAmount };
		
		public bool IsSupports(IOperationParameter parameter) => parameter is IntAmountParameter;
		
		public bool IsCanApply(IPlayerDataInfo data, IOperationParameter parameter) => IsCanApply(data);

		public void Apply(PlayerData data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			data.SetInt(_currentGoldKey,
				data.GetInt(_currentGoldKey) + Mathf.Max(1, intParam?.Amount ?? _defaultAmount));
		}
		
		public override void Apply(PlayerData data)
			=> data.SetInt(_currentGoldKey, data.GetInt(_currentGoldKey) + _defaultAmount);

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}