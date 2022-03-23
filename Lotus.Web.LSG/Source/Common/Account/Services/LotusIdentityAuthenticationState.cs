//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
		/// Класс для информирования о состоянии валидации пользователя для компонентов Razor
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CIdentityAuthenticationStateProvider : ServerAuthenticationStateProvider
		{
			#region ======================================= ДАННЫЕ ====================================================
			private CUserAuthorizeInfo mUserInfoCache;
			private readonly ILotusAuthorizeApi mAuthorizeApi;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="authorize_api">Интерфейс минимального механизма авторизации пользователя</param>
			//---------------------------------------------------------------------------------------------------------
			public CIdentityAuthenticationStateProvider(ILotusAuthorizeApi authorize_api)
			{
				this.mAuthorizeApi = authorize_api;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Аутентификация пользователя
			/// </summary>
			/// <param name="login_parameters">Параметры для аутентификации пользователя</param>
			/// <returns>Общий результат работы</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task Login(CLoginParameters login_parameters)
			{
				await mAuthorizeApi.Login(login_parameters);
				NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регистрация пользователя
			/// </summary>
			/// <param name="register_parameters">Параметры для регистрации нового пользователя</param>
			/// <returns>Общий результат работы</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task Register(CRegisterParameters register_parameters)
			{
				await mAuthorizeApi.Register(register_parameters);
				NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выход из статуса аутентификации пользователя
			/// </summary>
			/// <returns>Общий результат работы</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task Logout()
			{
				await mAuthorizeApi.Logout();
				mUserInfoCache = null;
				NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение информации о статусе аутентификации текущего пользователя
			/// </summary>
			/// <returns>Информация о статусе аутентификации текущего пользователя</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task<CUserAuthorizeInfo> GetUserAuthorizeInfo()
			{
				if (mUserInfoCache != null && mUserInfoCache.IsAuthenticated)
				{
					return mUserInfoCache;
				}

				mUserInfoCache = await mAuthorizeApi.GetUserAuthorizeInfo();
				
				return mUserInfoCache;
			}
			#endregion

			#region ======================================= ПЕРЕГРУЖЕННЫЕ МЕТОДЫ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение состояния аутентификации пользователя
			/// </summary>
			/// <returns>Состояние аутентификации пользователя</returns>
			//---------------------------------------------------------------------------------------------------------
			public override async Task<AuthenticationState> GetAuthenticationStateAsync()
			{
				var identity = new ClaimsIdentity();
				try
				{
					var userInfo = await GetUserAuthorizeInfo();

					if (userInfo.IsAuthenticated)
					{
						var claims = new[] { new Claim(ClaimTypes.Name, mUserInfoCache.UserName) }.Concat(mUserInfoCache.ExposedClaims.Select(c => new Claim(c.Key, c.Value)));
						identity = new ClaimsIdentity(claims, "Server authentication");
					}
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine("Request failed:" + ex.ToString());
				}

				return new AuthenticationState(new ClaimsPrincipal(identity));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================