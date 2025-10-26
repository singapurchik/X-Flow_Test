using UnityEngine;
using System;
using Core;

namespace Shop
{
	[Serializable]
	public struct StatPlusBinding
	{
		public PlayerDataValueInfo Info;
		public ProvideOperation Operation;
		[SerializeReference] public IOperationParameter Param;
	}
}