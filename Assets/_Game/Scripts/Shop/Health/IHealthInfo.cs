namespace Shop
{
	public interface IHealthInfo
	{
		public int CurrentHealth { get; }
		public int MaxHealth { get; }
		
		public string Name { get; }
	}
}