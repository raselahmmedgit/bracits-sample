using lab.WebApi19Sample.Data;
using lab.WebApi19Sample.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace lab.WebApi19Sample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : BaseApiController
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Index()
        {
            try
            {
                _resultApi = ResultApi.Ok();
                return Ok(_resultApi);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex.Message, "Error"));
                return Error(ex);
            }
        }
    }
}
