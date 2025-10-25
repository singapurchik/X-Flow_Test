using System;

namespace Core
{
	[Serializable]
	public class IntAmountParam : IOperationParam
	{
		public int Amount = 1;
	}
}