using System.Threading.Tasks;
using RealRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace RealRestApi.Queries
{
    public class GetUserQuery
    {
        private readonly BeautifulContext _context;
        private readonly TypeAdapterConfig _typeAdapterConfig;

        public GetUserQuery(BeautifulContext context, TypeAdapterConfig typeAdapterConfig)
        {
            _context = context;
            _typeAdapterConfig = typeAdapterConfig;
        }

        public async Task<User> Execute(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            return user?.Adapt<User>(_typeAdapterConfig);
        }
    }
}
