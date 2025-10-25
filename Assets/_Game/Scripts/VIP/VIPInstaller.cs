using UnityEngine;
using Zenject;
using System;
using Core;

namespace VIP
{
	public class VIPInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataKey _vipRemainingTime;
		[Min(0)] [SerializeField] private int _startDurationSeconds = 30;
		
		public override void InstallBindings()
		{
			Container.Bind<VIP>()
				.AsSingle()
				.WithArguments(_vipRemainingTime, TimeSpan.FromSeconds(_startDurationSeconds))
				.NonLazy();
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_startDurationSeconds = Mathf.Max(0, _startDurationSeconds);
		}
#endif
	}
}