using RealRestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace RealRestApi.Controllers
{
    [Route("/")]
    public class RootController : Controller
    {
        public IActionResult Get()
        {
            var response = new
            {
                meta = new IonLink { Href = Url.Link("default", null) },
                users = new IonLink()
                {
                    Href = Url.Link("default", new { controller = "users" }),
                    Relations = new[] { "collection" }
                }
            };

            return Ok(response);
        }
    }
}