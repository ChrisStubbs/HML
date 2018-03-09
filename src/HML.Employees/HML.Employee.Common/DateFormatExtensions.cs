using System;

namespace HML.Employee.Common
{
	public static class DateFormatExtensions
	{
		public const string ShortDateFormat = "{0:dd MMM yyyy}";

		public static string ToShortDateFormat(this DateTime? dateTime)
		{
			return string.Format(ShortDateFormat, dateTime);
		}

		public static string ToShortDateFormat(this DateTime dateTime)
		{
			return ToShortDateFormat((DateTime?)dateTime);
		}
	}
}