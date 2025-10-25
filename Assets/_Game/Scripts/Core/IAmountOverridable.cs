namespace Core
{
	public interface IAmountOverridable
	{
		bool CanApply(IPlayerDataInfo data, int amountOverride);
		void Apply(PlayerData data, int amountOverride);
	}
}