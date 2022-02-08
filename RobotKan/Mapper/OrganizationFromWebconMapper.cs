using Newtonsoft.Json.Linq;
using RobotKan.DTO;

namespace RobotKan.Mapper
{
    public class OrganizationFromWebconMapper : IOrganizationFromWebconMapper
    {
        public Attachment MapAttachmentFromWebcon(JObject webconElement)
        {
            Attachment attachment = webconElement.ToObject<Attachment>();

            return attachment;
        }
        public WebconElement MapElementFromWebcon(JObject webconElement)
        {
            WebconElement element = webconElement.ToObject<WebconElement>();
            return element;
        }
    }
}
