//=====================================================================================================================
// Проект: Lotus.Web
// Раздел: Общий модуль
// Подраздел: Подсистема аккаунта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusWebAccountUser.cs
*		Класс для определения пользователя.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus.Web
{
	namespace Account
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup WebCommonAccount Подсистема аккаунта
		//! \ingroup WebCommon
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения пользователя
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUser : IdentityUser
		{
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CUser"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				model_builder.Entity<CUser>()
					.HasMany(user => user.FieldActivities)
					.WithMany(field => field.Users)
					.UsingEntity(j => j.ToTable("AspUserFieldActivities"));
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal List<String> mRoleNames;
			protected internal List<String> mFieldActivityNames;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя пользователя
			/// </summary>
			[DataType(DataType.Text)]
			[MaxLength(30)]
			public String Name { get; set; }

			/// <summary>
			/// Фамилия пользователя
			/// </summary>
			[DataType(DataType.Text)]
			[MaxLength(30)]
			public String Surname { get; set; }

			/// <summary>
			/// Отчество пользователя
			/// </summary>
			public String? Patronymic { get; set; }

			/// <summary>
			/// Полное имя (ФИО)
			/// </summary>
			[NotMapped]
			public String FullName 
			{
				get { return ($"{Surname} {Name} {Patronymic}"); }
			}

			/// <summary>
			/// Краткое имя (Фамилия, инициалы)
			/// </summary>
			[NotMapped]
			public String ShortName
			{
				get 
				{
					if(Patronymic != null)
                    {
						return ($"{Surname} {Name[0]}. {Patronymic[0]}.");
					}
					else
                    {
						return ($"{Surname} {Name[0]}.");
					}
				}
			}

			/// <summary>
			/// Идентификатор должности
			/// </summary>
			public Int64? PostId { get; set; }

			/// <summary>
			/// Должность
			/// </summary>
			[ForeignKey(nameof(PostId))]
			public CPost Post { get; set; }

			/// <summary>
			/// Наименование должности
			/// </summary>
			[NotMapped]
			public String PostShortName
			{
				get 
				{
					if(Post == null)
					{
						return ("Нет должности");
					}
					else
					{
						return (Post.ShortName);
					}
				}
			}

			/// <summary>
			/// Cферы деятельности пользователя
			/// </summary>
			public List<CFieldActivity> FieldActivities { get; set; }

			/// <summary>
			/// Список имен сфер деятельности пользователя
			/// </summary>
			[NotMapped]
			public IReadOnlyList<String> FieldActivityNames
			{
				get
				{
					return (mFieldActivityNames);
				}
				set
				{
					mFieldActivityNames.Clear();
					if (value != null && value.Count > 0)
					{
						mFieldActivityNames.AddRange(value);
					}
				}
			}

			/// <summary>
			/// Текст имен ролей пользователя
			/// </summary>
			[NotMapped]
			public String FieldActivityNameText
			{
				get
				{
					return (mFieldActivityNames.ToTextString("Нет сфер", ',', true));
				}
				set
				{
				}
			}

			/// <summary>
			/// Список имен ролей пользователя
			/// </summary>
			[NotMapped]
			public IReadOnlyList<String> RoleNames
            {
                get
                {
                    return (mRoleNames);
                }
                set
                {
                    mRoleNames.Clear();
                    if (value != null && value.Count > 0)
                    {
                        mRoleNames.AddRange(value);
                    }
                }
            }

            /// <summary>
            /// Текст имен ролей пользователя
            /// </summary>
            [NotMapped]
			public String RoleNameText
			{
				get
				{
					return (mRoleNames.ToTextString("Нет ролей", ',', true));
				}
				set
                {
                }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUser()
				: base()
			{
				FieldActivities = new List<CFieldActivity>();
				mFieldActivityNames = new List<String>();
				mRoleNames = new List<String>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="user_name">Имя пользователя</param>
			//---------------------------------------------------------------------------------------------------------
			public CUser(String user_name)
				: base(user_name)
			{
				FieldActivities = new List<CFieldActivity>();
				mFieldActivityNames = new List<String>();
				mRoleNames = new List<String>();
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка всех ролей пользователя
			/// </summary>
			/// <param name="user_managers">Менеджер пользователей</param>
			/// <returns>Задача выполения</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task LoadAllRolesName(UserManager<CUser> user_managers)
			{
                mRoleNames.Clear();
                var roles = await user_managers.GetRolesAsync(this);
                if (roles != null)
                {
                    mRoleNames.AddRange(roles);
                }
            }

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновления имен сфер деятельности пользователя
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpadeFieldActivityName()
			{
				mFieldActivityNames.Clear();
				for (Int32 i = 0; i < FieldActivities.Count; i++)
				{
					mFieldActivityNames.Add(FieldActivities[i].ShortName);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновления сфер деятельности пользователя
			/// </summary>
			/// <param name="field_activities">Сферы деятельности</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpadeFieldActivityEntity(List<CFieldActivity> field_activities)
			{
				FieldActivities.Clear();
                for (Int32 i = 0; i < mFieldActivityNames.Count; i++)
                {
                    String fan = mFieldActivityNames[i];
                    CFieldActivity field_activity = field_activities.Find((field) =>
                    {
                        return (field.ShortName == fan);
                    });
					if(field_activity != null)
                    {
						FieldActivities.Add(field_activity);
					}
                }
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================