// Health/AddHealth.cs
using UnityEngine;
using Core;

namespace Health
{
	[CreateAssetMenu(fileName = "Add Health", menuName = "Health/Operations/Add")]
	public sealed class AddHealth : ProvideOperation, IOperationWithParameter
	{
		[SerializeField] private PlayerDataKey _currentHealthKey;
		[Min(1)] [SerializeField] private int _defaultAmount = 1;

		public IOperationParameter CreateDefaultParam() => new IntAmountParameter { Amount = _defaultAmount };
		
		public bool IsSupports(IOperationParameter parameter) => parameter is IntAmountParameter;
		
		public bool IsCanApply(IPlayerDataInfo data, IOperationParameter parameter) => true;
		
		public override void Apply(PlayerData data)
			=> data.SetInt(_currentHealthKey, data.GetInt(_currentHealthKey) + _defaultAmount);

		public void Apply(PlayerData data, IOperationParameter parameter)
		{
			var intParam = parameter as IntAmountParameter;
			data.SetInt(_currentHealthKey,
				data.GetInt(_currentHealthKey) + Mathf.Max(1, intParam?.Amount ?? _defaultAmount));
		}

#if UNITY_EDITOR
		private void OnValidate() => _defaultAmount = Mathf.Max(1, _defaultAmount);
#endif
	}
}