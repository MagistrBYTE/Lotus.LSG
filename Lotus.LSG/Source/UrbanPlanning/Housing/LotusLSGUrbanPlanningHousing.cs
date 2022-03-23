//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusClientTemplate
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGUrbanPlanningHousing.cs
*		Жилищная инфраструктура.
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
		//! \defgroup MunicipalityPlanHousing Подсистема жилищной инфраструктуры
		//! \ingroup MunicipalityPlan
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип элемента жилищной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<THouseType>))]
		public enum THouseType
		{
			/// <summary>
			/// Индивидуальный жилой дом
			/// </summary>
			[Description("Индивидуальный дом")]
			Individual,

			/// <summary>
			/// Жилой дом блокированной застройки
			/// </summary>
			[Description("Блокированный дом")]
			Locked,

			/// <summary>
			/// Многоквартирный жилой дом
			/// </summary>
			[Description("Многоквартирный дом")]
			Tenement
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент жилищной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CHouseElement : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsCount = new PropertyChangedEventArgs(nameof(Count));
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsHouseType = new PropertyChangedEventArgs(nameof(HouseType));
			protected static PropertyChangedEventArgs PropertyArgsMaterial = new PropertyChangedEventArgs(nameof(Material));
			protected static PropertyChangedEventArgs PropertyArgsWearPercent = new PropertyChangedEventArgs(nameof(WearPercent));
			protected static PropertyChangedEventArgs PropertyArgsOwnership = new PropertyChangedEventArgs(nameof(Ownership));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Int32 mCount;
			internal Double mArea;
			internal THouseType mHouseType;
			internal String mMaterial;
			internal Int32 mWearPercent;
			internal TOwnershipType mOwnership;
			internal CHousingInfrastructure mHousingInfra;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Количество объектов
			/// </summary>
			[DisplayName("Кол-во")]
			[Description("Количество объектов")]
			[Category("Жилищный фонд")]
			//[Display(Name = "Кол-во", Order = 0, GroupName = "2. Жилищный фонд")]
			[XmlAttribute]
			public Int32 Count
			{
				get { return (mCount); }
				set
				{
					if (value < 1) value = 1;
					if (value > 10000) value = 10000;
					mCount = value;
					NotifyPropertyChanged(PropertyArgsCount);
				}
			}

			/// <summary>
			/// Общая площадь объектов
			/// </summary>
			[DisplayName("Площадь")]
			[Description("Общая площадь объектов, кв.м")]
			[Category("Жилищный фонд")]
			//[Display(Name = "Площадь", Order = 1, GroupName = "2. Жилищный фонд")]
			[XmlAttribute]
			public Double Area
			{
				get { return (mArea); }
				set
				{
					mArea = value;
					NotifyPropertyChanged(PropertyArgsArea);
				}
			}

			/// <summary>
			/// Тип дома
			/// </summary>
			[DisplayName("Тип дома")]
			[Description("Тип дома")]
			[Category("Жилищный фонд")]
			//[Display(Name = "Тип дома", Order = 2, GroupName = "2. Жилищный фонд")]
			[XmlAttribute]
			public THouseType HouseType
			{
				get { return (mHouseType); }
				set
				{
					mHouseType = value;
					mName = mHouseType.GetDescriptionOrName();
					NotifyPropertyChanged(PropertyArgsHouseType);
					NotifyPropertyChanged(PropertyArgsName);
				}
			}

			/// <summary>
			/// Основной материал
			/// </summary>
			[DisplayName("Основной материал")]
			[Description("Основной материал дома")]
			[Category("Жилищный фонд")]
			//[Display(Name = "Основной материал", Order = 3, GroupName = "2. Жилищный фонд")]
			[XmlAttribute]
			public String Material
			{
				get { return (mMaterial); }
				set
				{
					mMaterial = value;
					NotifyPropertyChanged(PropertyArgsMaterial);
				}
			}

			/// <summary>
			/// Процент износа
			/// </summary>
			[DisplayName("Процент износа")]
			[Description("Процент износа, %")]
			[Category("Жилищный фонд")]
			//[Display(Name = "Процент износа", Order = 4, GroupName = "2. Жилищный фонд")]
			[XmlAttribute]
			public Int32 WearPercent
			{
				get { return (mWearPercent); }
				set
				{
					mWearPercent = value;
					NotifyPropertyChanged(PropertyArgsWearPercent);
				}
			}

			/// <summary>
			/// Право собственности
			/// </summary>
			[DisplayName("Собственность")]
			[Description("Право собственности на все объекты")]
			[Category("Жилищный фонд")]
			//[Display(Name = "Собственность", Order = 5, GroupName = "2. Жилищный фонд")]
			[XmlAttribute]
			public TOwnershipType Ownership
			{
				get { return (mOwnership); }
				set
				{
					mOwnership = value;
					NotifyPropertyChanged(PropertyArgsOwnership);
				}
			}

			/// <summary>
			/// Принадлежность к классу жилищной инфраструктуры
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CHousingInfrastructure HousingInfra
			{
				get { return (mHousingInfra); }
				set
				{
					mHousingInfra = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CHouseElement()
			{
				mCount = 1;
				mName = mHouseType.GetDescriptionOrName();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CHouseElement(String name)
					: base(name)
			{
				mCount = 1;
				mName = mHouseType.GetDescriptionOrName();
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
				mHousingInfra = parent as CHousingInfrastructure;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой информацию о жилищной инфраструктуре
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CHousingInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsIndividualCount = new PropertyChangedEventArgs(nameof(IndividualCount));
			protected static PropertyChangedEventArgs PropertyArgsIndividualArea = new PropertyChangedEventArgs(nameof(IndividualArea));
			protected static PropertyChangedEventArgs PropertyArgsLockedCount = new PropertyChangedEventArgs(nameof(LockedCount));
			protected static PropertyChangedEventArgs PropertyArgsLockedArea = new PropertyChangedEventArgs(nameof(LockedArea));
			protected static PropertyChangedEventArgs PropertyArgsTenementCount = new PropertyChangedEventArgs(nameof(TenementCount));
			protected static PropertyChangedEventArgs PropertyArgsTenementArea = new PropertyChangedEventArgs(nameof(TenementArea));

			// Обеспеченость
			protected static PropertyChangedEventArgs PropertyArgsProvidingWater = new PropertyChangedEventArgs(nameof(ProvidingWater));
			protected static PropertyChangedEventArgs PropertyArgsProvidingSewer = new PropertyChangedEventArgs(nameof(ProvidingSewer));
			protected static PropertyChangedEventArgs PropertyArgsProvidingGas = new PropertyChangedEventArgs(nameof(ProvidingGas));
			protected static PropertyChangedEventArgs PropertyArgsProvidingWarm = new PropertyChangedEventArgs(nameof(ProvidingWarm));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal ObservableCollection<CHouseElement> mHouseElements;

			// Обеспеченость
			internal TValueReal mProvidingWater;
			internal TValueReal mProvidingSewer;
			internal TValueReal mProvidingGas;
			internal TValueReal mProvidingWarm;

			// Служебные данные
			private Int32 mCountUnion = 0;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Общая площадь жилого фонда
			/// </summary>
			[DisplayName("Общая площадь")]
			[Description("Общая площадь жилого фонда")]
			[Category("Основные параметры")]
			//[Display(Name = "Общая площадь", Order = 0, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Double TotalArea
			{
				get
				{
					Double result = 0;
					for (Int32 i = 0; i < mHouseElements.Count; i++)
					{
						if (mHouseElements[i].NotCalculation) continue;

						if (mHouseElements[i].StatusUrban != TStatusUrban.Planned)
						{
							result += mHouseElements[i].Area;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Общая площадь планируемого жилого фонда
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public Double TotalAreaPlanned
			{
				get
				{
					Double result = 0;
					for (Int32 i = 0; i < mHouseElements.Count; i++)
					{
						if (mHouseElements[i].NotCalculation) continue;

						if (mHouseElements[i].StatusUrban == TStatusUrban.Abolished)
						{
							result -= mHouseElements[i].Area;
						}
						else
						{
							result += mHouseElements[i].Area;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Количество индивидуальных жилых домов
			/// </summary>
			[DisplayName("Количество ИЖС")]
			[Description("Количество индивидуальных жилых домов")]
			[Category("Основные параметры")]
			//[Display(Name = "Количество ИЖС", Order = 1, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Int32 IndividualCount
			{
				get
				{
					return (GetTotalCountFromHouseTypeCurrent(THouseType.Individual));
				}
			}

			/// <summary>
			/// Общая площадь индивидуальных жилых домов
			/// </summary>
			[DisplayName("Общая площадь ИЖС")]
			[Description("Общая площадь индивидуальных жилых домов")]
			[Category("Основные параметры")]
			//[Display(Name = "Общая площадь ИЖС", Order = 2, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Double IndividualArea
			{
				get
				{
					return (GetTotalAreaFromHouseTypeCurrent(THouseType.Individual));
				}
			}

			/// <summary>
			/// Количество жилых домов блокированной застройки
			/// </summary>
			[DisplayName("Количество блокированных")]
			[Description("Количество жилых домов блокированной застройки")]
			[Category("Основные параметры")]
			//[Display(Name = "Количество блокированных", Order = 3, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Int32 LockedCount
			{
				get
				{
					return (GetTotalCountFromHouseTypeCurrent(THouseType.Locked));
				}
			}

			/// <summary>
			/// Общая площадь жилых домов блокированной застройки
			/// </summary>
			[DisplayName("Общая площадь блокированных")]
			[Description("Общая площадь жилых домов блокированной застройки")]
			[Category("Основные параметры")]
			//[Display(Name = "Общая площадь блокированных", Order = 4, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Double LockedArea
			{
				get
				{
					return (GetTotalAreaFromHouseTypeCurrent(THouseType.Locked));
				}
			}

			/// <summary>
			/// Количество многоквартирных жилых домов
			/// </summary>
			[DisplayName("Количество МКД")]
			[Description("Количество многоквартирных жилых домов")]
			[Category("Основные параметры")]
			//[Display(Name = "Количество МКД", Order = 5, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Int32 TenementCount
			{
				get
				{
					return (GetTotalCountFromHouseTypeCurrent(THouseType.Tenement));
				}
			}

			/// <summary>
			/// Общая площадь многоквартирных жилых домов
			/// </summary>
			[DisplayName("Общая площадь МКД")]
			[Description("Общая площадь многоквартирных жилых домов")]
			[Category("Основные параметры")]
			//[Display(Name = "Общая площадь МКД", Order = 6, GroupName = "2. Жилищный фонд")]
			[XmlIgnore]
			public Double TenementArea
			{
				get
				{
					return (GetTotalAreaFromHouseTypeCurrent(THouseType.Tenement));
				}
			}

			/// <summary>
			/// Список всех элементов жилищной инфраструктуры по принадлежности
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CHouseElement> HouseElements
			{
				get { return (mHouseElements); }
			}

			//
			// ОБЕСПЕЧЕНОСТЬ
			//
			/// <summary>
			/// Обеспеченность жилищного фонда водой
			/// </summary>
			[DisplayName("Обеспеченность водой")]
			[Description("Обеспеченность жилищного фонда водой, %")]
			[Category("Обеспеченность инфраструктурой")]
			//[Display(Name = "Обеспеченность водой", Order = 0, GroupName = "3. Обеспеченность инфраструктурой")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal ProvidingWater
			{
				get { return (mProvidingWater); }
				set
				{
					mProvidingWater = value;
					NotifyPropertyChanged(PropertyArgsProvidingWater);
				}
			}

			/// <summary>
			/// Обеспеченность жилищного фонда канализацией
			/// </summary>
			[DisplayName("Обеспеченность канализацией")]
			[Description("Обеспеченность жилищного фонда канализацией, %")]
			[Category("Обеспеченность инфраструктурой")]
			//[Display(Name = "Обеспеченность канализацией", Order = 1, GroupName = "3. Обеспеченность инфраструктурой")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal ProvidingSewer
			{
				get { return (mProvidingSewer); }
				set
				{
					mProvidingSewer= value;
					NotifyPropertyChanged(PropertyArgsProvidingSewer);
				}
			}

			/// <summary>
			/// Обеспеченность жилищного фонда газом
			/// </summary>
			[DisplayName("Обеспеченность газом")]
			[Description("Обеспеченность жилищного фонда газом, %")]
			[Category("Обеспеченность инфраструктурой")]
			//[Display(Name = "Обеспеченность газом", Order = 2, GroupName = "3. Обеспеченность инфраструктурой")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal ProvidingGas
			{
				get { return (mProvidingGas); }
				set
				{
					mProvidingGas = value;
					NotifyPropertyChanged(PropertyArgsProvidingGas);
				}
			}

			/// <summary>
			/// Обеспеченность жилищного фонда теплом
			/// </summary>
			[DisplayName("Обеспеченность теплом")]
			[Description("Обеспеченность жилищного фонда теплом, %")]
			[Category("Обеспеченность инфраструктурой")]
			//[Display(Name = "Обеспеченность теплом", Order = 3, GroupName = "3. Обеспеченность инфраструктурой")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal ProvidingWarm
			{
				get { return (mProvidingWarm); }
				set
				{
					mProvidingWarm = value;
					NotifyPropertyChanged(PropertyArgsProvidingWarm);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CHousingInfrastructure()
			{
				mHouseElements = new ObservableCollection<CHouseElement>();
				mHouseElements.CollectionChanged += OnElementsChanged;
				mName = "Жилищная инфраструктура";
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение элементов
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private void OnElementsChanged(Object sender, NotifyCollectionChangedEventArgs args)
			{
				for (Int32 i = 0; i < mHouseElements.Count; i++)
				{
					mHouseElements[i].HousingInfra = this;
				}
				NotifyPropertyChanged(PropertyArgsIndividualCount);
				NotifyPropertyChanged(PropertyArgsIndividualArea);
				NotifyPropertyChanged(PropertyArgsLockedCount);
				NotifyPropertyChanged(PropertyArgsLockedArea);
				NotifyPropertyChanged(PropertyArgsTenementCount);
				NotifyPropertyChanged(PropertyArgsTenementArea);
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
				return (mHouseElements);
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
				CHouseElement house_element = new CHouseElement("Дом");
				house_element.HousingInfra = this;
				mHouseElements.Add(house_element);
				return (house_element);
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
				CHouseElement house_element = element as CHouseElement;
				if (house_element != null)
				{
					house_element.HousingInfra = this;
					mHouseElements.Add(house_element);
					return (true);
				}
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
				CHouseElement house_element = element as CHouseElement;
				if (house_element != null)
				{
					mHouseElements.Remove(house_element);
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

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="housing_infrastructure">Жилищная инфраструктура</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CHousingInfrastructure housing_infrastructure)
			{
				mCountUnion++;

				for (Int32 i = 0; i < housing_infrastructure.HouseElements.Count; i++)
				{
					mHouseElements.Add(housing_infrastructure.HouseElements[i]);
				}

				mProvidingWater += housing_infrastructure.ProvidingWater;
				mProvidingSewer += housing_infrastructure.ProvidingSewer;
				mProvidingGas += housing_infrastructure.ProvidingGas;
				mProvidingWarm += housing_infrastructure.ProvidingWarm;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление среднего процента обеспеченности
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ComputePercentProviding()
			{
				mProvidingWater /= mCountUnion;
				mProvidingSewer /= mCountUnion;
				mProvidingGas /= mCountUnion;
				mProvidingWarm /= mCountUnion;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			///Вычисление общего количества домов указанного типа
			/// </summary>
			/// <param name="house_type">Тип дома</param>
			/// <returns>Количество домов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetTotalCountFromHouseTypeCurrent(THouseType house_type)
			{
				Int32 result = 0;
				for (Int32 i = 0; i < mHouseElements.Count; i++)
				{
					if (mHouseElements[i].NotCalculation) continue;

					if (mHouseElements[i].HouseType == house_type && mHouseElements[i].StatusUrban != TStatusUrban.Planned)
					{
						result += mHouseElements[i].Count;
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			///Вычисление общего количества планируемых домов указанного типа
			/// </summary>
			/// <param name="house_type">Тип дома</param>
			/// <returns>Количество домов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetTotalCountFromHouseTypePlanned(THouseType house_type)
			{
				Int32 result = 0;
				for (Int32 i = 0; i < mHouseElements.Count; i++)
				{
					if (mHouseElements[i].NotCalculation) continue;

					if (mHouseElements[i].HouseType == house_type && mHouseElements[i].StatusUrban != TStatusUrban.Abolished)
					{
						result += mHouseElements[i].Count;
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			///Вычисление общей площади домов указанного типа
			/// </summary>
			/// <param name="house_type">Тип дома</param>
			/// <returns>Общая площадь домов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetTotalAreaFromHouseTypeCurrent(THouseType house_type)
			{
				Double result = 0;
				for (Int32 i = 0; i < mHouseElements.Count; i++)
				{
					if (mHouseElements[i].NotCalculation) continue;

					if (mHouseElements[i].HouseType == house_type && mHouseElements[i].StatusUrban != TStatusUrban.Planned)
					{
						result += mHouseElements[i].Area;
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			///Вычисление общей площади планируемых домов указанного типа
			/// </summary>
			/// <param name="house_type">Тип дома</param>
			/// <returns>Общая площадь домов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetTotalAreaFromHouseTypePlanned(THouseType house_type)
			{
				Double result = 0;
				for (Int32 i = 0; i < mHouseElements.Count; i++)
				{
					if (mHouseElements[i].NotCalculation) continue;

					if (mHouseElements[i].HouseType == house_type && mHouseElements[i].StatusUrban != TStatusUrban.Abolished)
					{
						result += mHouseElements[i].Area;
					}
				}

				return (result);
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
				for (Int32 i = 0; i < mHouseElements.Count; i++)
				{
					mHouseElements[i].OnUpdateLink(this);
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