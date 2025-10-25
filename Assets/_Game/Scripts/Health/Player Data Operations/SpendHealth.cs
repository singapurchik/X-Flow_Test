// Health/SpendHealth.cs
using UnityEngine;
using Core;

namespace Health
{
	[CreateAssetMenu(fileName = "Spend Health", menuName = "Health/Operations/Spend")]
	public sealed class SpendHealth : ConsumeOperation, IOperationWithParam
	{
		[SerializeField] private PlayerDataKey _hpKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public override bool IsCanApply(IPlayerDataInfo data)
			=> data.GetInt(_hpKey) >= _defaultAmount;

		public override void Apply(PlayerData data)
			=> data.SetInt(_hpKey, data.GetInt(_hpKey) - _defaultAmount);

		public bool CanApply(IPlayerDataInfo data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int need = Mathf.Max(1, p?.Amount ?? _defaultAmount);
			return data.GetInt(_hpKey) >= need;
		}

		public void Apply(PlayerData data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int need = Mathf.Max(1, p?.Amount ?? _defaultAmount);
			data.SetInt(_hpKey, data.GetInt(_hpKey) - need);
		}

		public IOperationParam CreateDefaultParam() => new IntAmountParam { Amount = _defaultAmount };
		public bool Supports(IOperationParam param) => param is IntAmountParam;

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}