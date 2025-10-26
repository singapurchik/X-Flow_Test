using UnityEngine;
using Core;

namespace Health
{
    [CreateAssetMenu(fileName = "Health", menuName = "Health/Health")]
    public sealed class Health : PlayerDataValue
    {
        [SerializeField] private HealthInfo _info;
        [SerializeField] private int _startHealth = 10;
        [SerializeField] private int _maxHealth   = 100;

        public override PlayerDataValueInfo Info => _info;

        public override void Initialize(PlayerData data)
        {
            var max = Mathf.Max(1, _maxHealth);
            var cur = Mathf.Clamp(_startHealth, 0, max);

            data.SetInt(_info.MaxHealthKey, max);
            data.SetInt(_info.CurrentHealthKey, cur);
        }

        public int GetCurrentHealth(IPlayerDataInfo data) => _info.GetCurrentHealth(data);
        public int GetMaxHealth    (IPlayerDataInfo data) => _info.GetMaxHealth(data);

        public void Increase(PlayerData data, int amount)
        {
            var max  = GetMaxHealth(data);
            var cur  = GetCurrentHealth(data);
            var next = Mathf.Clamp(cur + Mathf.Max(0, amount), 0, max);
            data.SetInt(_info.CurrentHealthKey, next);
        }

        public void Decrease(PlayerData data, int amount)
        {
            var max  = GetMaxHealth(data);
            var cur  = GetCurrentHealth(data);
            var next = Mathf.Clamp(cur - Mathf.Max(0, amount), 0, max);
            data.SetInt(_info.CurrentHealthKey, next);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            _maxHealth   = Mathf.Max(1, _maxHealth);
            _startHealth = Mathf.Clamp(_startHealth, 0, _maxHealth);
        }
#endif
    }
}
