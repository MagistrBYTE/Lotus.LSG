//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль ОМСУ
// Подраздел: Подсистема муниципальных программ
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGMunicipalProgramDataSelector.cs
*		Определение селекторов шаблона модели и стилей для отображения модели.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 16.09.2018
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Controls;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Селектор шаблона данных для отображения иерархии элементов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CMunicipalProgramDataSelector : DataTemplateSelector
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Суффикс для шаблонов данных
			/// </summary>
			private const String TEMPLATE_KEY = "TemplateKey";
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Глобальный экземпляр
			/// </summary>
			public static readonly CMunicipalProgramDataSelector Instance = new CMunicipalProgramDataSelector()
			{
				Program = Application.Current.Resources[nameof(Program) + TEMPLATE_KEY] as DataTemplate,
				SubProgram = Application.Current.Resources[nameof(SubProgram) + TEMPLATE_KEY] as DataTemplate,
				Indicators = Application.Current.Resources[nameof(Indicators) + TEMPLATE_KEY] as DataTemplate,
				Indicator = Application.Current.Resources[nameof(Indicator) + TEMPLATE_KEY] as DataTemplate,
				Activities = Application.Current.Resources[nameof(Activities) + TEMPLATE_KEY] as DataTemplate,
				Activity = Application.Current.Resources[nameof(Activity) + TEMPLATE_KEY] as DataTemplate
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Шаблон для представления программы
			/// </summary>
			public DataTemplate Program { get; set; }

			/// <summary>
			/// Шаблон для представления подпрограммы
			/// </summary>
			public DataTemplate SubProgram { get; set; }

			/// <summary>
			/// Шаблон для представления целевых индикаторов программы
			/// </summary>
			public DataTemplate Indicators { get; set; }

			/// <summary>
			/// Шаблон для представления целевого индикатора программы
			/// </summary>
			public DataTemplate Indicator { get; set; }

			/// <summary>
			/// Шаблон для представления мероприятия программы
			/// </summary>
			public DataTemplate Activities { get; set; }

			/// <summary>
			/// Шаблон для представления мероприятия
			/// </summary>
			public DataTemplate Activity { get; set; }
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выбор шаблона привязки данных
			/// </summary>
			/// <param name="item">Объект</param>
			/// <param name="container">Контейнер</param>
			/// <returns>Нужный шаблон</returns>
			//---------------------------------------------------------------------------------------------------------
			public override DataTemplate SelectTemplate(Object item, DependencyObject container)
			{
				CMunicipalProgram mp = item as CMunicipalProgram;
				if (mp != null)
				{
					return (Program);
				}

				CMunicipalProgramIndicators indicators = item as CMunicipalProgramIndicators;
				if (indicators != null)
				{
					return (Indicators);
				}

				CMunicipalProgramIndicator indicator = item as CMunicipalProgramIndicator;
				if (indicator != null)
				{
					return (Indicator);
				}

				CMunicipalProgramActivities activities = item as CMunicipalProgramActivities;
				if (activities != null)
				{
					return (Activities);
				}

				CMunicipalProgramActivity activity = item as CMunicipalProgramActivity;
				if (activity != null)
				{
					return (Activity);
				}


				return (Activity);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Селектор стиля
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CMunicipalProgramStyleSelector : StyleSelector
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Суффикс для стилей
			/// </summary>
			private const String STYLE_KEY = "StyleKey";
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Глобальный экземпляр
			/// </summary>
			public static readonly CMunicipalProgramStyleSelector Instance = new CMunicipalProgramStyleSelector()
			{
				NotCalculation = Application.Current.Resources[nameof(NotCalculation) + STYLE_KEY] as Style,
				Verified = Application.Current.Resources[nameof(Verified) + STYLE_KEY] as Style,
				Presented = Application.Current.Resources[nameof(Presented) + STYLE_KEY] as Style
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Стиль для отображения элемента который не учитывается
			/// </summary>
			public Style NotCalculation { get; set; }

			/// <summary>
			/// Стиль для отображения элемента который верифицирован
			/// </summary>
			public Style Verified { get; set; }

			/// <summary>
			/// Стиль для отображения элемента который отображается
			/// </summary>
			public Style Presented { get; set; }
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выбор стиля данных
			/// </summary>
			/// <param name="item">Объект</param>
			/// <param name="container">Контейнер</param>
			/// <returns>Нужный стиль</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Style SelectStyle(Object item, DependencyObject container)
			{
				if (item is ILotusNotCalculation)
				{
					ILotusNotCalculation no_calculation = item as ILotusNotCalculation;
					if (no_calculation.NotCalculation)
					{
						return (NotCalculation);
					}
				}

				return (null);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================