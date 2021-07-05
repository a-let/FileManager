using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Reflection;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Default")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get()
        {
            var message = $"File Manager - {Environment.MachineName} - {Assembly.GetExecutingAssembly().GetName().Version}";
            return Ok(message);
        }
    }
}