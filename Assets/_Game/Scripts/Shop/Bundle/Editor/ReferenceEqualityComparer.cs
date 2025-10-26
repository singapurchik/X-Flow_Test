#if UNITY_EDITOR
using System.Collections.Generic;

namespace Shop.EditorTools
{
	public sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		public static readonly ReferenceEqualityComparer<T> Instance = new();
		
		public bool Equals(T x, T y) => ReferenceEquals(x, y);
		
		public int GetHashCode(T obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
	}
}
#endif