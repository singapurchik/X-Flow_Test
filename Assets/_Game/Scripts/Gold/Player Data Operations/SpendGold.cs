using UnityEngine;
using Core;

namespace Gold
{
	[CreateAssetMenu(fileName = "Spend Gold", menuName = "Gold/Operations/Spend")]
	public sealed class SpendGold : ConsumeOperation, IOperationWithParam
	{
		[SerializeField] private PlayerDataKey _goldKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public override bool IsCanApply(IPlayerDataInfo data)
			=> data.GetInt(_goldKey) >= _defaultAmount;

		public override void Apply(PlayerData data)
			=> data.SetInt(_goldKey, data.GetInt(_goldKey) - _defaultAmount);

		public bool CanApply(IPlayerDataInfo data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int need = Mathf.Max(1, p?.Amount ?? _defaultAmount);
			return data.GetInt(_goldKey) >= need;
		}

		public void Apply(PlayerData data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int need = Mathf.Max(1, p?.Amount ?? _defaultAmount);
			data.SetInt(_goldKey, data.GetInt(_goldKey) - need);
		}

		public IOperationParam CreateDefaultParam() => new IntAmountParam { Amount = _defaultAmount };
		public bool Supports(IOperationParam param) => param is IntAmountParam;

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}