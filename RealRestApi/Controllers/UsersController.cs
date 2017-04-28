using RealRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealRestApi.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var users = (await _context.Users.ToArrayAsync(ct))
                .Select(dbu => new User()
                {
                    Meta = new IonLink { Href = Url.Link("default", new { id = dbu.Id }) },
                    FirstName = dbu.FirstName,
                    LastName = dbu.LastName
                }).ToArray();

            var response = new IonCollection<User>()
            {
                Meta = new IonLink()
                {
                    Href = Url.Link("default", null),
                    Relations = new[] { "collection" }
                },
                Items = users
            };

            return Ok(response);
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken ct)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id, ct);

            if (user == null)
            {
                return NotFound();
            }

            var response = new User()
            {
                Meta = new IonLink { Href = Url.Link("default", new { id = user.Id }) },
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return Ok(response);
        }
    }
}
