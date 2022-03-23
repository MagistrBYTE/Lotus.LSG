//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль градостроительства
// Подраздел: Подсистема земельных отношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGUrbanPlanningLandSpecial.cs
*		Общие типы и структуры данных подсистемы земельных отношений.
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
		//! \addtogroup MunicipalityPlanLand
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип территорий специального назначения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TSpecialType>))]
		public enum TSpecialType
		{
			/// <summary>
			/// Кладбища
			/// </summary>
			[Description("Кладбища")]
			Cemetery,

			/// <summary>
			/// Свалки ТКО
			/// </summary>
			[Description("Свалки ТКО")]
			Landfill,

			/// <summary>
			/// Скотомогильник
			/// </summary>
			[Description("Скотомогильник")]
			CattleCemetery
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент территорий специального назначения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CSpecialElement : CUrbanPlanningItemPoint, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsSpecialType = new PropertyChangedEventArgs(nameof(SpecialType));
			protected static PropertyChangedEventArgs PropertyArgsStatus = new PropertyChangedEventArgs(nameof(Status));
			protected static PropertyChangedEventArgs PropertyArgsOwnership = new PropertyChangedEventArgs(nameof(Ownership));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Double mArea;
			internal TSpecialType mSpecialType;
			internal String mStatus;
			internal TOwnershipType mOwnership;
			internal CSpecial mSpecial;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Площадь элемента территории
			/// </summary>
			[DisplayName("Площадь, га")]
			[Description("Площадь элемента территории")]
			[Category("Основные параметры")]
			//[Display(Name = "Площадь, га", Order = 0, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public Double Area
			{
				get { return (mArea); }
				set
				{
					mArea = value;
					NotifyPropertyChanged(PropertyArgsArea);

					if (mSpecial != null)
					{
						mSpecial.NotifyPropertyChanged(PropertyArgsArea);
					}

				}
			}

			/// <summary>
			/// Тип территорий
			/// </summary>
			[DisplayName("Тип территорий")]
			[Description("Тип территорий специального назначения")]
			[Category("Основные параметры")]
			//[Display(Name = "Тип территорий", Order = 1, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public TSpecialType SpecialType
			{
				get { return (mSpecialType); }
				set
				{
					mSpecialType = value;
					NotifyPropertyChanged(PropertyArgsSpecialType);
				}
			}

			/// <summary>
			/// Статус территории специального назначения
			/// </summary>
			[DisplayName("Статус")]
			[Description("Статус территории специального назначения")]
			[Category("Основные параметры")]
			//[Display(Name = "Статус", Order = 2, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(System.Windows.Controls.TextBox), "Text")]
			public String Status
			{
				get { return (mStatus); }
				set
				{
					mStatus = value;
					NotifyPropertyChanged(PropertyArgsStatus);
				}
			}

			/// <summary>
			/// Принадлежность элемента территории по собственности
			/// </summary>
			[DisplayName("Собственник")]
			[Description("Принадлежность элемента территории по собственности")]
			[Category("Территория")]
			//[Display(Name = "Собственник", Order = 3, GroupName = "3. Основные параметры")]
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
			/// Принадлежность к типу специальных территорий
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CSpecial Special
			{
				get { return (mSpecial); }
				set
				{
					mSpecial = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSpecialElement()
			{
				mOwnership = TOwnershipType.Settlement;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CSpecialElement(String name)
					: base(name)
			{
				mOwnership = TOwnershipType.Settlement;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			/// <param name="special_type">Тип территорий специального назначения</param>
			//---------------------------------------------------------------------------------------------------------
			public CSpecialElement(String name, TSpecialType special_type)
					: base(name)
			{
				mSpecialType = special_type;
				mOwnership = TOwnershipType.Settlement;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Трансформация в объект перевода
			/// </summary>
			/// <returns>Объект перевода</returns>
			//---------------------------------------------------------------------------------------------------------
			public CTransfer ToTransfer()
			{
				CTransfer transfer = new CTransfer();
				transfer.Name = this.Name;
				transfer.Address = this.Address.CloneAddress();
				transfer.Area = this.Area;
				transfer.Code = this.Code;
				transfer.Group = this.Group;
				transfer.Latitude = this.Latitude;
				transfer.Longitude = this.Longitude;
				transfer.FromCategory = TLandCategory.AgriculturalLand;
				return (transfer);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Список территорий специального назначения одного типа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CSpecial : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsSpecialType = new PropertyChangedEventArgs(nameof(SpecialType));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal TSpecialType mSpecialType;
			internal ObservableCollection<CSpecialElement> mSpecialElements;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОБЩИЕ ДАННЫЕ
			//
			/// <summary>
			/// Площадь территории
			/// </summary>
			[DisplayName("Площадь, га")]
			[Description("Площадь территории")]
			[Category("Территория")]
			//[Display(Name = "Площадь, га", Order = 0, GroupName = "Территория")]
			[XmlAttribute]
			public Double Area
			{
				get
				{
					Double result = 0;
					for (Int32 i = 0; i < mSpecialElements.Count; i++)
					{
						result += mSpecialElements[i].Area;
					}

					return (result);
				}
			}

			/// <summary>
			/// Количество объектов
			/// </summary>
			[DisplayName("Кол-во")]
			[Description("Количество объектов")]
			[Category("Территория")]
			//[Display(Name = "Кол-во", Order = 1, GroupName = "Территория")]
			[XmlIgnore]
			public Int32 Count
			{
				get
				{
					return (mSpecialElements.Count);
				}
			}

			/// <summary>
			/// Тип территорий
			/// </summary>
			[DisplayName("Тип территорий")]
			[Description("Тип территорий специального назначения")]
			[Category("Территория")]
			//[Display(Name = "Тип территорий", Order = 2, GroupName = "Территория")]
			[XmlAttribute]
			public TSpecialType SpecialType
			{
				get { return (mSpecialType); }
				set { mSpecialType = value; }
			}

			/// <summary>
			/// Список всех элементов территории
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CSpecialElement> SpecialElements
			{
				get { return (mSpecialElements); }
				set { mSpecialElements = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSpecial()
			{
				mSpecialElements = new ObservableCollection<CSpecialElement>();
				mSpecialElements.CollectionChanged += OnElementsChanged;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="special_type">Тип территорий специального назначения</param>
			//---------------------------------------------------------------------------------------------------------
			public CSpecial(TSpecialType special_type)
					: this()
			{
				mSpecialType = special_type;
				mName = special_type.GetDescriptionOrName();
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
				for (Int32 i = 0; i < mSpecialElements.Count; i++)
				{
					mSpecialElements[i].Special = this;
				}
				NotifyPropertyChanged(PropertyArgsArea);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="special">Список территорий специального назначения одного типа</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CSpecial special)
			{
				for (Int32 i = 0; i < special.SpecialElements.Count; i++)
				{
					if (special.SpecialElements[i].NotCalculation) continue;

					SpecialElements.Add(special.SpecialElements[i]);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение текущего количества элементов
			/// </summary>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountCurrentElement()
			{
				Int32 result = 0;
				for (Int32 i = 0; i < mSpecialElements.Count; i++)
				{
					if (mSpecialElements[i].NotCalculation) continue;

					if (mSpecialElements[i].StatusUrban != TStatusUrban.Planned)
					{
						result++;
					}
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение планируемого количества элементов
			/// </summary>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountPlannedElement()
			{
				Int32 result = 0;
				for (Int32 i = 0; i < mSpecialElements.Count; i++)
				{
					if (mSpecialElements[i].NotCalculation) continue;

					if (mSpecialElements[i].StatusUrban == TStatusUrban.Abolished)
					{
						result--;
					}
					else
					{
						result++;
					}
				}
				return (result);
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
				return (mSpecialElements);
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
				CSpecialElement special_element = new CSpecialElement(mSpecialType.GetDescriptionOrName(), mSpecialType);
				special_element.Special = this;
				mSpecialElements.Add(special_element);
				return (special_element);
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
				CSpecialElement special_element = element as CSpecialElement;
				if (special_element != null)
				{
					special_element.Special = this;
					mSpecialElements.Add(special_element);
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
				CSpecialElement special_element = element as CSpecialElement;
				if (special_element != null)
				{
					mSpecialElements.Remove(special_element);
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
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Список территорий специального назначения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CSpecialInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal CSpecial mCemetery = new CSpecial(TSpecialType.Cemetery);
			internal CSpecial mLandfill = new CSpecial(TSpecialType.Landfill);
			internal CSpecial mCattleCemetery = new CSpecial(TSpecialType.CattleCemetery);
			internal ObservableCollection<CSpecial> mSpecials;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Кладбища
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CSpecial Cemetery
			{
				get { return (mCemetery); }
				set { mCemetery = value; }
			}

			/// <summary>
			/// Свалки
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CSpecial Landfill
			{
				get { return (mLandfill); }
				set { mLandfill = value; }
			}

			/// <summary>
			/// Скотомогильники
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CSpecial CattleCemetery
			{
				get { return (mCattleCemetery); }
				set { mCattleCemetery = value; }
			}

			/// <summary>
			/// Список всех территорий специального назначения
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public ObservableCollection<CSpecial> Specials
			{
				get
				{
					if (mSpecials == null)
					{
						mSpecials = new ObservableCollection<CSpecial>();
						mSpecials.Add(mCemetery);
						mSpecials.Add(mLandfill);
						mSpecials.Add(mCattleCemetery);
					}
					return (mSpecials);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSpecialInfrastructure()
			{
				mName = "Территории специального назначения";
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
				return (mSpecials);
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
			/// <param name="special_infrastructure">Список территорий специального назначения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CSpecialInfrastructure special_infrastructure)
			{
				Cemetery.Union(special_infrastructure.Cemetery);
				Landfill.Union(special_infrastructure.Landfill);
				CattleCemetery.Union(special_infrastructure.CattleCemetery);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение текущего количества элементов территорий специального назначения
			/// </summary>
			/// <param name="special_type">Тип территорий специального назначения</param>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountFromTypeCurrent(TSpecialType special_type)
			{
				Int32 result = 0;
				for (Int32 i = 0; i < Specials.Count; i++)
				{
					if (Specials[i].NotCalculation) continue;

					if (Specials[i].SpecialType == special_type && Specials[i].StatusUrban != TStatusUrban.Planned)
					{
						result++;
					}
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение планируемого количества элементов территорий специального назначения
			/// </summary>
			/// <param name="special_type">Тип территорий специального назначения</param>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountFromTypePlanned(TSpecialType special_type)
			{
				Int32 result = 0;
				for (Int32 i = 0; i < Specials.Count; i++)
				{
					if (Specials[i].NotCalculation) continue;

					if (Specials[i].SpecialType == special_type && Specials[i].StatusUrban == TStatusUrban.Abolished)
					{
						result--;
					}
					else
					{
						result++;
					}
				}
				return (result);
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
				mCemetery.OnUpdateLink(parent);
				mLandfill.OnUpdateLink(parent);
				mCattleCemetery.OnUpdateLink(parent);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================