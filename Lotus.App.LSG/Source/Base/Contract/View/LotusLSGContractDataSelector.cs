//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль ОМСУ
// Подраздел: Подсистема представления контрактов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGContractDataSelector.cs
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
		public class CContractDataSelector : DataTemplateSelector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Глобальный экземпляр
			/// </summary>
			public static readonly CContractDataSelector Instance = new CContractDataSelector()
			{
				ContractManager = Application.Current.Resources["ContractManagerTemplateKey"] as DataTemplate,
				ContractSet = Application.Current.Resources["ContractSetTemplateKey"] as DataTemplate,
				Contract = Application.Current.Resources["ContractTemplateKey"] as DataTemplate
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Шаблон для представления менеджера контрактов
			/// </summary>
			public DataTemplate ContractManager { get; set; }

			/// <summary>
			/// Шаблон для представления набоп контрактов
			/// </summary>
			public DataTemplate ContractSet { get; set; }

			/// <summary>
			/// Шаблон для представления контракта
			/// </summary>
			public DataTemplate Contract { get; set; }
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
				CContractManager contract_manager = item as CContractManager;
				if (contract_manager != null)
				{
					return (ContractManager);
				}

				CContractSet contract_set = item as CContractSet;
				if (contract_set != null)
				{
					return (ContractSet);
				}

				CContract contract = item as CContract;
				if (contract != null)
				{
					return (Contract);
				}

				return (Contract);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================