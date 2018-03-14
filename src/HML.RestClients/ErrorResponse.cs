using System.Collections.Generic;
using System.Linq;

namespace HML.RestClients
{
	public class ErrorResponse
	{
		public string Message { get; set; }
		public Dictionary<string, IList<string>> ModelState { get; set; }
		public List<string> Errors => ModelState.SelectMany(x => x.Value).ToList();
	}


}