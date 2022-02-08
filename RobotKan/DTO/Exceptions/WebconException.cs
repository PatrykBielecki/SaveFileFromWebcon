using Newtonsoft.Json;
using RestSharp;

namespace RobotKan.DTO.Exceptions
{
    public class WebconException : AppException
    {
        public override string ErrorCode => "webcon_error";
        public WebconException(IRestResponse response)
            : base($"Błąd zapytania response={response.StatusCode} " +
                    $"body={response.Content} " +
                    $"success={response.IsSuccessful} " +
                    $"request={JsonConvert.SerializeObject(response.Request.Body)} " +
                    $"Error Message={response.ErrorMessage}")
        {

        }
    }
}
