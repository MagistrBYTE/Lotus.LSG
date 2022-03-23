//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusClientTemplate
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUrbanPlanningSocial.cs
*		Социальная инфраструктура.
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
		//! \defgroup MunicipalityPlanSocial Подсистема социальной инфраструктуры
		//! \ingroup MunicipalityPlan
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип элемента социальной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TSocialType>))]
		public enum TSocialType
		{
			/// <summary>
			/// Административные учреждения
			/// </summary>
			[Description("Административные учреждения")]
			Administrative,

			/// <summary>
			/// Образовательные учреждения
			/// </summary>
			[Description("Образовательные учреждения")]
			Educational,

			/// <summary>
			/// Учреждения здравоохранения
			/// </summary>
			[Description("Учреждения здравоохранения")]
			Health,

			/// <summary>
			/// Культурно-досуговые учреждения
			/// </summary>
			[Description("Культурно-досуговые учреждения")]
			Cultural,

			/// <summary>
			/// Спортивные учреждения и объекты
			/// </summary>
			[Description("Спортивные учреждения и объекты")]
			Sport,

			/// <summary>
			/// Учреждения социальной сферы
			/// </summary>
			[Description("Учреждения социальной сферы")]
			Social
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент социальной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[TypeConverter(typeof(CSocialElementConverter))]
		public class CSocialElement : CUrbanPlanningItemPoint, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsSocialType = new PropertyChangedEventArgs(nameof(SocialType));
			protected static PropertyChangedEventArgs PropertyArgsNumberEmployees = new PropertyChangedEventArgs(nameof(NumberEmployees));
			protected static PropertyChangedEventArgs PropertyArgsNumberPersons = new PropertyChangedEventArgs(nameof(NumberPersons));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal TSocialType mSocialType;
			internal Double mArea;
			internal Int32 mNumberEmployees;
			internal Int32 mNumberPersons;
			internal CSocialInfrastructure mSocialInfra;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип элемента социальной инфраструктуры
			/// </summary>
			[DisplayName("Тип")]
			[Description("Тип элемента социальной инфраструктуры")]
			[Category("Основные параметры")]
			//[Display(Name = "Тип", Order = 0, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public TSocialType SocialType
			{
				get { return (mSocialType); }
				set
				{
					mSocialType = value;
					NotifyPropertyChanged(PropertyArgsName);
				}
			}

			/// <summary>
			/// Площадь объекта
			/// </summary>
			[DisplayName("Площадь")]
			[Description("Площадь объекта, кв.м")]
			[Category("Основные параметры")]
			//[Display(Name = "Площадь", Order = 1, GroupName = "3. Основные параметры")]
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
			/// Количество работающих
			/// </summary>
			[DisplayName("Кол-во работающих")]
			[Description("Количество работающих")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во работающих", Order = 2, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public Int32 NumberEmployees
			{
				get { return (mNumberEmployees); }
				set
				{
					mNumberEmployees = value;
					NotifyPropertyChanged(PropertyArgsNumberEmployees);
				}
			}

			/// <summary>
			/// Количество обслуживаемых лиц
			/// </summary>
			[DisplayName("Кол-во обслуживаемых")]
			[Description("Количество обслуживаемых лиц")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во обслуживаемых", Order = 3, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public Int32 NumberPersons
			{
				get { return (mNumberPersons); }
				set
				{
					mNumberPersons = value;
					NotifyPropertyChanged(PropertyArgsNumberPersons);
				}
			}

			/// <summary>
			/// Принадлежность к классу социальной инфраструктуры
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CSocialInfrastructure SocialInfra
			{
				get { return (mSocialInfra); }
				set
				{
					mSocialInfra = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSocialElement()
			{
				mName = "Элемент";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CSocialElement(String name)
					: base(name)
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
				mSocialInfra = parent as CSocialInfrastructure;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конвертер типа <see cref="CSocialElement"/> для предоставления свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSocialElementConverter : TypeConverter
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

				return (new PropertyDescriptorCollection(result.ToArray(), true));
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой информацию о социальной инфраструктуре
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CSocialInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal ObservableCollection<CSocialElement> mSocialElements;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Список всех элементов социальной инфраструктуры
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CSocialElement> SocialElements
			{
				get { return (mSocialElements); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSocialInfrastructure()
			{
				mName = "Социальная инфраструктура";
				mSocialElements = new ObservableCollection<CSocialElement>();
				mSocialElements.CollectionChanged += OnElementsChanged;
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
				for (Int32 i = 0; i < mSocialElements.Count; i++)
				{
					mSocialElements[i].SocialInfra = this;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="social_infrastructure">Социальная инфраструктура</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CSocialInfrastructure social_infrastructure)
			{
				for (Int32 i = 0; i < social_infrastructure.SocialElements.Count; i++)
				{
					SocialElements.Add(social_infrastructure.SocialElements[i]);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение текущего количества элементов социальной структуры
			/// </summary>
			/// <param name="social_type">Тип элемента социальной инфраструктуры</param>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountFromTypeCurrent(TSocialType social_type)
			{
				Int32 result = 0;
				for (Int32 i = 0; i < mSocialElements.Count; i++)
				{
					if (mSocialElements[i].NotCalculation) continue;

					if (mSocialElements[i].SocialType == social_type && mSocialElements[i].StatusUrban != TStatusUrban.Planned)
					{
						result++;
					}
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение планируемого количества элементов социальной структуры
			/// </summary>
			/// <param name="social_type">Тип элемента социальной инфраструктуры</param>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountFromTypePlanned(TSocialType social_type)
			{
				Int32 result = 0;
				for (Int32 i = 0; i < mSocialElements.Count; i++)
				{
					if (mSocialElements[i].NotCalculation) continue;

					if (mSocialElements[i].SocialType == social_type && mSocialElements[i].StatusUrban == TStatusUrban.Abolished)
					{
						result++;
					}
					if (mSocialElements[i].SocialType == social_type && mSocialElements[i].StatusUrban != TStatusUrban.Abolished)
					{
						result--;
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
				return (mSocialElements);
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
				CSocialElement social_element = new CSocialElement("Элемент");
				social_element.SocialInfra = this;
				mSocialElements.Add(social_element);
				return (social_element);
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
				CSocialElement social_element = element as CSocialElement;
				if (social_element != null)
				{
					social_element.SocialInfra = this;
					mSocialElements.Add(social_element);
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
				CSocialElement social_element = element as CSocialElement;
				if (social_element != null)
				{
					mSocialElements.Remove(social_element);
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
			//-------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей
			/// </summary>
			/// <param name="parent">Родительский объект</param>
			//-------------------------------------------------------------------------------------------------------------
			public override void OnUpdateLink(CUrbanPlanningItem parent)
			{
				for (Int32 i = 0; i < mSocialElements.Count; i++)
				{
					mSocialElements[i].OnUpdateLink(this);
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