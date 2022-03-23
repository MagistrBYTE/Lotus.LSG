//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль инженерной инфраструктуры
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGInfrastructure.cs
*		Общие типы и структуры данных инженерной инфраструктуры.
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
		//! \defgroup MunicipalityInfrastructure Модуль инженерной инфраструктуры
		//! Базовый модуль определяет общие данные для общей цифровизации управления
		//! \ingroup Municipality
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип элемента инженерной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TEngineeringType>))]
		public enum TEngineeringType
		{
			/// <summary>
			/// Водоснабжение
			/// </summary>
			[Description("Водоснабжение")]
			WaterSupply,

			/// <summary>
			/// Электроснабжение
			/// </summary>
			[Description("Электроснабжение")]
			PowerSupply,

			/// <summary>
			/// Газоснабжение
			/// </summary>
			[Description("Газоснабжение")]
			GasSupply,

			/// <summary>
			/// Теплоснабжение
			/// </summary>
			[Description("Теплоснабжение")]
			HeatSupply
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый элемент инженерной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CEngineeringElement : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsEngineeringType = new PropertyChangedEventArgs(nameof(EngineeringType));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TEngineeringType mEngineeringType;
			internal CEngineeringInfrastructure mEngineeringInfra;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип дома
			/// </summary>
			[DisplayName("Тип элемента")]
			[Description("Тип элемента инженерной инфраструктуры")]
			[Category("Основные параметры")]
			//[Display(Name = "Тип элемента", Order = 0, GroupName = "2. Основные параметры")]
			[XmlAttribute]
			public TEngineeringType EngineeringType
			{
				get { return (mEngineeringType); }
				set
				{
					mEngineeringType = value;
					mName = mEngineeringType.GetDescriptionOrName();
					NotifyPropertyChanged(PropertyArgsEngineeringType);
					NotifyPropertyChanged(PropertyArgsName);
				}
			}

			/// <summary>
			/// Принадлежность к классу инженерной инфраструктуры
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CEngineeringInfrastructure EngineeringInfra
			{
				get { return (mEngineeringInfra); }
				set
				{
					mEngineeringInfra = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CEngineeringElement()
			{
				mName = mEngineeringType.GetDescriptionOrName();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CEngineeringElement(String name)
					: base(name)
			{
				mName = mEngineeringType.GetDescriptionOrName();
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой информацию о инженерной инфраструктуре
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CEngineeringInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================

			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal CWaterSupply mWaterSupply = new CWaterSupply();
			internal CPowerSupply mPowerSupply = new CPowerSupply();
			internal CGasSupply mGasSupply = new CGasSupply();
			internal CHeatSupply mHeatSupply = new CHeatSupply();
			internal ObservableCollection<CEngineeringElement> mEngineeringElements;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Водоснабжение
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CWaterSupply WaterSupply
			{
				get { return (mWaterSupply); }
				set { mWaterSupply = value; }
			}

			/// <summary>
			/// Электроснабжение
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CPowerSupply PowerSupply
			{
				get { return (mPowerSupply); }
				set { mPowerSupply = value; }
			}

			/// <summary>
			/// Газоснабжение
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CGasSupply GasSupply
			{
				get { return (mGasSupply); }
				set { mGasSupply = value; }
			}

			/// <summary>
			/// Теплоснабжение
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CHeatSupply HeatSupply
			{
				get { return (mHeatSupply); }
				set { mHeatSupply = value; }
			}

			/// <summary>
			/// Список всех направлений инженерной инфраструктуры
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public ObservableCollection<CEngineeringElement> EngineeringElements
			{
				get
				{
					if (mEngineeringElements == null)
					{
						mEngineeringElements = new ObservableCollection<CEngineeringElement>();
						mEngineeringElements.Add(mWaterSupply);
						mEngineeringElements.Add(mPowerSupply);
						mEngineeringElements.Add(mGasSupply);
						mEngineeringElements.Add(mHeatSupply);
					}

					return (mEngineeringElements);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CEngineeringInfrastructure()
			{
				mName = "Инженерная инфраструктура";
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="engineering_infrastructure">Инженерная инфраструктура</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CEngineeringInfrastructure engineering_infrastructure)
			{
				WaterSupply.Union(engineering_infrastructure.WaterSupply);
				PowerSupply.Union(engineering_infrastructure.PowerSupply);
				GasSupply.Union(engineering_infrastructure.GasSupply);
				HeatSupply.Union(engineering_infrastructure.HeatSupply);
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ЭЛЕМЕНТАМИ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение источника данных связанного с этим объектом
			/// </summary>
			/// <returns>Источник данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Object GetItemSource()
			{
				return (mEngineeringElements);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка дочерних элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void SortChildElements()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Группировка дочерних элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void GroupChildElements()
			{

			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================