namespace Shop
{
	public interface IHealthInfo : IHasDisplayName
	{
		public int CurrentHealth { get; }
		public int MaxHealth { get; }
	}
}