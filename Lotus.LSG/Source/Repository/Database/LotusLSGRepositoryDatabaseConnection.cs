//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль репозитория
// Подраздел: Подсистема базы данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRepositoryDatabaseConnection.cs
*		Параметры подключения баз данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityRepositoryDatabase Подсистема базы данных
		//! Подсистема базы данных обеспечивает хранение все информации в базе данных MySQL.
		//! \ingroup MunicipalityRepository
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для определения данных для подключения к базе данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDbContextConnection
		{
			#region ======================================= ДАННЫЕ ====================================================
			//
			// Параметры удаленного сервера
			//
			/// <summary>
			/// Имя сервера
			/// </summary>
			public static String REMOTE_SERVER = "mysql99.1gb.ru";

			/// <summary>
			/// Имя порта
			/// </summary>
			public static String REMOTE_PORT = "3306";

			/// <summary>
			/// Имя базы данных
			/// </summary>
			public static String REMOTE_DATABASE = "gb_localgov1"; 
			/// <summary>
			/// Имя пользователя
			/// </summary>
			public static String REMOTE_USER = "gb_localgov1"; 

			/// <summary>
			/// Пароль
			/// </summary>
			public static String REMOTE_PASSWORD = "752za46az45";

			//
			// Параметры локального сервера
			//
			/// <summary>
			/// Имя сервера
			/// </summary>
			public static String LOCAL_SERVER = "localhost";

			/// <summary>
			/// Имя порта
			/// </summary>
			public static String LOCAL_PORT = "3306";

			/// <summary>
			/// Имя базы данных
			/// </summary>
			public static String LOCAL_DATABASE = "localselfgovernment";

			/// <summary>
			/// Имя пользователя
			/// </summary>
			public static String LOCAL_USER = "root";

			/// <summary>
			/// Пароль
			/// </summary>
			public static String LOCAL_PASSWORD = "1111";
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение строки подключения к удаленной базе данных
			/// </summary>
			/// <returns>Строка подключения к базе данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetRemoteConnectString()
			{
				String connect_string =
				String.Format("server={0};user={1};port={2};database={3};password={4};ConvertZeroDateTime=True",
					REMOTE_SERVER,
					REMOTE_USER,
					REMOTE_PORT,
					REMOTE_DATABASE, 
					REMOTE_PASSWORD);
				return (connect_string);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение строки подключения к локальной базе данных
			/// </summary>
			/// <returns>Строка подключения к базе данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetLocalConnectString()
			{
				String connect_string =
				String.Format("server={0};user={1};port={2};database={3};password={4};ConvertZeroDateTime=True",
					LOCAL_SERVER,
					LOCAL_USER,
					LOCAL_PORT,
					LOCAL_DATABASE,
					LOCAL_PASSWORD);
				return (connect_string);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение строки подключения к базе данных
			/// </summary>
			/// <param name="is_remote">Статус подключения к удаленной базе данных</param>
			/// <returns>Строка подключения к базе данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetConnectString(Boolean is_remote)
			{
				if(is_remote)
				{
					return (GetRemoteConnectString());
				}
				else
				{
					return (GetLocalConnectString());
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