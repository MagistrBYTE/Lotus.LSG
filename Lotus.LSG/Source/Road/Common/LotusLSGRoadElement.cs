//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Дорожное хозяйство
// Подраздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRoadElement.cs
*		Транспортная инфраструктура.
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
		//! \addtogroup MunicipalityRoadCommon
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент дорожной инфраструктуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[TypeConverter(typeof(CRoadElementConverter))]
		public class CRoadElement : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsLength = new PropertyChangedEventArgs(nameof(Length));
			protected static PropertyChangedEventArgs PropertyArgsWidth = new PropertyChangedEventArgs(nameof(Width));
			protected static PropertyChangedEventArgs PropertyArgsPlace = new PropertyChangedEventArgs(nameof(Place));
			protected static PropertyChangedEventArgs PropertyArgsCategory = new PropertyChangedEventArgs(nameof(Category));
			protected static PropertyChangedEventArgs PropertyArgsCoverage = new PropertyChangedEventArgs(nameof(Coverage));
			protected static PropertyChangedEventArgs PropertyArgsOwnership = new PropertyChangedEventArgs(nameof(Ownership));
			protected static PropertyChangedEventArgs PropertyArgsIsStatus = new PropertyChangedEventArgs(nameof(IsStatus));

			// Проектируемое положение
			protected static PropertyChangedEventArgs PropertyArgsCategoryProjected = new PropertyChangedEventArgs(nameof(CategoryProjected));
			protected static PropertyChangedEventArgs PropertyArgsCoverageProjected = new PropertyChangedEventArgs(nameof(CoverageProjected));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Double mLength;
			internal Double mWidth;
			internal TRoadPlaceType mPlace;
			internal TRoadCategoryType mCategory;
			internal TRoadCoverageType mCoverage;
			internal TOwnershipType mOwnership;
			internal Boolean mIsStatus;
			internal CRoadInfrastructure mRoadInfra;

			// Проектируемое положение
			internal TRoadCategoryType mCategoryProjected;
			internal TRoadCoverageType mCoverageProjected;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ИДЕНТИФИКАЦИЯ
			//
			/// <summary>
			/// Номер элемента
			/// </summary>
			[DisplayName("Оформлено")]
			[Description("Статус оформления")]
			[Category("Идентификация")]
			//[Display(Name = "Оформлено", Order = 5, GroupName = "1. Идентификация")]
			public Boolean IsStatus
			{
				get { return (mIsStatus); }
				set
				{
					mIsStatus = value;
					NotifyPropertyChanged(PropertyArgsIsStatus);
				}
			}

			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Длина автодороги, км
			/// </summary>
			[DisplayName("Длина")]
			[Description("Длина автодороги, км")]
			[Category("Дорога")]
			//[Display(Name = "Длина", Order = 0, GroupName = "2. Дорога")]
			[XmlAttribute]
			public Double Length
			{
				get { return (mLength); }
				set
				{
					mLength = value;
					NotifyPropertyChanged(PropertyArgsLength);
					if(mRoadInfra != null)
					{
						mRoadInfra.UpdateData();
					}
				}
			}

			/// <summary>
			/// Тип дорожного покрытия
			/// </summary>
			[DisplayName("Тип покрытия")]
			[Description("Тип дорожного покрытия")]
			[Category("Проектируемое положение")]
			//[Display(Name = "Тип покрытия", Order = 1, GroupName = "2. Дорога")]
			[XmlAttribute]
			public TRoadCoverageType Coverage
			{
				get { return (mCoverage); }
				set
				{
					mCoverage = value;
					NotifyPropertyChanged(PropertyArgsCoverage);
				}
			}

			/// <summary>
			/// Ширина автодороги, км
			/// </summary>
			[DisplayName("Ширина")]
			[Description("Ширина автодороги, м")]
			[Category("Дорога")]
			//[Display(Name = "Ширина", Order = 2, GroupName = "2. Дорога")]
			[XmlAttribute]
			public Double Width
			{
				get { return (mWidth); }
				set
				{
					mWidth = value;
					NotifyPropertyChanged(PropertyArgsWidth);
				}
			}

			/// <summary>
			/// Категория дороги
			/// </summary>
			[DisplayName("Категория дороги")]
			[Description("Категория дороги")]
			[Category("Дорога")]
			//[Display(Name = "Категория дороги", Order = 3, GroupName = "2. Дорога")]
			[XmlAttribute]
			public TRoadCategoryType Category
			{
				get { return (mCategory); }
				set
				{
					mCategory = value;
					NotifyPropertyChanged(PropertyArgsCategory);
				}
			}

			/// <summary>
			/// Местоположение дороги
			/// </summary>
			[DisplayName("Местоположение")]
			[Description("Местоположение дороги")]
			[Category("Дорога")]
			//[Display(Name = "Местоположение", Order = 4, GroupName = "2. Дорога")]
			[ReadOnly(true)]
			[XmlAttribute]
			public TRoadPlaceType Place
			{
				get { return (mPlace); }
				set
				{
					mPlace = value;
					NotifyPropertyChanged(PropertyArgsPlace);
				}
			}

			/// <summary>
			/// Собственник дороги
			/// </summary>
			[DisplayName("Собственник")]
			[Description("Собственник дороги")]
			[Category("Дорога")]
			//[Display(Name = "Собственник", Order = 5, GroupName = "2. Дорога")]
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

			//
			// ПРОЕКТИРУЕМОЕ ПОЛОЖЕНИЕ
			//
			/// <summary>
			/// Категория дороги
			/// </summary>
			[DisplayName("Категория дороги")]
			[Description("Категория дороги")]
			[Category("Дорога")]
			//[Display(Name = "Категория дороги", Order = 1, GroupName = "3. Проектируемое положение")]
			[XmlAttribute]
			public TRoadCategoryType CategoryProjected
			{
				get { return (mCategoryProjected); }
				set
				{
					mCategoryProjected = value;
					NotifyPropertyChanged(PropertyArgsCategoryProjected);
				}
			}

			/// <summary>
			/// Тип дорожного покрытия
			/// </summary>
			[DisplayName("Тип покрытия")]
			[Description("Тип дорожного покрытия")]
			[Category("Проектируемое положение")]
			//[Display(Name = "Тип покрытия", Order = 2, GroupName = "3. Проектируемое положение")]
			[XmlAttribute]
			public TRoadCoverageType CoverageProjected
			{
				get { return (mCoverageProjected); }
				set
				{
					mCoverageProjected = value;
					NotifyPropertyChanged(PropertyArgsCoverageProjected);
				}
			}

			/// <summary>
			/// Принадлежность к дорожной инфраструктуре определённого типа
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CRoadInfrastructure RoadInfra
			{
				get { return (mRoadInfra); }
				set
				{
					mRoadInfra = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadElement()
			{
				mWidth = 6;
				mCoverage = TRoadCoverageType.Asphalt;
				mCoverageProjected = TRoadCoverageType.Ground;

				mCategory = TRoadCategoryType.V;
				mCategoryProjected = TRoadCategoryType.V;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CRoadElement(String name)
					: base(name)
			{
				mWidth = 6;
				mCoverage = TRoadCoverageType.Asphalt;
				mCoverageProjected = TRoadCoverageType.Ground;

				mCategory = TRoadCategoryType.V;
				mCategoryProjected = TRoadCategoryType.V;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			/// <param name="place">Местоположение дороги</param>
			//---------------------------------------------------------------------------------------------------------
			public CRoadElement(String name, TRoadPlaceType place)
					: base(name)
			{
				mWidth = 6;
				mCoverage = TRoadCoverageType.Asphalt;
				mCoverageProjected = TRoadCoverageType.Ground;

				mCategory = TRoadCategoryType.V;
				mCategoryProjected = TRoadCategoryType.V;
				mPlace = place;
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
				mRoadInfra = parent as CRoadInfrastructure;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конвертер типа <see cref="CRoadElement"/> для предоставления свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadElementConverter : TypeConverter
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
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================