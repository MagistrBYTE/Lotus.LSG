//=====================================================================================================================
// Проект: Lotus.Web
// Раздел: Общий модуль
// Подраздел: Подсистема аккаунта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusWebAccountModuleInitializer.cs
*		Инициализация модуля аккаунта.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
//---------------------------------------------------------------------------------------------------------------------
using Pomelo.EntityFrameworkCore.MySql;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus.Web
{
    namespace Account
    {
        //-------------------------------------------------------------------------------------------------------------
        //! \addtogroup WebCommonAccount
        /*@{*/
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Инициализация модуля аккаунта
        /// </summary>
        //-------------------------------------------------------------------------------------------------------------
        public static class XModuleInitializer
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="app_builder"></param>
            /// <param name="connect_string"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void ConfigureAccount(this WebApplicationBuilder app_builder, String connect_string)
            {
                if (app_builder == null)
                {
                    throw new ArgumentNullException(nameof(app_builder));
                }

                  // Контекст базы данных для пользователей
                app_builder.Services.AddDbContext<CUserDbContext>(options =>
                {
                    //options.UseNpgsql(app_builder.Configuration.GetConnectionString(connect_string));

                    //
                    // MySql
                    //
                    String connect_str = app_builder.Configuration.GetConnectionString(connect_string);
                    MySqlServerVersion server_version = new MySqlServerVersion(new Version(5, 7, 0));
                    options.UseMySql(connect_str, server_version, (options_builder_mysql) =>
                    {
                    });
                },
                ServiceLifetime.Transient);

                // Добавление сервисов Idenity
                app_builder.Services.AddIdentity<CUser, IdentityRole>((IdentityOptions options) =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                    .AddEntityFrameworkStores<CUserDbContext>();

                // Добавление сервиса для информирования о состоянии валидации пользователя для компонентов Razor
                app_builder.Services.AddScoped<ILotusAuthorizeApi, CAuthorizeApi>();
                app_builder.Services.AddScoped<CIdentityAuthenticationStateProvider>();
                app_builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CIdentityAuthenticationStateProvider>());

                // Конфигурирование опций идентификации
                app_builder.Services.Configure<IdentityOptions>(options =>
                {
                    // Параметры пароля
                    options.Password.RequireDigit = false;              // если равно true, то пароль должен будет иметь как минимум одну цифру
                    options.Password.RequiredLength = 6;                // минимальная длина пароля
                    options.Password.RequireNonAlphanumeric = false;    // требуются ли не алфавитно-цифровые символы
                    options.Password.RequireUppercase = false;          // если равно true, то пароль должен будет иметь как минимум один символ в нижнем регистре
                    options.Password.RequireLowercase = false;          // если равно true, то пароль должен будет иметь как минимум один символ в верхнем регистр
                    options.Password.RequiredUniqueChars = 6;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = false;
                });


                app_builder.Services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = false;
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="application"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void UseAccount(this IApplicationBuilder application)
            {
                if (application == null)
                {
                    throw new ArgumentNullException(nameof(application));
                }

                if (application is not null)
                {
                    using var service_scope = application.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
                    using var context = service_scope.ServiceProvider.GetRequiredService<CUserDbContext>();

                    //try
                    //{
                    //    var userManager = service_scope.ServiceProvider.GetRequiredService<UserManager<CUser>>();
                    //    var rolesManager = service_scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    //    CUserDbContext.InitializeAsync(userManager, rolesManager);
                    //}
                    //catch (Exception exс)
                    //{

                    //}

                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception exс)
                    {

                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        /*@}*/
        //-------------------------------------------------------------------------------------------------------------
    }
}
