//=======================================// Последнее изменение от 27.03.2022==============================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Дорожное хозяйство
// Подраздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRoadAppropriations.cs
*		Финансовое обеспечение.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml;
//---------------------------------------------------------------------------------------------------------------------
#if USE_EFC
using Microsoft.EntityFrameworkCore;
#endif
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
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
		/// Сводный бюджет дорожного фонда
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadBudget : CNameableID
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			private static PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			private static PropertyChangedEventArgs PropertyArgsDistributionDate = new PropertyChangedEventArgs(nameof(DistributionDate));
			private static PropertyChangedEventArgs PropertyArgsLocalBudget = new PropertyChangedEventArgs(nameof(LocalBudget));
			private static PropertyChangedEventArgs PropertyArgsRegionalBudget = new PropertyChangedEventArgs(nameof(RegionalBudget));
			//private static PropertyChangedEventArgs PropertyArgsEditionDate = new PropertyChangedEventArgs(nameof(EditionDate));
			//private static PropertyChangedEventArgs PropertyArgsEditionNumber = new PropertyChangedEventArgs(nameof(EditionNumber));
			//private static PropertyChangedEventArgs PropertyArgsEditionDocument = new PropertyChangedEventArgs(nameof(EditionDocument));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mNumber;
			protected internal DateTime mDistributionDate;
			protected internal Decimal mLocalBudget;
			protected internal Decimal mRegionalBudget;

			// Редакция
			protected internal DateTime mEditionDate;
			protected internal String mEditionNumber;
			protected internal String mEditionDocument;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Условный номер распределения средств
			/// </summary>
			public String Number
			{
				get { return (mNumber); }
				set
				{
					mNumber = value;
					NotifyPropertyChanged(PropertyArgsNumber);
				}
			}

			/// <summary>
			/// Дата распределения средств
			/// </summary>
			public DateTime DistributionDate
			{
				get { return (mDistributionDate); }
				set
				{
					mDistributionDate = value;
					NotifyPropertyChanged(PropertyArgsDistributionDate);
				}
			}

			/// <summary>
			/// Стоимость распределения средств по местному бюджету
			/// </summary>
			public Decimal LocalBudget
			{
				get { return (mLocalBudget); }
				set
				{
					mLocalBudget = value;
					NotifyPropertyChanged(PropertyArgsLocalBudget);
				}
			}

			/// <summary>
			/// Стоимость распределения средств по областному бюджету
			/// </summary>
			public Decimal RegionalBudget
			{
				get { return (mRegionalBudget); }
				set
				{
					mRegionalBudget = value;
					NotifyPropertyChanged(PropertyArgsRegionalBudget);
				}
			}

			/// <summary>
			/// Ответственный исполнитель
			/// </summary>
			public CSubjectCivil Executor { get; set; }

			/// <summary>
			/// Идентификатор ответственного исполнителя
			/// </summary>
			public Int32? ExecutorID { get; set; }

			/// <summary>
			/// Наименование ответственного исполнителя
			/// </summary>
			public String ExecutorName
			{
				get
				{
					if (Executor == null)
					{
						return ("");
					}
					else
					{
						return (Executor.Name);
					}
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Распределение средств дорожного фонда
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadBudgetDistribution : CNameableID, IComparable<CRoadBudgetDistribution>, ICloneable,
			ILotusNotCalculation, ILotusCopyParameters, ILotusDuplicate<CRoadBudgetDistribution>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			private static PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			private static PropertyChangedEventArgs PropertyArgsEditionDate = new PropertyChangedEventArgs(nameof(EditionDate));
			private static PropertyChangedEventArgs PropertyArgsEditionNumber = new PropertyChangedEventArgs(nameof(EditionNumber));
			private static PropertyChangedEventArgs PropertyArgsEditionDocument = new PropertyChangedEventArgs(nameof(EditionDocument));
			private static PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mNumber;

			// Редакция
			protected internal DateTime mEditionDate;
			protected internal String mEditionNumber;
			protected internal String mEditionDocument;

			// Расчеты
			protected internal Boolean mNotCalculation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Условный номер распределения средств
			/// </summary>
			public String Number
			{
				get { return (mNumber); }
				set
				{
					mNumber = value;
					NotifyPropertyChanged(PropertyArgsNumber);
				}
			}

			/// <summary>
			/// Общая стоимость распределения средств по местному бюджету
			/// </summary>
			public Decimal LocalBudget
			{
				get 
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Items.Count; i++)
					{
						if(Items[i].NotCalculation == false)
						{
							result += Items[i].LocalBudget;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Общая стоимость распределения средств по областному бюджету
			/// </summary>
			public Decimal RegionalBudget
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Items.Count; i++)
					{
						if (Items[i].NotCalculation == false)
						{
							result += Items[i].RegionalBudget;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Список элементов распределения средств
			/// </summary>
			public List<CRoadBudgetDistributionItem> Items { get; set; } = new List<CRoadBudgetDistributionItem>();

			/// <summary>
			/// Дата редакции программы
			/// </summary>
			public DateTime EditionDate
			{
				get { return (mEditionDate); }
				set
				{
					mEditionDate = value;
					NotifyPropertyChanged(PropertyArgsEditionDate);
				}
			}

			/// <summary>
			/// Документ утверждающий редакцию
			/// </summary>
			public String EditionNumber
			{
				get { return (mEditionNumber); }
				set
				{
					mEditionNumber = value;
					NotifyPropertyChanged(PropertyArgsEditionNumber);
				}
			}

			/// <summary>
			/// Документ утверждающий редакцию
			/// </summary>
			public String EditionDocument
			{
				get { return (mEditionDocument); }
				set
				{
					mEditionDocument = value;
					NotifyPropertyChanged(PropertyArgsEditionDocument);
				}
			}

			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadBudgetDistribution()
			{

			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CRoadBudgetDistribution other)
			{
				return (Name.CompareTo(other.Name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object Clone()
			{
				CRoadBudgetDistribution clone = new CRoadBudgetDistribution();
				clone.CopyParameters(this);
				return (clone);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Наименование объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (Name);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void CopyParameters(System.Object source_object)
			{
				//base.CopyParameters(source_object);
				if (source_object != null && source_object is CRoadBudgetDistribution)
				{
					CRoadBudgetDistribution source = source_object as CRoadBudgetDistribution;
					mNumber = source.mNumber;
					mEditionDate = source.mEditionDate;
					mEditionNumber = source.mEditionNumber;
					mEditionDocument = source.mEditionDocument;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CRoadBudgetDistribution Duplicate()
			{
				return (Clone() as CRoadBudgetDistribution);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент распределение средств дорожного фонда
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadBudgetDistributionItem : CIdentifierID, IComparable<CRoadBudgetDistributionItem>, ICloneable,
			ILotusNotCalculation, ILotusCopyParameters, ILotusDuplicate<CRoadBudgetDistributionItem>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			private static PropertyChangedEventArgs PropertyArgsLocalBudget = new PropertyChangedEventArgs(nameof(LocalBudget));
			private static PropertyChangedEventArgs PropertyArgsRegionalBudget = new PropertyChangedEventArgs(nameof(RegionalBudget));
			private static PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CRoadBudgetDistributionItem"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CRoadBudgetDistributionItem>();
				model.ToTable("road_budget_dist_item");
				model.HasKey(vs => vs.ID);
				model.HasIndex(vs => vs.ID).IsUnique();
				model.Ignore(vs => vs.NotCalculation);
				model.Ignore(vs => vs.BudgetDistributionName);
				model.Ignore(vs => vs.ExecutorName);

				var property_id = model.Property(vs => vs.ID);
				property_id.HasColumnName("id");

				var property_local_budget = model.Property(vs => vs.LocalBudget);
				property_local_budget.HasColumnName("local_budget");
				property_local_budget.HasColumnType("decimal(12, 2)");

				var property_regional_budget = model.Property(vs => vs.RegionalBudget);
				property_regional_budget.HasColumnName("regional_budget");
				property_regional_budget.HasColumnType("decimal(12, 2)");

				var property_responsible_executor_id = model.Property(vs => vs.ExecutorID);
				property_responsible_executor_id.HasColumnName("executor_id");

				var property_budget_distribution_id = model.Property(vs => vs.BudgetDistributionID);
				property_budget_distribution_id.HasColumnName("distribution_id");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Decimal mLocalBudget;
			protected internal Decimal mRegionalBudget;

			// Расчеты
			protected internal Boolean mNotCalculation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Стоимость распределения средств по местному бюджету
			/// </summary>
			public Decimal LocalBudget
			{
				get { return (mLocalBudget); }
				set
				{
					mLocalBudget = value;
					NotifyPropertyChanged(PropertyArgsLocalBudget);
				}
			}

			/// <summary>
			/// Стоимость распределения средств по областному бюджету
			/// </summary>
			public Decimal RegionalBudget
			{
				get { return (mRegionalBudget); }
				set
				{
					mRegionalBudget = value;
					NotifyPropertyChanged(PropertyArgsRegionalBudget);
				}
			}

			/// <summary>
			/// Ответственный исполнитель
			/// </summary>
			public CLegalEntityBase Executor { get; set; }

			/// <summary>
			/// Идентификатор ответственного исполнителя
			/// </summary>
			public Int32? ExecutorID { get; set; }

			/// <summary>
			/// Наименование ответственного исполнителя
			/// </summary>
			public String ExecutorName
			{
				get
				{
					if (Executor == null)
					{
						return ("");
					}
					else
					{
						return (Executor.Name);
					}
				}
			}

			/// <summary>
			/// Распределение средств дорожного фонда 
			/// </summary>
			public CRoadBudgetDistribution BudgetDistribution { get; set; }

			/// <summary>
			/// Идентификатор распределения средств дорожного фонда
			/// </summary>
			public Int32? BudgetDistributionID { get; set; }

			/// <summary>
			/// Наименование распределения средств дорожного фонда
			/// </summary>
			public String BudgetDistributionName
			{
				get
				{
					if (BudgetDistribution == null)
					{
						return ("");
					}
					else
					{
						return (BudgetDistribution.Name);
					}
				}
			}

			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadBudgetDistributionItem()
			{

			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CRoadBudgetDistributionItem other)
			{
				return (ID.CompareTo(other.ID));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				CRoadBudgetDistributionItem clone = new CRoadBudgetDistributionItem();
				clone.CopyParameters(this);
				return (clone);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Наименование объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (ID.ToString());
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void CopyParameters(System.Object source_object)
			{
				if (source_object != null && source_object is CRoadBudgetDistributionItem)
				{
					CRoadBudgetDistributionItem source = source_object as CRoadBudgetDistributionItem;
					LocalBudget = source.LocalBudget;
					RegionalBudget = source.RegionalBudget;
					ExecutorID = source.ExecutorID;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CRoadBudgetDistributionItem Duplicate()
			{
				return (Clone() as CRoadBudgetDistributionItem);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Критерии для разделения средств дорожного фонда
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadDistributionCriterion : CNameableID, IComparable<CRoadDistributionCriterion>, ICloneable,
			ILotusNotCalculation
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			private static PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			private static PropertyChangedEventArgs PropertyArgsShortName = new PropertyChangedEventArgs(nameof(ShortName));
			private static PropertyChangedEventArgs PropertyArgsValueUnit = new PropertyChangedEventArgs(nameof(ValueUnit));
			private static PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mNumber;
			protected internal String mShortName;
			protected internal String mValueUnit;

			// Расчеты
			protected internal Boolean mNotCalculation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Условный номер распределения средств
			/// </summary>
			public String Number
			{
				get { return (mNumber); }
				set
				{
					mNumber = value;
					NotifyPropertyChanged(PropertyArgsNumber);
				}
			}

			/// <summary>
			/// Краткое наименование 
			/// </summary>
			public String ShortName
			{
				get { return (mShortName); }
				set
				{
					mShortName = value;
					NotifyPropertyChanged(PropertyArgsShortName);
				}
			}

			/// <summary>
			/// Единца измерения критерия распределения средств
			/// </summary>
			public String ValueUnit
			{
				get { return (mValueUnit); }
				set
				{
					mValueUnit = value;
					NotifyPropertyChanged(PropertyArgsValueUnit);
				}
			}

			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadDistributionCriterion()
			{

			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CRoadDistributionCriterion other)
			{
				return (Name.CompareTo(other.Name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object Clone()
			{
				CRoadDistributionCriterion clone = new CRoadDistributionCriterion();
				clone.CopyParameters(this);
				return (clone);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Наименование объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (Name);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void CopyParameters(System.Object source_object)
			{
				//base.CopyParameters(source_object);
				if (source_object != null && source_object is CRoadDistributionCriterion)
				{
					CRoadDistributionCriterion source = source_object as CRoadDistributionCriterion;
					mNumber = source.mNumber;
					mShortName = source.mShortName;
					mValueUnit = source.mValueUnit;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CRoadDistributionCriterion Duplicate()
			{
				return (Clone() as CRoadDistributionCriterion);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Критерии для разделения средств дорожного фонда
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadDistributionCriterionValue : CIdentifierID, IComparable<CRoadDistributionCriterionValue>, 
			ICloneable, ILotusCopyParameters, ILotusNotCalculation, ILotusDuplicate<CRoadDistributionCriterionValue>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			private static PropertyChangedEventArgs PropertyArgsValue = new PropertyChangedEventArgs(nameof(Value));
			private static PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Double mValue;

			// Расчеты
			protected internal Boolean mNotCalculation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Значение критерия
			/// </summary>
			public Double Value
			{
				get { return (mValue); }
				set
				{
					mValue = value;
					NotifyPropertyChanged(PropertyArgsValue);
				}
			}

			/// <summary>
			/// Критерий
			/// </summary>
			public CRoadDistributionCriterion Criterion { get; set; }

			/// <summary>
			/// Идентификатор критерия
			/// </summary>
			public Int32? CriterionID { get; set; }

			/// <summary>
			/// Наименование критерия
			/// </summary>
			public String CriterionName
			{
				get
				{
					if (Criterion == null)
					{
						return ("");
					}
					else
					{
						return (Criterion.Name);
					}
				}
			}

			/// <summary>
			/// Краткое наименование критерия
			/// </summary>
			public String CriterionShortName
			{
				get
				{
					if (Criterion == null)
					{
						return ("");
					}
					else
					{
						return (Criterion.ShortName);
					}
				}
			}

			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadDistributionCriterionValue()
			{

			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CRoadDistributionCriterionValue other)
			{
				return (Value.CompareTo(other.Value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				CRoadDistributionCriterionValue clone = new CRoadDistributionCriterionValue();
				clone.CopyParameters(this);
				return (clone);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Наименование объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (Value.ToString());
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(System.Object source_object)
			{
				if (source_object != null && source_object is CRoadDistributionCriterionValue)
				{
					CRoadDistributionCriterionValue source = source_object as CRoadDistributionCriterionValue;
					mValue = source.mValue;
					CriterionID = source.CriterionID;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CRoadDistributionCriterionValue Duplicate()
			{
				return (Clone() as CRoadDistributionCriterionValue);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Критерии для разделения средств дорожного фонда
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRoadDistribution : CIdentifierID, IComparable<CRoadDistribution>,
			ICloneable, ILotusCopyParameters, ILotusNotCalculation, ILotusDuplicate<CRoadDistribution>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			//private static PropertyChangedEventArgs PropertyArgsValue = new PropertyChangedEventArgs(nameof(Value));
			private static PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Double mValue;

			// Расчеты
			protected internal Boolean mNotCalculation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Ответственный исполнитель
			/// </summary>
			public CLegalEntityBase Executor { get; set; }

			/// <summary>
			/// Идентификатор ответственного исполнителя
			/// </summary>
			public Int32? ExecutorID { get; set; }

			/// <summary>
			/// Наименование ответственного исполнителя
			/// </summary>
			public String ExecutorName
			{
				get
				{
					if (Executor == null)
					{
						return ("");
					}
					else
					{
						return (Executor.Name);
					}
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public List<CRoadDistributionCriterionValue> CriterionValues { get; set; } = new List<CRoadDistributionCriterionValue>();

			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadDistribution()
			{

			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CRoadDistribution other)
			{
				return (0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				CRoadDistribution clone = new CRoadDistribution();
				clone.CopyParameters(this);
				return (clone);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Наименование объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return ("");
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(System.Object source_object)
			{
				if (source_object != null && source_object is CRoadDistribution)
				{
					CRoadDistribution source = source_object as CRoadDistribution;
					//mValue = source.mValue;
					//CriterionID = source.CriterionID;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CRoadDistribution Duplicate()
			{
				return (Clone() as CRoadDistribution);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================