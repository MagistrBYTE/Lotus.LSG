//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль градостроительства
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGUrbanPlanningBase.cs
*		Базовые элементы градостроительного проектирования.
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
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityPlan Модуль градостроительства
		//! Базовый модуль определяет общие данные для общей цифровизации управления
		//! \ingroup Municipality
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityPlanBase Общая подсистема
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityPlan
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		public static class XVillageSettlement
		{
			public static readonly String[] Names = new String[]
			{
				"Андреевское СП",
				"Атамановское СП",
				"Наследницкое СП",
			};
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый элемент градостроительного проектирования
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[XmlInclude(typeof(CUrbanPlanningItemPoint))]
		public class CUrbanPlanningItem : ICloneable, INotifyPropertyChanged, INotifyPropertyChanging
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static PropertyChangedEventArgs PropertyArgsGroup = new PropertyChangedEventArgs(nameof(Group));
			protected static PropertyChangedEventArgs PropertyArgsCode = new PropertyChangedEventArgs(nameof(Code));
			protected static PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static PropertyChangedEventArgs PropertyArgsStatusUrban = new PropertyChangedEventArgs(nameof(StatusUrban));
			protected static PropertyChangedEventArgs PropertyArgsID = new PropertyChangedEventArgs(nameof(ID));
			protected static PropertyChangedEventArgs PropertyArgsIsConst = new PropertyChangedEventArgs(nameof(IsConst));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal String mName = "";
			internal String mGroup;
			internal String mCode;
			internal String mNumber;
			internal Int64 mID;
			internal Boolean mNotCalculation;
			internal TStatusUrban mStatusUrban;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ИДЕНТИФИКАЦИЯ
			//
			/// <summary>
			/// Наименование объекта
			/// </summary>
			[DisplayName("Наименование")]
			[Description("Наименование объекта")]
			[Category("Идентификация")]
			//[Display(Name= "Наименование", Order = 0, GroupName = "1. Идентификация")]
			[XmlAttribute]
			public String Name
			{
				get { return (mName); }
				set
				{
					mName = value;
					NotifyPropertyChanged(PropertyArgsName);
					RaiseNameChanged();
				}
			}

			/// <summary>
			/// Логическая группа которой принадлежит объект
			/// </summary>
			[DisplayName("Группа")]
			[Description("Логическая группа которой принадлежит объект")]
			[Category("Идентификация")]
			//[Display(Name = "Группа", Order = 1, GroupName = "1. Идентификация")]
			[XmlAttribute]
			public String Group
			{
				get { return (mGroup); }
				set
				{
					mGroup = value;
					NotifyPropertyChanged(PropertyArgsGroup);
					RaiseGroupChanged();
				}
			}

			/// <summary>
			/// Код объекта по классификатору
			/// </summary>
			[DisplayName("Код объекта")]
			[Description("Код объекта по классификатору")]
			[Category("Идентификация")]
			//[Display(Name = "Код объекта", Order = 2, GroupName = "1. Идентификация")]
			[XmlAttribute]
			public String Code
			{
				get { return (mCode); }
				set
				{
					mCode = value;
					NotifyPropertyChanged(PropertyArgsCode);
				}
			}

			/// <summary>
			/// Номер элемента
			/// </summary>
			[DisplayName("Номер")]
			[Description("Номер объекта")]
			[Category("Идентификация")]
			//[Display(Name = "Номер объекта", Order = 3, GroupName = "1. Идентификация")]
			[XmlAttribute]
			public String Number
			{
				get { return (mNumber); }
				set
				{
					mNumber = value;
					NotifyPropertyChanged(PropertyArgsNumber);
					RaiseNumberChanged();
				}
			}

			/// <summary>
			/// Не учитывать
			/// </summary>
			[DisplayName("Не учитывать")]
			[Description("Не учитывать объект в расчетах")]
			[Category("Идентификация")]
			//[Display(Name = "Не учитывать", Order = 4, GroupName = "1. Идентификация")]
			[XmlAttribute]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}

			/// <summary>
			/// Тип объекта градостроительства
			/// </summary>
			[DisplayName("Тип объекта")]
			[Description("Тип объекта градостроительства")]
			[Category("Идентификация")]
			//[Display(Name = "Тип объекта градостроительства", Order = 5, GroupName = "1. Идентификация")]
			[XmlAttribute]
			public TStatusUrban StatusUrban
			{
				get { return (mStatusUrban); }
				set
				{
					mStatusUrban = value;
					NotifyPropertyChanged(PropertyArgsStatusUrban);
				}
			}

			/// <summary>
			/// Уникальный идентификатор объекта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Int64 ID
			{
				get { return (mID); }
				set
				{
					if (value == -1)
					{
						mID = GenerateID();
					}
					else
					{
						mID = value;
					}
				}
			}

			/// <summary>
			/// Статус константного объекта
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public Boolean IsConst
			{
				get { return (mID < 1000); }
			}

			//
			// ПОДДЕРЖКА ИНСПЕКТОРА СВОЙСТВ
			//
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public virtual String InspectorTypeName
			{
				get { return ("ЭЛЕМЕНТ"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public virtual String InspectorObjectName
			{
				get
				{
					if (String.IsNullOrEmpty(mName))
					{
						return ("<Без имени>");
					}
					else
					{
						return (mName);
					}
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUrbanPlanningItem()
			{
				mID = GenerateID();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CUrbanPlanningItem(String name)
				:this()
			{
				mName = name;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return (MemberwiseClone());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CUrbanPlanningItem CloneItem()
			{
				return (MemberwiseClone() as CUrbanPlanningItem);
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Генерация уникального идентификатора
			/// </summary>
			/// <returns>Уникальный идентификатор</returns>
			//---------------------------------------------------------------------------------------------------------
			internal Int64 GenerateID()
			{
				return ((GetHashCode() << 32) + (mName.GetHashCode()));
			}
			#endregion

			#region ======================================= CЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение имени объекта.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseNameChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение логической группы объекта.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseGroupChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение номера элемента.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseNumberChanged()
			{
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ЭЛЕМЕНТАМИ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение источника данных связанного с этим объектом
			/// </summary>
			/// <returns>Источник данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Object GetItemSource()
			{
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание нового элемента
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента указанного типа.
			/// Сам элемент НЕ добавляется в список дочерних элементов
			/// </remarks>
			/// <param name="type_name">Имя типа элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUrbanPlanningItem CreateChildElement(String type_name)
			{
				return (CreateChildElement(type_name, "Новый элемент"));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание нового элемента
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента указанного типа.
			/// Сам элемент НЕ добавляется в список дочерних элементов
			/// </remarks>
			/// <param name="type_name">Имя типа элемента</param>
			/// <param name="element_name">Имя элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUrbanPlanningItem CreateChildElement(String type_name, String element_name)
			{
				// TODO Реализовать правильно
				CUrbanPlanningItem element = null;
				return (element);
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
			public virtual CUrbanPlanningItem AddChildNewElement()
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
			/// <param name="type_name">Имя типа элемента</param>
			/// <returns>Структурный элемент документа</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUrbanPlanningItem AddChildNewElement(String type_name)
			{
				return (AddChildNewElement(type_name, "Новый элемент"));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание и добавление созданного элемента в список дочерних элементов
			/// </summary>
			/// <remarks>
			/// Происходит создание элемента указанного типа и добавление его в список дочерних элементов
			/// </remarks>
			/// <param name="type_name">Имя типа элемента</param>
			/// <param name="element_name">Имя элемента</param>
			/// <returns>Статус успешности добавления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUrbanPlanningItem AddChildNewElement(String type_name, String element_name)
			{
				CUrbanPlanningItem element = null;
				return (element);
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
			public virtual Boolean AddChildExistingElement(CUrbanPlanningItem element)
			{
				if (element == null) return (false);
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск дочернего элемента по имени
			/// </summary>
			/// <param name="name">Имя элемента</param>
			/// <returns>Найденный элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUrbanPlanningItem GetChildElementFromName(String name)
			{
				CUrbanPlanningItem element = null;
				return (element);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск дочернего элемента по идентификатору
			/// </summary>
			/// <param name="id">Идентификатор элемента</param>
			/// <returns>Найденный элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUrbanPlanningItem GetChildElementFromId(Int64 id)
			{
				CUrbanPlanningItem element = null;

				return (element);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из списка дочерних элементов
			/// </summary>
			/// <param name="name">Имя элемента</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean RemoveChildElement(String name)
			{
				Boolean status = false;
				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из списка дочерних элементов
			/// </summary>
			/// <param name="id">Идентификатор элемента</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean RemoveChildElement(Int64 id)
			{
				Boolean status = false;
				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из списка дочерних элементов
			/// </summary>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean RemoveChildElement(CUrbanPlanningItem element)
			{
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка дочерних элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SortChildElements()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Группировка дочерних элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void GroupChildElements()
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
			public virtual void OnUpdateLink(CUrbanPlanningItem parent)
			{
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyPropertyChanging ============================
			/// <summary>
			/// Событие срабатывает ПЕРЕД изменением свойства
			/// </summary>
			public event PropertyChangingEventHandler PropertyChanging;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanging(String property_name = "")
			{
				if (PropertyChanging != null)
				{
					PropertyChanging(this, new PropertyChangingEventArgs(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanging(PropertyChangingEventArgs args)
			{
				if (PropertyChanging != null)
				{
					PropertyChanging(this, args);
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyPropertyChanged =============================
			/// <summary>
			/// Событие срабатывает ПОСЛЕ изменения свойства
			/// </summary>
			public event PropertyChangedEventHandler PropertyChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(String property_name = "")
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(PropertyChangedEventArgs args)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, args);
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент градостроительного проектирования который имеет территориальную сущность в виде отдельного объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CUrbanPlanningItemPoint : CUrbanPlanningItem, ILocation
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsLatitude = new PropertyChangedEventArgs(nameof(Latitude));
			protected static PropertyChangedEventArgs PropertyArgsLongitude = new PropertyChangedEventArgs(nameof(Longitude));
			protected static PropertyChangedEventArgs PropertyArgsAddress = new PropertyChangedEventArgs(nameof(Address));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal Double mLatitude;
			internal Double mLongitude;
			internal CAddress mAddress;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ГЕОГРАФИЧЕСКАЯ ПОЗИЦИЯ
			//
			/// <summary>
			/// Географическая широта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Double Latitude
			{
				get { return (mLatitude); }
				set
				{
					mLatitude = value;
					NotifyPropertyChanged(PropertyArgsLatitude);
				}
			}

			/// <summary>
			/// Географическая долгота
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Double Longitude
			{
				get { return (mLongitude); }
				set
				{
					mLongitude = value;
					NotifyPropertyChanged(PropertyArgsLongitude);
				}
			}

			//
			// АДРЕС ОБЪЕКТА
			//
			/// <summary>
			/// Адрес объекта
			/// </summary>
			[DisplayName("Адрес")]
			[Description("Адрес объекта")]
			[Category("Местоположение")]
			//[Display(Name = "Адрес", Order = 0, GroupName = "2. Местоположение")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorAddressTelerik), "Value")]
			public CAddress Address
			{
				get { return (mAddress); }
				set
				{
					mAddress = value;
					NotifyPropertyChanged(PropertyArgsLatitude);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUrbanPlanningItemPoint()
				: base()
			{
				mAddress = new CAddress();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CUrbanPlanningItemPoint(String name)
				: base(name)
			{
				mAddress = new CAddress();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================