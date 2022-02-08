using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RobotKan.Core.Configuration;
using RobotKan.DTO;
using RobotKan.Mapper;
using RobotKan.Service;
using System;

namespace RobotKan.Controllers
{
    [ApiController]
    public class FileDownloadController : ControllerBase
    {
        private readonly WebconConfig _webconConfig;
        private readonly IWebconService _webconService;
        private readonly IOrganizationFromWebconMapper _organizationFromWebconMapper;

        public FileDownloadController(IWebconService webconService, IOrganizationFromWebconMapper organizationFromWebconMapper, IOptions<WebconConfig> webconConfigOptions)
        {
            _webconConfig = webconConfigOptions.Value;
            _webconService = webconService;
            _organizationFromWebconMapper = organizationFromWebconMapper;
        }

        [HttpGet("OK")]
        public IActionResult OK()
        {
            return Ok();
        }

        [HttpPost("downloadFile/{wfdID}")]
        public IActionResult DownloadFile(int wfdID)
        {
            var webconElement = _webconService.GetElemenmt(wfdID);
            WebconElement element = _organizationFromWebconMapper.MapElementFromWebcon(webconElement);

            var webconAttachment = _webconService.GetAttachment(wfdID, element.Attachments[0].Id);
            var attachment = _organizationFromWebconMapper.MapAttachmentFromWebcon(webconAttachment);

            System.IO.File.WriteAllBytes(_webconConfig.Path + attachment.Name, Convert.FromBase64String(attachment.Content));

            return Ok();
        }
    }
}
