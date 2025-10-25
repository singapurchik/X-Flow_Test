using UnityEngine;

namespace Core
{
	[CreateAssetMenu(fileName = "Resource Descriptor", menuName = "Core/Resource Descriptor")]
	public sealed class ResourceDescriptor : ScriptableObject
	{
		[SerializeField] private string _displayName = "Resource";
		
		public string DisplayName => _displayName;
	}
}