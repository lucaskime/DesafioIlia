﻿using DesafioIlia.Domain.Interface;
using DesafioIlia.Infra.Data.Context;
using DesafioIlia.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DesafioIlia.Application.Interfaces;
using DesafioIlia.Application.Services;

namespace DesafioIlia.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IPontoRepository, PontoRepository>();
            services.AddScoped<IPontoService, PontoService>();

            return services;
        }
    }
}
