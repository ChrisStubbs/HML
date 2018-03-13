using System.Collections.Generic;

namespace HML.Immunisation.Common
{
	public static class Extensions
	{
		public const char SplitChar = '-';
		public static string DisplayValue<T, T2>(this KeyValuePair<T, T2> keyValue)
		{
			return (keyValue.Key != null && !EqualityComparer<T>.Default.Equals(keyValue.Key, default(T)))
				? $"{keyValue.Key} {SplitChar} {keyValue.Value}"
				: string.Empty;
		}
	}
}