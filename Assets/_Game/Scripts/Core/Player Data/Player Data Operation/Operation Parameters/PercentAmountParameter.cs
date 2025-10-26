using UnityEngine;
using System;

namespace Core
{
	[Serializable]
	public class PercentAmountParameter : IOperationParameter, ISerializationCallbackReceiver
	{
		[Range(1, 100)] public int Percent = 10;
		
		public void OnBeforeSerialize()  { Percent = Mathf.Clamp(Percent, 1, 100); }
		public void OnAfterDeserialize() { Percent = Mathf.Clamp(Percent, 1, 100); }
	}
}