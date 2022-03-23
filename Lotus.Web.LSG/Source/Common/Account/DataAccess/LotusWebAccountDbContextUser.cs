//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//=====================================================================================================================
namespace Lotus.Web
{
	namespace Account
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Контекст базы данных представляющий собой всех пользователей
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUserDbContext : IdentityDbContext<CUser>
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация данных
			/// </summary>
			/// <param name="user_manager">Менеджер пользователей</param>
			/// <param name="role_manager">Менеджер ролей</param>
			/// <returns>Задача</returns>
			//---------------------------------------------------------------------------------------------------------
			public static async Task InitializeAsync(UserManager<CUser> user_manager, RoleManager<IdentityRole> role_manager)
			{
				// Создаем базовые роли
				if (await role_manager.FindByNameAsync("Аминистратор") == null)
				{
					await role_manager.CreateAsync(new IdentityRole("Аминистратор"));
				}
				if (await role_manager.FindByNameAsync("Редактор") == null)
				{
					await role_manager.CreateAsync(new IdentityRole("Редактор"));
				}
				if (await role_manager.FindByNameAsync("Пользователь") == null)
				{
					await role_manager.CreateAsync(new IdentityRole("Пользователь"));
				}

				// Создаем администратора системы
				String admin_name = "DanielDem";
				String admin_email = "dementevds@gmail.com";
				String admin_password = "198418dsf";

				if (await user_manager.FindByNameAsync(admin_email) == null)
				{
					CUser admin = new CUser 
					{ 
						Email = admin_email, 
						UserName = admin_name,
						Name = "Даниил",
						Surname = "Дементьев",
						Patronymic = "Сергеевич",
					};
					IdentityResult result = await user_manager.CreateAsync(admin, admin_password);
					
					if (result.Succeeded)
					{
						await user_manager.AddToRoleAsync(admin, "Аминистратор");
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Список должностей
			/// </summary>
			[Browsable(false)]
			public DbSet<CPost> Posts
			{
				get; set;
			}

			/// <summary>
			/// Список сфер деятельности
			/// </summary>
			[Browsable(false)]
			public DbSet<CFieldActivity> FieldActivities
			{
				get; set;
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="options">Параметры конфигурации</param>
			//---------------------------------------------------------------------------------------------------------
			public CUserDbContext(DbContextOptions<CUserDbContext> options)
				: base(options)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование моделей
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			protected override void OnModelCreating(ModelBuilder model_builder)
			{
				CUser.ModelCreating(model_builder);
				CPost.ModelCreating(model_builder);
				CFieldActivity.ModelCreating(model_builder);

				base.OnModelCreating(model_builder);

			}
			#endregion
		}
	}
}
//=====================================================================================================================