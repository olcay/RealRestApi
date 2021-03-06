﻿using System.Linq;
using System.Threading.Tasks;
using RealRestApi.Models;
using RealRestApi.Queries;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RealRestApi.Controllers
{
    [Route(Endpoint)]
    public class UsersController : Controller
    {
        public const string Endpoint = "users";

        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingOptions;
        private readonly TypeAdapterConfig _typeAdapterConfig;

        public UsersController(
            BeautifulContext context,
            IOptions<PagedCollectionParameters> defaultPagingOptions,
            TypeAdapterConfig typeAdapterConfig)
        {
            _context = context;
            _defaultPagingOptions = defaultPagingOptions.Value;
            _typeAdapterConfig = typeAdapterConfig;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        {
            var getAllQuery = new GetAllUsersQuery(_context, _defaultPagingOptions, _typeAdapterConfig, Endpoint);
            var results = await getAllQuery.Execute(parameters);

            // Attach form definitions for discoverability
            results.Forms = new[] { Form.FromModel<UserCreateModel>(Endpoint, "POST", "create-form") };

            return new ObjectResult(results);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var getQuery = new GetUserQuery(_context, _typeAdapterConfig);
            var user = await getQuery.Execute(id);

            return user == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(user);
        }

        [HttpGet]
        [Route("{id}/posts")]
        public async Task<IActionResult> GetPosts(string id, PagedCollectionParameters parameters)
        {
            var query = new GetPostsByUserQuery(_context, _defaultPagingOptions, _typeAdapterConfig, Endpoint);
            var posts = await query.Execute(id, parameters);

            return posts == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ModelState.Values.First().Errors.First().ErrorMessage
                });
            }

            var createQuery = new CreateUserQuery(_context, _typeAdapterConfig);
            var user = await createQuery.Execute(model);

            return new CreatedAtRouteResult("default", new { controller = Endpoint, id = user.Item1 }, user.Item2);
        }
    }
}
