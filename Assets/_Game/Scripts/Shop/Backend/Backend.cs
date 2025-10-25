using System.Collections;
using UnityEngine;
using System;

namespace Shop
{
	public sealed class Backend : IBackend
	{
		private const float DEFAULT_DELAY_SECONDS = 3f;

		public bool IsBusy { get; private set; }

		public event Action OnFinished;
		public event Action OnStarted;

		public IEnumerator SendRequestRoutine()
		{
			if (IsBusy) yield break;

			IsBusy = true;
			OnStarted?.Invoke();

			yield return new WaitForSecondsRealtime(DEFAULT_DELAY_SECONDS);

			OnFinished?.Invoke();
			IsBusy = false;
		}
	}
}