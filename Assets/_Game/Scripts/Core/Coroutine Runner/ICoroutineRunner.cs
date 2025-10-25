using System.Collections;
using UnityEngine;

namespace Core
{
	public interface ICoroutineRunner
	{
		public Coroutine Run(IEnumerator routine);
	}
}