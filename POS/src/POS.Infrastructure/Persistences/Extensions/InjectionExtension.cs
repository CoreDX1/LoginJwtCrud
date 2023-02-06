using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.Persistences.Context;
using POS.Infrastructure.Persistences.Interface;
using POS.Infrastructure.Persistences.Repository;

namespace POS.Infrastructure.Persistences.Extensions;

public static class InjectionExtension
{
    public static IServiceCollection addInjectionInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var assemble = typeof(FormContext).Assembly.FullName;
        services.AddDbContext<FormContext>(
            options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("POSConnectionString"),
                    b => b.MigrationsAssembly(assemble)
                );
            },
            ServiceLifetime.Transient
        );
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
