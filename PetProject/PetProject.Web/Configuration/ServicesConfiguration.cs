﻿using PetProject.Orchestrators.Interfaces;
using PetProject.Orchestrators.Implementations;
using PetProject.DataContext.MsSql;
using Microsoft.EntityFrameworkCore;
using PetProject.DataContext.Interfaces;
using PetProject.DataAccess.Repositories.Interfaces;
using PetProject.DataAccess.Repositories.Implementations;

namespace PetProject.Web.Configuration
{
    /// <summary>
    /// Класс расширений для регистрации сервисов в приложении
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Регистрация сервисов Context
        /// </summary>
        public static void RegistrationUserMsSqlServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MSSqlContext>(options => options.UseSqlServer(configuration.GetSection("Data:MSSQL:ConnectionString").Value), ServiceLifetime.Transient);
            services.AddDbContext<IUserContext, MSSqlContext>();
        }

        /// <summary>
        /// Регистрация сервисов предоставления данных
        /// </summary>
        public static void AddDataLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,,>), typeof(BaseRepository<,,>));
        }

        /// <summary>
        /// Регистрация оркестраторов
        /// </summary>
        public static void AddOrchestrators(this IServiceCollection services, IConfiguration configuration)
        {
            // Scoped - в рамках одного запроса внедряется один и тот же экземпляр          
            services.AddScoped<IUserOrchestrator, UserOrchestrator>();
        }

        /// <summary>
        /// Регистрация сервисов кэширования
        /// </summary>
        public static void AddCacheService(this IServiceCollection services)
        {
            /*services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<INSDataMapService, NSDataMapService>();
            services.AddScoped<ApiNSHelper>();*/
        }
    }
}
