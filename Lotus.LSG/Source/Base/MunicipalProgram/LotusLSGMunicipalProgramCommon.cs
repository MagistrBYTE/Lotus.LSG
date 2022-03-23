﻿//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема муниципальных программ
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGMunicipalProgramCommon.cs
*		Общие типы и структуры данных для определения инфраструктуры цифровизации муниципальных программ.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityBaseProgram Подсистема муниципальных программ
		//! Подсистема муниципальных программ. 
		//! \ingroup MunicipalityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Обобщенный интерфейс сущности муниципальной программы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusMunicipalProgramItem : ILotusNameable, ILotusNotCalculation
		{
			//
			// ОБЩИЕ ДАННЫЕ
			//
			/// <summary>
			/// Номер сущности муниципальной программы
			/// </summary>
			String Number { get; set; }

			/// <summary>
			/// Описание сущности муниципальной программы
			/// </summary>
			String Desc { get; set; }

			/// <summary>
			/// Группа которой принадлежит сущность муниципальной программы
			/// </summary>
			String Group { get; set; }

			/// <summary>
			/// Подгруппа которой принадлежит сущность муниципальной программы
			/// </summary>
			String SubGroup { get; set; }

			/// <summary>
			/// Дата начало сущности муниципальной программы
			/// </summary>
			DateTime BeginDate { get; set; }

			/// <summary>
			/// Дата окончания сущности муниципальной программы
			/// </summary>
			DateTime EndDate { get; set; }

			//
			// МУНИЦИПАЛЬНАЯ ПРОГРАММА
			//
			/// <summary>
			/// Муниципальная программа
			/// </summary>
			CMunicipalProgram Program { get; set; }

			/// <summary>
			/// Идентификатор муниципальной программы
			/// </summary>
			Int32? ProgramId { get; set; }

			/// <summary>
			/// Наименование муниципальной программы
			/// </summary>
			String ProgramName { get; }

			//
			// МУНИЦИПАЛЬНАЯ ПОДПРОГРАММА
			//
			/// <summary>
			/// Муниципальная подпрограмма
			/// </summary>
			CMunicipalSubProgram SubProgram { get; set; }

			/// <summary>
			/// Идентификатор муниципальной подпрограммы
			/// </summary>
			Int32? SubProgramId { get; set; }

			/// <summary>
			/// Наименование муниципальной подпрограммы
			/// </summary>
			String SubProgramName { get; }

			//
			// ИНДИКАТОР МУНИЦИПАЛЬНОЙ ПРОГРАММЫ/ПОДПРОГРАММЫ
			//
			/// <summary>
			/// Индикатор муниципальной программы/подпрограммы
			/// </summary>
			CMunicipalProgramIndicator Indicator { get; set; }

			/// <summary>
			/// Идентификатор индикатора муниципальной программы/подпрограммы
			/// </summary>
			Int32? IndicatorId { get; set; }

			/// <summary>
			/// Наименование индикатора муниципальной подпрограммы
			/// </summary>
			String IndicatorName { get; }

			//
			// ОТВЕТСТВЕННЫЙ ИСПОЛНИТЕЛЬ
			//
			/// <summary>
			/// Ответственный исполнитель
			/// </summary>
			CSubjectCivil Executor { get; set; }

			/// <summary>
			/// Идентификатор ответственного исполнителя
			/// </summary>
			Int32? ExecutorId { get; set; }

			/// <summary>
			/// Наименование ответственного исполнителя
			/// </summary>
			String ExecutorName { get; }

			//
			// ТЕРРИТОРИЯ ИСПОЛНЕНИЯ
			//
			/// <summary>
			/// Сельское поселение на территории которого реализуется мероприятие 
			/// </summary>
			CAddressVillageSettlement VillageSettlement { get; set; }

			/// <summary>
			/// Идентификатор ответственного исполнителя
			/// </summary>
			Int32? VillageSettlementId { get; set; }

			/// <summary>
			/// Наименование ответственного исполнителя
			/// </summary>
			String VillageSettlementName { get; }
		}


		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс мероприятия муниципальной программы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusMunicipalProgramActivity
		{
			/// <summary>
			/// Краткое наименование подпрограммы
			/// </summary>
			String SubProgramName { get; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс индикатора муниципальной программы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusMunicipalProgramIndicator : ILotusNameable, ILotusNotCalculation
		{
			/// <summary>
			/// Краткое наименование подпрограммы
			/// </summary>
			String SubProgramName { get; }

			/// <summary>
			/// Описание индикатора
			/// </summary>
			String Desc { get; }

			/// <summary>
			/// Год индикатора
			/// </summary>
			Int32 Year { get; }

			/// <summary>
			/// Основной показатель целевого индикатора
			/// </summary>
			//TMeasurementValue PlanedValue { get; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Коды и идентификаторы муниципальных программ
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMunicipalProgramData
		{
			/// <summary>
			/// Идентификатор муниципальной программы по дорогам 
			/// </summary>
			public const Int32 ROAD_Id = 1;

			/// <summary>
			/// Идентификатор муниципальной программы по ДКЖ 
			/// </summary>
			public const Int32 HOUSING_Id = 2;
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Коды и идентификаторы муниципальных подпрограмм
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMunicipalSubProgramData
		{
			/// <summary>
			/// Идентификатор муниципальной программы по ремонту дорог 
			/// </summary>
			public const Int32 ROAD_REPAIRS_Id = 1;

			/// <summary>
			/// Идентификатор муниципальной программы по безопасности дорог 
			/// </summary>
			public const Int32 ROAD_SECURITY_Id = 2;
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Список групп для мероприятий
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMunicipalGroupData
		{
			/// <summary>
			/// Группа контрактов по дорожной деятельности
			/// </summary>
			public static readonly String[] Roads = new String[]
			{
				"Летнее содержание",
				"Зимнее содержание",
				"Содержание автодорог",
				"Ремонт",
				"Капитальный ремонт",
				"Реконструкция",
				"Разработка ПСД",
				"Иные обязательства"
			};
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================