using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace HML.RestClients
{
	public abstract class BaseHttpClient
	{
		public IConfig Config { get; set; }

		protected abstract string BaseUrl { get; }
		public IRestResponse Response { get; set; }

		protected BaseHttpClient(IConfig config)
		{
			Config = config;
		}

		public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
		{
			var response = await ExecuteAsyncWithResponse<T>(request);
			return response.Data;
		}

		public async Task<IRestResponse<T>> ExecuteAsyncWithResponse<T>(RestRequest request)
		{
			var client = new RestClient { BaseUrl = new System.Uri(BaseUrl) };

			request.AddParameter("Username", Config.Username, ParameterType.HttpHeader); // used on every request
			var response = await client.ExecuteTaskAsync<T>(request).ConfigureAwait(false);

			if (response.ErrorException != null || response.StatusCode == HttpStatusCode.InternalServerError)
			{
				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error retrieving response.  Check inner details for more info.", response.ErrorException);
				}
				throw new ApplicationException($"{response.StatusCode} from api {BaseUrl} ");
			}


			return response;
		}
	}
}