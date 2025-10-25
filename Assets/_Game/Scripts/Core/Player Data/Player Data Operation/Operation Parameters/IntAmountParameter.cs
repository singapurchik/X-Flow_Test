using System;

namespace Core
{
	[Serializable]
	public class IntAmountParameter : IOperationParameter
	{
		public int Amount = 1;
	}
}