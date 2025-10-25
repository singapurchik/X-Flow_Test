namespace Shop
{
	public interface ILocationInfo : IHasDisplayName
	{
		public string CurrentLocation { get; }
	}
}