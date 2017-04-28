using System;
using System.Threading.Tasks;
using RealRestApi.DbModels;
using RealRestApi.Models;
using Mapster;

namespace RealRestApi.Queries
{
    public class CreateUserQuery
    {
        private readonly BeautifulContext _context;
        private readonly TypeAdapterConfig _typeAdapterConfig;

        public CreateUserQuery(BeautifulContext context, TypeAdapterConfig typeAdapterConfig)
        {
            _context = context;
            _typeAdapterConfig = typeAdapterConfig;
        }

        public async Task<Tuple<string, User>> Execute(UserCreateModel model)
        {
            var entry = _context.Users.Add(new DbUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate
            });

            await _context.SaveChangesAsync();

            return new Tuple<string, User>(
                entry.Entity.Id,
                entry.Entity.Adapt<User>(_typeAdapterConfig));
        }
    }
}
