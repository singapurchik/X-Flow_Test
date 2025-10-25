using UnityEngine;
using Core;

namespace Gold
{
	[CreateAssetMenu(fileName = "Add Gold", menuName = "Gold/Operations/Add")]
	public sealed class AddGold : ProvideOperation, IOperationWithParam
	{
		[SerializeField] private PlayerDataKey _goldKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public override bool IsCanApply(IPlayerDataInfo data) => true;

		public override void Apply(PlayerData data)
			=> data.SetInt(_goldKey, data.GetInt(_goldKey) + _defaultAmount);

		public bool CanApply(IPlayerDataInfo data, IOperationParam param) => true;

		public void Apply(PlayerData data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int add = Mathf.Max(1, p?.Amount ?? _defaultAmount);
			data.SetInt(_goldKey, data.GetInt(_goldKey) + add);
		}

		public IOperationParam CreateDefaultParam() => new IntAmountParam { Amount = _defaultAmount };
		public bool Supports(IOperationParam param) => param is IntAmountParam;

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}