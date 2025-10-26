using UnityEngine;
using Core;

namespace Health
{
	[CreateAssetMenu(fileName = "Health", menuName = "Health/Health")]
	public sealed class Health : PlayerDataValue
	{
		[SerializeField] private HealthInfo _info;
		[SerializeField] private int _startHealth = 10;
		
		public override PlayerDataValueInfo Info => _info;

		public override void Initialize(PlayerData data) => data.SetInt(_info.CurrentHealthKey, _startHealth);
		
		public void Increase(PlayerData data, int amount)
			=> data.SetInt(_info.CurrentHealthKey, _info.GetCurrentHealth(data) + amount);
		
		public void Decrease(PlayerData data, int amount)
			=> data.SetInt(_info.CurrentHealthKey, _info.GetCurrentHealth(data) - amount);
		
		public int GetCurrentHealth(IPlayerDataInfo data) =>_info.GetCurrentHealth(data);
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			_startHealth = Mathf.Max(0, _startHealth);
		}
#endif
	}
}