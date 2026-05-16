using CommerceHub.Persistence.Context;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Extentsions
{
	public static  class PersistenceServiceExtensions
	{

		public static IServiceCollection AddPersistenceService(this IServiceCollection services,IConfiguration configuration)
		{

			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;

		}
		
	}
}
