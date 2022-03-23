//=====================================================================================================================
// Проект: Lotus.Web
// Раздел: Общий модуль
// Подраздел: Подсистема аккаунта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusWebAccountAuthorizeController.cs
*		Контролёр для аутентификации пользователя.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
		/// Контролёр для аутентификации пользователя
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Route("api/[controller]/[action]")]
		[ApiController]
		public class AuthorizeController : ControllerBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			private readonly UserManager<CUser> mUserManager;
			private readonly SignInManager<CUser> mSignInManager;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="user_manager">Менеджер пользователей</param>
			/// <param name="sign_in_manager">Менеджер авторизации</param>
			//---------------------------------------------------------------------------------------------------------
			public AuthorizeController(UserManager<CUser> user_manager, SignInManager<CUser> sign_in_manager)
			{
				mUserManager = user_manager;
				mSignInManager = sign_in_manager;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регистрация пользователя
			/// </summary>
			/// <param name="parameters">Параметры для регистрации нового пользователя</param>
			/// <returns>Общий результат работы</returns>
			//---------------------------------------------------------------------------------------------------------
			[HttpPost]
			public async Task<IActionResult> Register([FromBody] CRegisterParameters parameters)
			{
				// Создаем нового пользователя
				var user = new CUser
				{
					UserName = parameters.UserName,
					Email = parameters.Email,
					Name = parameters.Name ?? parameters.UserName,
					Surname = parameters.Surname ?? parameters.UserName,
					Patronymic = parameters.Patronymic,
					PostId = 1
				};

				// Пробуем создать
				var result = await mUserManager.CreateAsync(user, parameters.Password);
				if (!result.Succeeded)
				{
					return BadRequest(result.Errors.FirstOrDefault()?.Description);
				}

				// Если успешно сразу входим
				return await Login(new CLoginParameters
				{
					UserName = parameters.UserName,
					Password = parameters.Password
				});
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Аутентификация пользователя
			/// </summary>
			/// <param name="parameters">Параметры для аутентификации пользователя</param>
			/// <returns>Общий результат работы</returns>
			//---------------------------------------------------------------------------------------------------------
			[HttpPost]
			public async Task<IActionResult> Login([FromBody] CLoginParameters parameters)
			{
				// Пробуем найти пользователя с таким именем
				var user = await mUserManager.FindByNameAsync(parameters.UserName);

				if (user == null)
				{
					return BadRequest("Пользователь с таким именем не обнаружен");
				}

				// Проверяем пароль
				var sing_in_result = await mSignInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
				if (!sing_in_result.Succeeded)
				{
					return BadRequest("Неверный пароль");
				}

				// Входим
				await mSignInManager.SignInAsync(user, parameters.RememberMe);

				return Ok();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выход из статуса аутентификации пользователя
			/// </summary>
			/// <returns>Общий результат работы</returns>
			//---------------------------------------------------------------------------------------------------------
			[Authorize]
			[HttpPost]
			public async Task<IActionResult> Logout()
			{
				await mSignInManager.SignOutAsync();
				return Ok();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение информации о статусе аутентификации текущего пользователя
			/// </summary>
			/// <returns>Информация о статусе аутентификации текущего пользователя</returns>
			//---------------------------------------------------------------------------------------------------------
			[HttpGet]
			public CUserAuthorizeInfo UserAuthorizeInfo()
			{
				// Устанавливаем тип контента ответа
				Response.ContentType = XMIMETypes.APP_JSON;

				// Создаем информацию о пользователе
				CUserAuthorizeInfo userAuthorizeInfo = new CUserAuthorizeInfo(User);
				return (userAuthorizeInfo);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================