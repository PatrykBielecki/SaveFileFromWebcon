using Newtonsoft.Json.Linq;

namespace RobotKan.Service
{
    public interface IWebconService
    {
        JObject GetElemenmt(int wfd_id);

        JObject GetAttachment(int wfd_id, string att_id);
    }
}