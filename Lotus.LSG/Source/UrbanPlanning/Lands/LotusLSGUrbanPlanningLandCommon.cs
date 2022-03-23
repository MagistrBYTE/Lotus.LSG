//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль градостроительства
// Подраздел: Подсистема земельных отношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGUrbanPlanningLandCommon.cs
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
		//! \defgroup MunicipalityPlanLand Подсистема земельных отношений
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityPlan
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Категории земли
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TLandCategory>))]
		public enum TLandCategory
		{
			/// <summary>
			/// Земли сельскохозяйственного назначения
			/// </summary>
			[Description("Земли с/х назначения")]
			AgriculturalLand,

			/// <summary>
			/// Земли населённых пунктов
			/// </summary>
			[Description("Земли населённых пунктов")]
			LandsOfSettlements,

			/// <summary>
			/// Земли промышленности
			/// </summary>
			[Description("Земли промышленности")]
			IndustrialLand,

			/// <summary>
			/// Земли особо охраняемых территорий и объектов
			/// </summary>
			[Description("Земли ООТО")]
			LandsProtected,

			/// <summary>
			/// Земли лесного фонда
			/// </summary>
			[Description("Земли лесного фонда")]
			LandsForest,

			/// <summary>
			/// Земли водного фонда
			/// </summary>
			[Description("Земли водного фонда")]
			LandsWater
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Все земли по категориям
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CLandInfrastructure : ListArray<CLandCategory>
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Группы сельскохозяйственных земель
			/// </summary>
			public static readonly String[] GroupAgricultural = new String[]
			{
				"Прочие",
				"Пашни",
				"Пастбище",
				"Сенокосы",
				"Сельхозпредприятия",
			};

			/// <summary>
			/// Группы земель населенных пунктов
			/// </summary>
			public static readonly String[] GroupSettlements = new String[]
			{
				"Жилые зоны",
				"Зоны промышленности",
			};

			/// <summary>
			/// Группы промышленных земель
			/// </summary>
			public static readonly String[] GroupIndustrial = new String[]
			{
				"Земли траспорта",
				"Земли под объектами энергетики",
				"Земли под объектами газоснабжения",
				"Земли специальных территорий",
				"Участки полезных ископаемых",
				"Зоны промышленности",
			};

			/// <summary>
			/// Группы земель особо-охраняемых территорий
			/// </summary>
			public static readonly String[] GroupProtected = new String[]
			{
				"Земли ООПТ",
				"Природоохранного назначения",
				"Рекреационного назначения",
				"Историко-культурного назначения",
			};

			/// <summary>
			/// Группы земли лесного фонда
			/// </summary>
			public static readonly String[] GroupForest = new String[]
			{
				"Земли покрытые лесами",
				"Вспомогательные земли",
			};

			/// <summary>
			/// Группы земли водного фонда
			/// </summary>
			public static readonly String[] GroupWater = new String[]
			{
				"Земли покрытые водами",
				"Зоны гидротехнических сооружений",
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal CLandCategory mAgriculturalLand = new CLandCategory(TLandCategory.AgriculturalLand, GroupAgricultural);
			protected internal CLandCategory mSettlementsLands = new CLandCategory(TLandCategory.LandsOfSettlements, GroupSettlements);
			protected internal CLandCategory mIndustrialLand = new CLandCategory(TLandCategory.IndustrialLand, GroupIndustrial);
			protected internal CLandCategory mLandsProtected = new CLandCategory(TLandCategory.LandsProtected, GroupProtected);
			protected internal CLandCategory mLandsForest = new CLandCategory(TLandCategory.LandsForest, GroupForest);
			protected internal CLandCategory mLandsWater = new CLandCategory(TLandCategory.LandsWater, GroupWater);
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Земли сельскохозяйственного назначения
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandCategory AgriculturalLand
			{
				get { return (mAgriculturalLand); }
				set { mAgriculturalLand = value; }
			}

			/// <summary>
			/// Земли населенных пунктов
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandCategory SettlementsLands
			{
				get { return (mSettlementsLands); }
				set { mSettlementsLands = value; }
			}

			/// <summary>
			/// Земли промышленности
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandCategory IndustrialLand
			{
				get { return (mIndustrialLand); }
				set { mIndustrialLand = value; }
			}

			/// <summary>
			/// Земли особо охраняемых территорий и объектов
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandCategory LandsProtected
			{
				get { return (mLandsProtected); }
				set { mLandsProtected = value; }
			}

			/// <summary>
			/// Земли лесного фонда
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandCategory LandsForest
			{
				get { return (mLandsForest); }
				set { mLandsForest = value; }
			}

			/// <summary>
			/// Земли водного фонда
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandCategory LandsWater
			{
				get { return (mLandsWater); }
				set { mLandsWater = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CLandInfrastructure()
			{
				//mName = "Земли по категориям";
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание списка элементов.
			/// Метод автоматически вызывается если при доступе к списку элементу он оказывается не инициализированным
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			//protected override void RaiseCreatedItems()
			//{
			//	//mItems.Add(mAgriculturalLand);
			//	//mItems.Add(mSettlementsLands);
			//	//mItems.Add(mIndustrialLand);
			//	//mItems.Add(mLandsProtected);
			//	//mItems.Add(mLandsForest);
			//	//mItems.Add(mLandsWater);
			//}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий категорию земли
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CLandCategory : ListArray<CLandPermittedUse>, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsPercent = new PropertyChangedEventArgs(nameof(Percent));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal TLandCategory mCategory;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ПАРАМЕТРЫ ТЕРРИТОРИИ
			//
			/// <summary>
			/// Категория земли
			/// </summary>
			[DisplayName("Категория земли")]
			[Description("Категория земли")]
			[Category("Территория")]
			[XmlAttribute]
			public TLandCategory Category
			{
				get { return (mCategory); }
			}

			/// <summary>
			/// Общая площадь территории
			/// </summary>
			[DisplayName("Площадь, га")]
			[Description("Общая площадь территории")]
			[Category("Территория")]
			[XmlIgnore]
			public Double Area
			{
				get
				{
					return (GetTotalArea());
				}
			}

			/// <summary>
			/// Общая площадь территории в процентах
			/// </summary>
			[DisplayName("Процент от общей")]
			[Description("Общая площадь территории")]
			[Category("Территория")]
			[XmlIgnore]
			public Double Percent
			{
				get
				{
					return (0.0);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CLandCategory()
			{
				//mItems = new ObservableCollection<CLandPermittedUse>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="category">Категория земли</param>
			//---------------------------------------------------------------------------------------------------------
			public CLandCategory(TLandCategory category)
					: this()
			{
				mCategory = category;
				//mName = category.GetDescriptionOrName();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="category">Категория земли</param>
			/// <param name="groups">Группы земель в составе категории земель</param>
			//---------------------------------------------------------------------------------------------------------
			public CLandCategory(TLandCategory category, String[] groups)
					: this()
			{
				mCategory = category;
				//mName = category.GetDescriptionOrName();

				for (Int32 i = 0; i < groups.Length; i++)
				{
					//this.AddNewItem(groups[i]);
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление общей площади территории
			/// </summary>
			/// <returns>Общая площадь территории</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Double GetTotalArea()
			{
				return (0);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий категорию вид разрешенного использования
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CLandPermittedUse : CNameableID, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsPercent = new PropertyChangedEventArgs(nameof(Percent));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal IArea mParentArea;
			internal Double mRequiredArea;
			internal Double mRequiredPercent;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ПАРАМЕТРЫ ТЕРРИТОРИИ
			//
			/// <summary>
			/// Общая площадь территории
			/// </summary>
			[DisplayName("Площадь, га")]
			[Description("Общая площадь территории")]
			[Category("Территория")]
			[XmlIgnore]
			public Double Area
			{
				get
				{
					return (GetTotalArea());
				}
			}

			/// <summary>
			/// Общая площадь территории в процентах
			/// </summary>
			[DisplayName("Процент от общей")]
			[Description("Общая площадь территории")]
			[Category("Территория")]
			[XmlIgnore]
			public Double Percent
			{
				get
				{
					return (0.0);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CLandPermittedUse()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление общей площади территории
			/// </summary>
			/// <returns>Общая площадь территории</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Double GetTotalArea()
			{
				return (0);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент территории
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CLandElement : CNameableID, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsOwnership = new PropertyChangedEventArgs(nameof(Ownership));
			protected static PropertyChangedEventArgs PropertyArgsPercent = new PropertyChangedEventArgs(nameof(Percent));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal Double mArea;
			protected internal TOwnershipType mOwnership;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ПАРАМЕТРЫ ТЕРРИТОРИИ
			//
			/// <summary>
			/// Площадь элемента территории
			/// </summary>
			[DisplayName("Площадь, га")]
			[Description("Площадь элемента территории")]
			[Category("Территория")]
			[XmlAttribute]
			public Double Area
			{
				get { return (mArea); }
				set
				{
					mArea = value;
					NotifyPropertyChanged(PropertyArgsArea);
					NotifyPropertyChanged(PropertyArgsPercent);
				}
			}

			/// <summary>
			/// Процент элемента территории
			/// </summary>
			[DisplayName("Процент, %")]
			[Description("Процент элемента территории")]
			[Category("Территория")]
			[XmlIgnore]
			public Double Percent
			{
				get
				{
					return (0.0);
				}
				set
				{
					//if (mLand != null)
					//{
					//	mArea = value / 100 * mLand.Area;
					//	NotifyPropertyChanged(PropertyArgsArea);
					//	mLand.NotifyPropertyChanged(PropertyArgsArea);
					//}
				}
			}

			/// <summary>
			/// Принадлежность элемента территории по собственности
			/// </summary>
			[DisplayName("Собственник")]
			[Description("Принадлежность элемента территории по собственности")]
			[Category("Территория")]
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
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CLandElement()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CLandElement(String name)
					: base(name)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================