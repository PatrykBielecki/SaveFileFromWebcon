using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RobotKan.Core.Configuration;
using RobotKan.DTO.Exceptions;
using RestSharp;

namespace RobotKan.Service
{
    public class WebconService : IWebconService
    {
        private readonly WebconConfig _webconConfig;
        private readonly RestClient _restClient;
        public WebconService(IOptions<WebconConfig> webconConfigOptions)
        {
            _webconConfig = webconConfigOptions.Value;
            _restClient = new RestClient(_webconConfig.PortalURL);
        }

        private string GetAuthorizationToken()
        {
            var accessTokenRequestBody = string.IsNullOrEmpty(_webconConfig.ImpersoniteLogin) ? $@"{{""clientId"": ""{_webconConfig.WebconClientId}"",""clientSecret"": ""{_webconConfig.WebconSecret}""}}" :
                                         $@"{{""clientId"": ""{_webconConfig.WebconClientId}"",""clientSecret"": ""{_webconConfig.WebconSecret}"",""impersonation"":{{""login"":""{_webconConfig.ImpersoniteLogin}""}}}}";

            var requestToken = new RestRequest("api/login", Method.POST);
            requestToken.AddHeader("Accept", "application/json");
            requestToken.AddHeader("Content-type", "application/json");
            requestToken.AddParameter("application/json", accessTokenRequestBody, ParameterType.RequestBody);
            var response = _restClient.Execute(requestToken);
            var responseObj = JObject.Parse(response.Content);
            return (string)responseObj["token"];
        }

        private IRestResponse ExecuteRequest(RestRequest request)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {GetAuthorizationToken()}");

            return _restClient.Execute(request);
        }

        public JObject GetElemenmt(int wfd_id)
        {
            var request = new RestRequest($"api/data/v1.0/db/1/elements/{wfd_id}", Method.GET);
            var response = ExecuteRequest(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new WebconException(response);
            }

            var responseObj = JObject.Parse(response.Content);
            return responseObj;
        }
        public JObject GetAttachment(int wfd_id, string att_id)
        {
            var request = new RestRequest($"api/data/v3.0/db/1/elements/{wfd_id}/attachments/{att_id}", Method.GET);
            var response = ExecuteRequest(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new WebconException(response);
            }

            var responseObj = JObject.Parse(response.Content);
            return responseObj;
        }

    }
}
