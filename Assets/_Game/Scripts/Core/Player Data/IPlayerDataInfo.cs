namespace Core
{
	public interface IPlayerDataInfo
	{
		public string GetString(PlayerDataKey key, string def = "");
		public bool GetBool(PlayerDataKey key, bool def = false);
		public float GetFloat(PlayerDataKey key, float def = 0f);
		public int GetInt(PlayerDataKey key, int def = 0);
	}
}