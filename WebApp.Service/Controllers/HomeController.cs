using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Service.Controllers
{
    [ApiController]
    [Route("HomeController")]
    public class HomeController : ControllerBase
    {
        
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new[]
            {
                1, 2, 3, 4, 5, 6
            };
        }
    }
}
