using System;

namespace HML.Employee.Common
{
	public static class DateFormatExtensions
	{
		public const string ShortDateFormat = "{0:dd MMM yyyy}";

		public const string DateTimeFormat = "{0:dd MMM yyyy HH:mm:ss}";

		public static string ToShortDateFormat(this DateTime? dateTime)
		{
			return string.Format(ShortDateFormat, dateTime);
		}

		public static string ToShortDateFormat(this DateTime dateTime)
		{
			return ToShortDateFormat((DateTime?)dateTime);
		}

		public static string ToDateTimeFormat(this DateTime? dateTime)
		{
			return string.Format(DateTimeFormat, dateTime);
		}

		public static string ToDateTimeFormat(this DateTime dateTime)
		{
			return ToDateTimeFormat((DateTime?)dateTime);
		}
	}
}