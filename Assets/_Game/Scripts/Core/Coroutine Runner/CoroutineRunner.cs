using System.Collections;
using UnityEngine;

namespace Core
{
	public sealed class CoroutineRunner : MonoBehaviour, ICoroutineRunner
	{
		public Coroutine Run(IEnumerator routine) => StartCoroutine(routine);
	}
}