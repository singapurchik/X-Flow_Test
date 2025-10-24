using UnityEngine;
using Zenject;

namespace Shop
{
	public class ShopEntry : MonoBehaviour
	{
		[SerializeField] private int _startBundlesCount = 3;

		[Inject] private IShopEntryPoint _shop;

		private void Start()
		{
			_shop.Initialize(_startBundlesCount);
		}
	}
}