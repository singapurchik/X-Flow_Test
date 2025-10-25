// Health/AddHealth.cs
using UnityEngine;
using Core;

namespace Health
{
	[CreateAssetMenu(fileName = "Add Health", menuName = "Health/Operations/Add")]
	public sealed class AddHealth : ProvideOperation, IOperationWithParam
	{
		[SerializeField] private PlayerDataKey _hpKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public override bool IsCanApply(IPlayerDataInfo data) => true;

		public override void Apply(PlayerData data)
			=> data.SetInt(_hpKey, data.GetInt(_hpKey) + _defaultAmount);

		public bool CanApply(IPlayerDataInfo data, IOperationParam param) => true;

		public void Apply(PlayerData data, IOperationParam param)
		{
			var p = param as IntAmountParam;
			int add = Mathf.Max(1, p?.Amount ?? _defaultAmount);
			data.SetInt(_hpKey, data.GetInt(_hpKey) + add);
		}

		public IOperationParam CreateDefaultParam() => new IntAmountParam { Amount = _defaultAmount };
		public bool Supports(IOperationParam param) => param is IntAmountParam;

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}