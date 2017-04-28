using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace RealRestApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase());

            // Add framework services.
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc(opt => opt.MapRoute("default", "{controller=root}/{id?}"));
        }

        private static void AddTestData(ApiContext context)
        {
            context.Users.Add(new Models.DbUser()
            {
                Id = 17,
                FirstName = "Luke",
                LastName = "Skywalker"
            });
            context.Users.Add(new Models.DbUser()
            {
                Id = 18,
                FirstName = "Han",
                LastName = "Solo"
            });

            context.SaveChanges();
        }
    }
}
