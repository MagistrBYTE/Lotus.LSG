//=====================================================================================================================
// Проект: Lotus
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUrbanPlanningView.cs
*		Отображение модели и селекторы шаблонов.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 29.11.2015
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
using System.Windows;
using System.Windows.Controls;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Windows;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace ISUD
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup FunctionalISUD
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Селектор шаблона данных для отображения иерархии элементов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUrbanPlanningSolutionDataSelector : DataTemplateSelector
		{
			#region ======================================= ДАННЫЕ ====================================================
			//
			// ВСЕ ЗЕМЛИ ПО КАТЕГОРИЯМ
			//
			/// <summary>
			/// Шаблон для представления раздела - Все земли по категориям
			/// </summary>
			public DataTemplate LandInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Земли по катогориям
			/// </summary>
			public DataTemplate Land { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Земли по катогориям
			/// </summary>
			public DataTemplate LandProtected { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Элемент участка земли
			/// </summary>
			public DataTemplate LandElement { get; set; }

			//
			// ВСЕ ТЕРРИТОРИИ СПЕЦИАЛЬНОГО НАЗНАЧЕНИЯ
			//
			/// <summary>
			/// Шаблон для представления раздела - Все территории специального назначения
			/// </summary>
			public DataTemplate SpecialInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Список территорий специального назначения одного типа
			/// </summary>
			public DataTemplate Special { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Элемент территорий специального назначения
			/// </summary>
			public DataTemplate SpecialElements { get; set; }

			//
			// ОБЪЕКТЫ ПЕРЕВОДА
			//

			/// <summary>
			/// Шаблон для представления раздела - Все объекты перевода
			/// </summary>
			public DataTemplate TransferInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Объект перевода
			/// </summary>
			public DataTemplate Transfer { get; set; }

			//
			// ИНЖЕНЕРНАЯ ИНФРАСТРУКТУРА
			//
			/// <summary>
			/// Шаблон для представления раздела - Инженерная инфраструктура
			/// </summary>
			public DataTemplate EngineeringInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Водоснабжение
			/// </summary>
			public DataTemplate WaterSupply { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Электроснабжение
			/// </summary>
			public DataTemplate PowerSupply { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Газоснабжение
			/// </summary>
			public DataTemplate GasSupply { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Теплоснабжение
			/// </summary>
			public DataTemplate HeatSupply { get; set; }

			//
			// ТРАНСПОРТНАЯ ИНФРАСТРУКТУРА
			//
			/// <summary>
			/// Шаблон для представления раздела - Дороги
			/// </summary>
			public DataTemplate RoadInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Элемент дороги
			/// </summary>
			public DataTemplate RoadElement { get; set; }

			//
			// ЖИЛИЩНАЯ ИНФРАСТРУКТУРА
			//
			/// <summary>
			/// Шаблон для представления раздела - Жилищная инфраструктура
			/// </summary>
			public DataTemplate HousingInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Элемент жилищной инфраструктуры
			/// </summary>
			public DataTemplate HouseElement { get; set; }

			//
			// СОЦИАЛЬНАЯ ИНФРАСТРУКТУРА
			//
			/// <summary>
			/// Шаблон для представления раздела - Социальная инфраструктура
			/// </summary>
			public DataTemplate SocialInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Элемент социальной инфраструктуры
			/// </summary>
			public DataTemplate SocialElement { get; set; }

			//
			// НАСЕЛЕННЫЕ ПУНКТЫ
			//
			/// <summary>
			/// Шаблон для представления раздела - Населенные пункты
			/// </summary>
			public DataTemplate VillageInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Населенный пункт
			/// </summary>
			public DataTemplate Village { get; set; }

			//
			// СЕЛЬСКИЕ ПОСЕЛЕНИЯ
			//
			/// <summary>
			/// Шаблон для представления раздела - Сельские поселения
			/// </summary>
			public DataTemplate VillageSettlementInfrastructure { get; set; }

			/// <summary>
			/// Шаблон для представления раздела - Сельские поселения
			/// </summary>
			public DataTemplate VillageSettlement { get; set; }


			/// <summary>
			/// Шаблон для представления элемента
			/// </summary>
			public DataTemplate ElementDataTemplate { get; set; }
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
				//
				// ВСЕ ЗЕМЛИ ПО КАТЕГОРИЯМ
				//
				CLandInfrastructure land_infrastructure = item as CLandInfrastructure;
				if (land_infrastructure != null)
				{
					return (LandInfrastructure);
				}

				//CLand land = item as CLand;
				//if (land != null)
				//{
				//	return (Land);
				//}

				//CLandProtected land_protected = item as CLandProtected;
				//if (land_protected != null)
				//{
				//	return (LandProtected);
				//}

				CLandElement land_element = item as CLandElement;
				if (land_element != null)
				{
					return (LandElement);
				}

				//
				// ВСЕ ТЕРРИТОРИИ СПЕЦИАЛЬНОГО НАЗНАЧЕНИЯ
				//
				CSpecialInfrastructure special_infrastructure = item as CSpecialInfrastructure;
				if (special_infrastructure != null)
				{
					return (SpecialInfrastructure);
				}

				CSpecial special = item as CSpecial;
				if (special != null)
				{
					return (Special);
				}

				CSpecialElement special_element = item as CSpecialElement;
				if (special_element != null)
				{
					return (SpecialElements);
				}

				//
				// ОБЪЕКТЫ ПЕРЕВОДА
				//
				CTransferInfrastructure transfer_infrastructure = item as CTransferInfrastructure;
				if (transfer_infrastructure != null)
				{
					return (TransferInfrastructure);
				}

				CTransfer transfer = item as CTransfer;
				if (transfer != null)
				{
					return (Transfer);
				}

				//
				// ИНЖЕНЕРНАЯ ИНФРАСТРУКТУРА
				//
				CEngineeringInfrastructure eng_infrastructure = item as CEngineeringInfrastructure;
				if (eng_infrastructure != null)
				{
					return (EngineeringInfrastructure);
				}

				CWaterSupply water_supply = item as CWaterSupply;
				if (water_supply != null)
				{
					return (WaterSupply);
				}

				CPowerSupply power_supply = item as CPowerSupply;
				if (power_supply != null)
				{
					return (PowerSupply);
				}

				CGasSupply gas_supply = item as CGasSupply;
				if (gas_supply != null)
				{
					return (GasSupply);
				}

				CHeatSupply heat_supply = item as CHeatSupply;
				if (heat_supply != null)
				{
					return (HeatSupply);
				}

				//
				// ТРАНСПОРТНАЯ ИНФРАСТРУКТУРА
				//
				CRoadInfrastructure road_infrastructure = item as CRoadInfrastructure;
				if (road_infrastructure != null)
				{
					return (RoadInfrastructure);
				}

				CRoadElement road_element = item as CRoadElement;
				if (road_element != null)
				{
					return (RoadElement);
				}

				//
				// ЖИЛИЩНАЯ ИНФРАСТРУКТУРА
				//
				CHousingInfrastructure house_infrastructure = item as CHousingInfrastructure;
				if (house_infrastructure != null)
				{
					return (HousingInfrastructure);
				}

				CHouseElement house_element = item as CHouseElement;
				if (house_element != null)
				{
					return (HouseElement);
				}

				//
				// СОЦИАЛЬНАЯ ИНФРАСТРУКТУРА
				//
				CSocialInfrastructure social_infrastructure = item as CSocialInfrastructure;
				if (social_infrastructure != null)
				{
					return (SocialInfrastructure);
				}

				CSocialElement social_element = item as CSocialElement;
				if (social_element != null)
				{
					return (SocialElement);
				}


				//
				// НАСЕЛЕННЫЕ ПУНКТЫ
				//
				CVillageInfrastructure village_infrastructure = item as CVillageInfrastructure;
				if (village_infrastructure != null)
				{
					return (VillageInfrastructure);
				}

				CVillage village = item as CVillage;
				if (village != null)
				{
					return (Village);
				}

				//
				// СЕЛЬСКИЕ ПОСЕЛЕНИЯ
				//
				CVillageSettlementInfrastructure village_settlement_infrastructure = item as CVillageSettlementInfrastructure;
				if (village_settlement_infrastructure != null)
				{
					return (VillageSettlementInfrastructure);
				}

				CVillageSettlement village_settlement = item as CVillageSettlement;
				if (village_settlement != null)
				{
					return (VillageSettlement);
				}

				return (ElementDataTemplate);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Селектор стиля
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUrbanPlanningStyleNotCalculation : StyleSelector
		{
			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Стиль для отображения элемента который не учитывается
			/// </summary>
			public Style NotCalculationStyle { get; set; }
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
				if (item is CUrbanPlanningItem)
				{
					CUrbanPlanningItem planning = item as CUrbanPlanningItem;
					if (planning.NotCalculation)
					{
						return (NotCalculationStyle);
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