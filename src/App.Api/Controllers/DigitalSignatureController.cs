using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DigitalSignatureController: ControllerBase
    {
        [HttpPost]
        [MiddlewareFilter(typeof(DigitalSignatureHandlerMiddlewarePipeline))]
        public async Task<IActionResult> ProxyMiddleware()
        {
            return await Task.Run(() => NotFound());
        }
    }
}
