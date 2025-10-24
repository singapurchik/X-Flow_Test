using UnityEngine;
using System;

namespace Shop
{
	public class Bundle : MonoBehaviour
	{
		public event Action<Bundle> OnBundleOutOfStock;
	}
}