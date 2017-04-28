using RealRestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace RealRestApi.Controllers
{
    [Route("/")]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(new RootModel());
        }
    }
}