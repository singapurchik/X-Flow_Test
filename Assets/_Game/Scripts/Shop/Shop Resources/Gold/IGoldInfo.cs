namespace Shop
{
	public interface IGoldInfo : IHasDisplayName
	{
		public int CurrentGold { get; }
	}
}