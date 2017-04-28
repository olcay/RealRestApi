using RealRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace RealRestApi
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<DbUser> Users { get; set; }
    }
}
