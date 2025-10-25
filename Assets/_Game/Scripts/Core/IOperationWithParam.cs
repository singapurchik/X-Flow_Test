namespace Core
{
	public interface IOperationWithParam
	{
		public bool CanApply(IPlayerDataInfo data, IOperationParam param);
		public void Apply(PlayerData data, IOperationParam param);
		public IOperationParam CreateDefaultParam();
		public bool Supports(IOperationParam param); 
	}
}