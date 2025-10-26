using UnityEngine;
using Core;

namespace Health
{
    [CreateAssetMenu(fileName = "Spend Health Percent of Max", menuName = "Health/Operations/Spend (Percent of Max)")]
    public sealed class SpendHealthPercentOfMax : ConsumeOperation, IOperationWithParameter
    {
        [SerializeField] private Health _health;
        [Range(1, 100)] [SerializeField] private int _defaultPercent = 10;

        public override PlayerDataValueInfo Info => _health.Info;

        public IOperationParameter CreateDefaultParam() => new PercentAmountParameter { Percent = _defaultPercent };

        public bool IsSupports(IOperationParameter parameter) => parameter is PercentAmountParameter;

        public override bool IsCanApply(IPlayerDataInfo data)
	        => IsCanApply(data, new PercentAmountParameter { Percent = _defaultPercent });

        public bool IsCanApply(IPlayerDataInfo data, IOperationParameter parameter)
        {
            var intParam = parameter as PercentAmountParameter;
            var percent = Mathf.Clamp(intParam?.Percent ?? _defaultPercent, 1, 100);

            var max  = Mathf.Max(1, _health.GetMaxHealth(data));
            var current  = Mathf.Max(0, _health.GetCurrentHealth(data));
            var need = Mathf.Max(1, Mathf.CeilToInt(max * (percent / 100f)));

            return current >= need;
        }

        public override void Apply(PlayerData data)
	        => Apply(data, new PercentAmountParameter { Percent = _defaultPercent });

        public void Apply(PlayerData data, IOperationParameter parameter)
        {
            var intParam = parameter as PercentAmountParameter;
            var percent = Mathf.Clamp(intParam?.Percent ?? _defaultPercent, 1, 100);

            var max   = Mathf.Max(1, _health.GetMaxHealth(data));
            var delta = Mathf.Max(1, Mathf.CeilToInt(max * (percent / 100f)));

            _health.Decrease(data, delta);
        }

#if UNITY_EDITOR
        private void OnValidate() => _defaultPercent = Mathf.Clamp(_defaultPercent, 1, 100);
#endif
    }
}
