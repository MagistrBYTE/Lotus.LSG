//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема бюджета и финансов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseFinancingBudget.cs
*		Элементы финансирования и бюджета.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
		//! \defgroup MunicipalityBaseFinancing Подсистема бюджета и финансов
		//! Подсистема бюджета и финансов определяет уровни бюджета и финансовые данные. 
		//! \ingroup MunicipalityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип уровня бюджета
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TBudgetFinancing
		{
			/// <summary>
			/// Общий бюджет
			/// </summary>
			Common,

			/// <summary>
			/// Местный бюджет
			/// </summary>
			Local,

			/// <summary>
			/// Областной бюджет
			/// </summary>
			Regional,

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			Federal,

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			Extra
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип уровня бюджета
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Flags]
		public enum TBudgetFinancingSet
		{
			/// <summary>
			/// Местный бюджет
			/// </summary>
			Local = 1,

			/// <summary>
			/// Областной бюджет
			/// </summary>
			Regional = 2,

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			Federal = 4,

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			Extra = 8
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения финансирования по уровням бюджета
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusBudgetFinancing
		{
			/// <summary>
			/// Общая стоимость
			/// </summary>
			Decimal Price { get; }

			/// <summary>
			/// Местный бюджет
			/// </summary>
			Decimal PriceLocal { get; }

			/// <summary>
			/// Областной бюджет
			/// </summary>
			Decimal PriceRegional { get; }

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			Decimal PriceFederal { get; }

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			Decimal PriceExtra { get; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения финансирования по уровням бюджета с учетом возможности учитывать в расчетах 
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusBudgetFinancingNotCalculation : ILotusBudgetFinancing, ILotusNotCalculation
		{

		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для интерфейса <see cref="ILotusBudgetFinancing"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionBudgetFinancing
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение финансирования уровня бюджета
			/// </summary>
			/// <param name="this">Интерфейс для определения финансирования по уровням бюджета</param>
			/// <param name="budget_financing">Уровень бюджета</param>
			/// <returns>Финансирование</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetBudgetFinancingOfType(this ILotusBudgetFinancing @this, TBudgetFinancing budget_financing)
			{
				switch (budget_financing)
				{
					case TBudgetFinancing.Common: return (@this.Price);
					case TBudgetFinancing.Local: return (@this.PriceLocal);
					case TBudgetFinancing.Regional: return (@this.PriceRegional);
					case TBudgetFinancing.Federal: return (@this.PriceFederal);
					case TBudgetFinancing.Extra: return (@this.PriceExtra);
					default: return (0);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение совокупного финансирования по уровням бюджета 
			/// </summary>
			/// <param name="this">Интерфейс для определения финансирования по уровням бюджета</param>
			/// <param name="budget_financing">Набор уровней бюджета</param>
			/// <returns>Совокупное финансирование</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetBudgetFinancingOfSet(this ILotusBudgetFinancing @this, TBudgetFinancingSet budget_financing)
			{
				Decimal total = 0;

				if (budget_financing.IsFlagSet(TBudgetFinancingSet.Local)) total += @this.PriceLocal;
				if (budget_financing.IsFlagSet(TBudgetFinancingSet.Regional)) total += @this.PriceRegional;
				if (budget_financing.IsFlagSet(TBudgetFinancingSet.Federal)) total += @this.PriceFederal;
				if (budget_financing.IsFlagSet(TBudgetFinancingSet.Extra)) total += @this.PriceExtra;

				return (total);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
