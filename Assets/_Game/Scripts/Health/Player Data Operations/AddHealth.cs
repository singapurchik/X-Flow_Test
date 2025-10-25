using UnityEngine;
using Core;

namespace Health
{
	[CreateAssetMenu(fileName = "Add Health", menuName = "Health/Operations/Add Health")]
	public sealed class AddHealth : ProvideOperation
	{
		[SerializeField] private PlayerDataKey _currentHealthKey;
		[SerializeField] private PlayerDataKey _maxHealthKey;
		[Min(1)][SerializeField] private int _amount = 1;

		public override bool CanApply(IPlayerDataInfo data) => true;
		
		public override void Apply(PlayerData data)
		{
			var currentHealth = data.GetInt(_currentHealthKey);
			var maxHealth = data.GetInt(_maxHealthKey);

			if (currentHealth < maxHealth)
			{
				var targetHealth = Mathf.Min(data.GetInt(_maxHealthKey), data.GetInt(_currentHealthKey) + _amount);
				data.SetInt(_currentHealthKey, targetHealth);	
			}
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_amount = Mathf.Max(1, _amount);
		}
#endif
	}
}