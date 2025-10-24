using Core;

namespace Shop
{
	public class StatsViewsPool : ObjectPool<StatsView>
	{
		protected override void InitializeObject(StatsView view)
		{
			view.OnHide += ReturnToPool;
		}

		protected override void CleanupObject(StatsView view)
		{
			view.OnHide -= ReturnToPool;
		}
	}
}