using UnityEngine;
using Zenject;

namespace Shop
{
	public class ShopEntry : MonoBehaviour
	{
		[Inject] private IShopEntryPoint _shop;

		private void Start()
		{
			_shop.Initialize();
		}
	}
}