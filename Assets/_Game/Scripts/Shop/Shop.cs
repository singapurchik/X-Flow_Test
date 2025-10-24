namespace Shop
{
	public sealed class Shop
	{
		private readonly IHealthInfo _healthInfo;

		public Shop(IHealthInfo healthInfo)
		{
			_healthInfo = healthInfo;
		}
	}
}