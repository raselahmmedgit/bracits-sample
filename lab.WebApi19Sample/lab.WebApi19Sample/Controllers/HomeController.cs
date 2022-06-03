using lab.WebApi19Sample.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lab.WebApi19Sample.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : BaseApiController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Index()
        {
            //https://codingsonata.com/apply-jwt-access-tokens-and-refresh-tokens-in-asp-net-core-web-api-6/
            //https://jasonwatmore.com/post/2020/05/25/aspnet-core-3-api-jwt-authentication-with-refresh-tokens
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
