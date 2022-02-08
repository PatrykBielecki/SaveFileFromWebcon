using Newtonsoft.Json.Linq;
using RobotKan.DTO;

namespace RobotKan.Mapper
{
    public interface IOrganizationFromWebconMapper
    {
        WebconElement MapElementFromWebcon(JObject webconElement);
        Attachment MapAttachmentFromWebcon(JObject webconElement);
    }
}