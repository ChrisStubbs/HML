using System.Web;
using HML.Employee.Common.Interfaces;

namespace HML.Employee.WebAPI.Infrastructure
{
	public class WebUsernameProvider : IUsernameProvider
	{
		public WebUsernameProvider()
		{
			SetUserName();
		}

		private void SetUserName()
		{
			Username = !string.IsNullOrEmpty(HttpContext.Current?.Request.Headers["Username"])
				   ? HttpContext.Current.Request.Headers["Username"]
				   : "Anonymous";
		}

		public string Username { get; set; }
	}
}