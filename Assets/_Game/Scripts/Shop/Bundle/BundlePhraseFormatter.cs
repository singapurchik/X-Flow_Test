using System.Collections.Generic;
using System.Text;
using Zenject;

namespace Shop
{
	public sealed class BundlePhraseFormatter
	{
		[Inject] private readonly ILocationInfo _location;
		[Inject] private readonly IHealthInfo _health;
		[Inject] private readonly IGoldInfo _gold;
		[Inject] private readonly IVIPInfo _vip;

		private const string FOR_SEPARATOR = " for ";
		private const string COMMA_SEPARATOR = ", ";
		private const string AND_WORD = " and ";

		public string Build(IReadOnlyList<BundleCurrencyType> costs, IReadOnlyList<BundleCurrencyType> rewards)
		{
			var sb = new StringBuilder(64);

			var costsCount = CountValid(costs);
			var rewardsCount = CountValid(rewards);

			if (costsCount > 0)
				AppendJoinedWithAnd(sb, costs, costsCount);

			if (rewardsCount > 0 && costsCount > 0)
				sb.Append(FOR_SEPARATOR);

			if (rewardsCount > 0)
				AppendJoinedWithAnd(sb, rewards, rewardsCount);

			if (sb.Length > 0)
				sb[0] = char.ToUpperInvariant(sb[0]);

			return sb.ToString();
		}

		private string NameFor(BundleCurrencyType currency)
		{
			return currency switch
			{
				BundleCurrencyType.Gold => _gold.DisplayName,
				BundleCurrencyType.Health => _health.DisplayName,
				BundleCurrencyType.Location => _location.DisplayName,
				BundleCurrencyType.Vip => _vip.DisplayName,
				_ => null
			};
		}

		private int CountValid(IReadOnlyList<BundleCurrencyType> list)
		{
			if (list != null)
			{
				int count = 0;
				
				for (var i = 0; i < list.Count; i++)
				{
					var name = NameFor(list[i]);
					if (!string.IsNullOrEmpty(name))
						count++;
				}
				return count;
			}
			return 0;
		}

		private void AppendJoinedWithAnd(StringBuilder sb, IReadOnlyList<BundleCurrencyType> list, int totalValid)
		{
			int appended = 0;
			for (var i = 0; i < list.Count; i++)
			{
				var name = NameFor(list[i]);
				
				if (!string.IsNullOrEmpty(name))
				{
					if (appended > 0)
					{
						if (appended == totalValid - 1)
							sb.Append(AND_WORD);
						else
							sb.Append(COMMA_SEPARATOR);
					}

					sb.Append(name);
					appended++;

					if (appended == totalValid)
						break;	
				}
			}
		}
	}
}