using UnityEngine;

namespace Core
{
	[CreateAssetMenu(menuName = "Core/Player Data Key", fileName = "Player Data Key")]
	public sealed class PlayerDataKey : ScriptableObject
	{
		[HideInInspector] [SerializeField] private string _id;
		
		public string Id => _id;

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (string.IsNullOrWhiteSpace(_id))
				_id = System.Guid.NewGuid().ToString("N");
		}
#endif
	}
}