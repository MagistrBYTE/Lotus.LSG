//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusClientTemplate
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUrbanPlanningVillage.cs
*		Населенный пункт.
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
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityPlanRegions Подсистема территориального планирования
		//! Подсистема территориального планирования обеспечивает представление данных на различных элементах
		//! планировочной структуры в соответствии с действующим законодательством
		//! \ingroup MunicipalityPlan
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Населенный пункт
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[TypeConverter(typeof(CVillageTypeConverter))]
		public class CVillage : CUrbanPlanningItem, IAreaProjected
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsAreaProjected = new PropertyChangedEventArgs(nameof(AreaProjected));

			// Население
			protected static PropertyChangedEventArgs PropertyArgsPopulationNumber = new PropertyChangedEventArgs(nameof(PopulationNumber));
			protected static PropertyChangedEventArgs PropertyArgsPopulationDensity = new PropertyChangedEventArgs(nameof(PopulationDensity));
			protected static PropertyChangedEventArgs PropertyArgsPopulationUnderWorking = new PropertyChangedEventArgs(nameof(PopulationUnderWorking));
			protected static PropertyChangedEventArgs PropertyArgsPopulationWorking = new PropertyChangedEventArgs(nameof(PopulationNumber));
			protected static PropertyChangedEventArgs PropertyArgsPopulationOverWorking = new PropertyChangedEventArgs(nameof(PopulationOverWorking));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Double mArea;
			internal Double mAreaProjected;

			// Территориальные зоны
			//internal CLand mZones;

			// Территории специального назначения
			internal CSpecialInfrastructure mSpecialInfrastructure;

			// Транспортная инфраструктура
			internal CRoadInfrastructure mRoads;

			// Жилищная инфраструктура
			internal CHousingInfrastructure mHousing;

			// Социальная инфраструктура
			internal CSocialInfrastructure mSocial;

			// Население
			internal TValueInt mPopulationNumber;
			internal Int32 mPopulationUnderWorking;
			internal Int32 mPopulationWorking;
			internal Int32 mPopulationOverWorking;

			internal ObservableCollection<CUrbanPlanningItem> mItems;
			internal CVillageSettlement mOwner;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Площадь населенного пункта
			/// </summary>
			[DisplayName("Площадь")]
			[Description("Площадь населенного пункта, га")]
			[Category("Территория")]
			[XmlAttribute]
			public Double Area
			{
				get { return (mArea); }
				set
				{
					mArea = value;
					NotifyPropertyChanged(PropertyArgsArea);
					NotifyPropertyChanged(PropertyArgsPopulationDensity);
				}
			}

			/// <summary>
			/// Площадь населенного пункта
			/// </summary>
			[DisplayName("Планируемая площадь")]
			[Description("Планируемая площадь населенного пункта, га")]
			[Category("Территория")]
			[XmlAttribute]
			public Double AreaProjected
			{
				get { return (mAreaProjected); }
				set
				{
					mAreaProjected = value;
					NotifyPropertyChanged(PropertyArgsAreaProjected);
				}
			}

			/// <summary>
			/// Территориальные зоны
			/// </summary>
			//[Browsable(false)]
			//[XmlElement]
			//public CLand Zones
			//{
			//	get { return (mZones); }
			//	set
			//	{
			//		mZones = value;
			//	}
			//}

			/// <summary>
			/// Территории специального назначения
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CSpecialInfrastructure SpecialInfrastructure
			{
				get { return (mSpecialInfrastructure); }
				set { mSpecialInfrastructure = value; }
			}

			/// <summary>
			/// Транспортная инфраструктура
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CRoadInfrastructure Roads
			{
				get { return (mRoads); }
				set
				{
					mRoads = value;
				}
			}

			/// <summary>
			/// Жилищная инфраструктура
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CHousingInfrastructure Housing
			{
				get { return (mHousing); }
				set
				{
					mHousing = value;
				}
			}

			/// <summary>
			/// Социальная инфраструктура
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CSocialInfrastructure Social
			{
				get { return (mSocial); }
				set
				{
					mSocial = value;
				}
			}

			//
			// НАСЕЛЕНИЕ
			//
			/// <summary>
			/// Общая численность населения
			/// </summary>
			[DisplayName("Численность")]
			[Description("Общая численность населения")]
			[Category("Население")]
			[XmlElement]
			public TValueInt PopulationNumber
			{
				get { return (mPopulationNumber); }
				set
				{
					mPopulationNumber = value;
					NotifyPropertyChanged(PropertyArgsPopulationNumber);
					NotifyPropertyChanged(PropertyArgsPopulationDensity);
				}
			}

			/// <summary>
			/// Плотность населения
			/// </summary>
			[DisplayName("Плотность")]
			[Description("Средняя плотность населения")]
			[Category("Население")]
			[XmlIgnore]
			public Double PopulationDensity
			{
				get
				{
					return (mPopulationNumber / mArea);
				}
			}

			/// <summary>
			/// Численность населения моложе трудоспособного возраста
			/// </summary>
			[DisplayName("Моложе труд/возраста")]
			[Description("Численность населения моложе трудоспособного возраста")]
			[Category("Население")]
			[XmlAttribute]
			public Int32 PopulationUnderWorking
			{
				get { return (mPopulationUnderWorking); }
				set
				{
					mPopulationUnderWorking = value;
					NotifyPropertyChanged(PropertyArgsPopulationUnderWorking);
				}
			}

			/// <summary>
			/// Численность населения трудоспособного возраста
			/// </summary>
			[DisplayName("Трудовой возраст")]
			[Description("Численность населения трудоспособного возраста")]
			[Category("Население")]
			[XmlAttribute]
			public Int32 PopulationWorking
			{
				get { return (mPopulationWorking); }
				set
				{
					mPopulationWorking = value;
					NotifyPropertyChanged(PropertyArgsPopulationWorking);
				}
			}

			/// <summary>
			/// Численность населения старше трудоспособного возраста
			/// </summary>
			[DisplayName("Старше труд/возраста")]
			[Description("Численность населения старше трудоспособного возраста")]
			[Category("Население")]
			[XmlAttribute]
			public Int32 PopulationOverWorking
			{
				get { return (mPopulationOverWorking); }
				set
				{
					mPopulationOverWorking = value;
					NotifyPropertyChanged(PropertyArgsPopulationOverWorking);
				}
			}

			/// <summary>
			/// Все объекты
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public ObservableCollection<CUrbanPlanningItem> Items
			{
				get
				{
					if(mItems == null)
					{
						mItems = new ObservableCollection<CUrbanPlanningItem>();
						//mItems.Add(mZones);
						mItems.Add(mSpecialInfrastructure);
						mItems.Add(mRoads);
						mItems.Add(mHousing);
						mItems.Add(mSocial);
					}
					return (mItems);
				}
			}

			/// <summary>
			/// Сельское поселение где находится населённый пункт
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CVillageSettlement Owner
			{
				get
				{
					return (mOwner);
				}
				set
				{
					mOwner = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CVillage()
				: base()
			{
				//mZones = new CLand(TLandCategory.LandsOfSettlements);
				mSpecialInfrastructure = new CSpecialInfrastructure();
				mRoads = new CRoadInfrastructure(TRoadPlaceType.Inside);
				mRoads.Name = "Внтрипослековые дороги";
				mHousing = new CHousingInfrastructure();
				mSocial = new CSocialInfrastructure();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CVillage(String name)
					: this()
			{
				mName = name;
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
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание и добавление созданного элемента в список дочерних элементов
			/// </summary>
			/// <remarks>
			/// Происходит создание элемента указанного типа и добавление его в список дочерних элементов
			/// </remarks>
			/// <returns>Структурный элемент документа</returns>
			//---------------------------------------------------------------------------------------------------------
			public override CUrbanPlanningItem AddChildNewElement()
			{
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из списка дочерних элементов
			/// </summary>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean RemoveChildElement(CUrbanPlanningItem element)
			{
				return (false);
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

			#region ======================================= МЕТОДЫ СЕРИАЛИЗАЦИИ =======================================
			//-------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей
			/// </summary>
			/// <param name="parent">Родительский объект</param>
			//-------------------------------------------------------------------------------------------------------------
			public override void OnUpdateLink(CUrbanPlanningItem parent)
			{
				//mZones.OnUpdateLink(this);
				mSpecialInfrastructure.OnUpdateLink(parent);
				mRoads.OnUpdateLink(this);
				mHousing.OnUpdateLink(this);
				mSocial.OnUpdateLink(this);
				mOwner = parent as CVillageSettlement;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конвертер типа <see cref="CVillage"/> для предоставления свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CVillageTypeConverter : TypeConverter
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение возможности использовать определенный набор свойств
			/// </summary>
			/// <param name="context">Контекст</param>
			/// <returns>True</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение нужной коллекции свойств
			/// </summary>
			/// <param name="context">Контекст</param>
			/// <param name="value">Объект</param>
			/// <param name="attributes">Атрибуты</param>
			/// <returns>Сформированная коллекция свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value,
				Attribute[] attributes)
			{
				List<PropertyDescriptor> result = new List<PropertyDescriptor>();
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, true);

				// 1) Общие данные
				result.Add(pdc["Name"]);
				result.Add(pdc["ID"]);
				//result.Add(pdc["Grouping"]);

				// 2) Основные параметры
				//result.Add(pdc["Length"]);

				return (new PropertyDescriptorCollection(result.ToArray(), true));
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Список населенных пунктов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CVillageInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal ObservableCollection<CVillage> mVillages;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Список населенных пунктов
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CVillage> Villages
			{
				get { return (mVillages); }
				set { mVillages = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CVillageInfrastructure()
			{
				mVillages = new ObservableCollection<CVillage>();
				mName = "Населенные пункты";
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
				return (mVillages);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание и добавление созданного элемента в список дочерних элементов
			/// </summary>
			/// <remarks>
			/// Происходит создание элемента указанного типа и добавление его в список дочерних элементов
			/// </remarks>
			/// <returns>Структурный элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public override CUrbanPlanningItem AddChildNewElement()
			{
				CVillage village = new CVillage("Село");
				mVillages.Add(village);
				return (village);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление существующего элемента
			/// </summary>
			/// <remarks>
			/// Происходит только добавление существующего элемента. Если элемент принадлежит другому элементу,
			/// то элемент будет сначала удален из его списка
			/// </remarks>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности добавления</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean AddChildExistingElement(CUrbanPlanningItem element)
			{
				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из списка дочерних элементов
			/// </summary>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean RemoveChildElement(CUrbanPlanningItem element)
			{
				CVillage village = element as CVillage;
				if (village != null)
				{
					mVillages.Remove(village);
					return (true);
				}
				return (false);
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

			#region ======================================= МЕТОДЫ СЕРИАЛИЗАЦИИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей
			/// </summary>
			/// <param name="parent">Родительский объект</param>
			//---------------------------------------------------------------------------------------------------------
			public override void OnUpdateLink(CUrbanPlanningItem parent)
			{
				for (Int32 i = 0; i < mVillages.Count; i++)
				{
					mVillages[i].OnUpdateLink(parent);
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