using UnityEngine;

namespace Shop
{
	public class ShopView : MonoBehaviour
	{
		[SerializeField] private RectTransform _bundlesContainer;
		
		public RectTransform BundlesContainer => _bundlesContainer;
	}
}