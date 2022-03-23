//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль ОМСУ
// Подраздел: Подсистема представления контрактов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGFinancingEntityDataSelector.cs
*		Определение селекторов шаблона модели и стилей для отображения модели.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
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
		//! \addtogroup MunicipalityContract
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Селектор шаблона данных для отображения иерархии контрактов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CFinancingEntityDataSelector : DataTemplateSelector
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
			public static readonly CFinancingEntityDataSelector Instance = new CFinancingEntityDataSelector()
			{
				FinancingEntityManager = Application.Current.Resources[nameof(FinancingEntityManager) + TEMPLATE_KEY] as DataTemplate,
				FinancingEntitySet = Application.Current.Resources[nameof(FinancingEntitySet) + TEMPLATE_KEY] as DataTemplate,
				FinancingEntity = Application.Current.Resources[nameof(FinancingEntity) + TEMPLATE_KEY] as DataTemplate
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Шаблон для представления менеджера контрактов
			/// </summary>
			public DataTemplate FinancingEntityManager { get; set; }

			/// <summary>
			/// Шаблон для представления набоп контрактов
			/// </summary>
			public DataTemplate FinancingEntitySet { get; set; }

			/// <summary>
			/// Шаблон для представления контракта
			/// </summary>
			public DataTemplate FinancingEntity { get; set; }
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
				CFinancingEntityManager contract_manager = item as CFinancingEntityManager;
				if (contract_manager != null)
				{
					return (FinancingEntityManager);
				}

				CFinancingEntitySet contract_set = item as CFinancingEntitySet;
				if (contract_set != null)
				{
					return (FinancingEntitySet);
				}

				CFinancingEntity contract = item as CFinancingEntity;
				if (contract != null)
				{
					return (FinancingEntity);
				}

				return (FinancingEntity);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================