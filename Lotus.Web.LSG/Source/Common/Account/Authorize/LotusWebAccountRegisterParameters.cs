﻿//=====================================================================================================================
// Проект: Lotus.Web
// Раздел: Общий модуль
// Подраздел: Подсистема аккаунта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusWebAccountRegisterParameters.cs
*		Класс определяющий минимально необходимые параметры для регистрации нового пользователя.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
		/// Класс определяющий минимально необходимые параметры для регистрации нового пользователя
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRegisterParameters
		{
			/// <summary>
			/// Email
			/// </summary>
			[Required]
			[Display(Name = "Email")]
			public String Email { get; set; }

			/// <summary>
			/// Имя пользователя
			/// </summary>
			[Required]
			[Display(Name = "Имя пользователя")]
			public String UserName { get; set; }

			/// <summary>
			/// Пароль
			/// </summary>
			[Required]
			[DataType(DataType.Password)]
			[Display(Name = "Пароль")]
			public String Password { get; set; }

			/// <summary>
			/// Подтвердить пароль
			/// </summary>
			[Required]
			[Compare("Password", ErrorMessage = "Пароли не совпадают")]
			[DataType(DataType.Password)]
			[Display(Name = "Подтвердить пароль")]
			public String PasswordConfirm { get; set; }

			/// <summary>
			/// Имя пользователя
			/// </summary>
			[Display(Name = "Имя пользователя")]
			public String? Name { get; set; }

			/// <summary>
			/// Фамилия пользователя
			/// </summary>
			[Display(Name = "Фамилия пользователя")]
			public String? Surname { get; set; }

			/// <summary>
			/// Отчество пользователя
			/// </summary>
			[Display(Name = "Отчество пользователя")]
			public String? Patronymic { get; set; }
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================