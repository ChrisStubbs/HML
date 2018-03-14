namespace HML.RestClients
{
	public class RestRequest : RestSharp.RestRequest
	{
		public RestRequest()
		{
			JsonSerializer = new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer());
		}
	}


}