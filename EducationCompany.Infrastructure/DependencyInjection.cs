using EducationCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;   
using Microsoft.Extensions.DependencyInjection;

namespace EducationCompany.Infrastructure;
public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
         var cs = configuration.GetConnectionString("DefaultConnection");

         if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("Connection string 'DefaultConnection' saknas.");

         services.AddDbContext<AppDbContext>(options => options.UseSqlite(cs));

         return services;
    }
}
