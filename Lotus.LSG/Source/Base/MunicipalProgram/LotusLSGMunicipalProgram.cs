//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема муниципальных программ
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGMunicipalProgram.cs
*		Определение концепции муниципальной программы.
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
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
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
		//! \addtogroup MunicipalityBaseProgram
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Муниципальная программа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CMunicipalProgram : CNameableId, ILotusNotCalculation
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsShortName = new PropertyChangedEventArgs(nameof(ShortName));
			protected static readonly PropertyChangedEventArgs PropertyArgsDesc = new PropertyChangedEventArgs(nameof(Desc));
			protected static readonly PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static readonly PropertyChangedEventArgs PropertyArgsBeginDate = new PropertyChangedEventArgs(nameof(BeginDate));
			protected static readonly PropertyChangedEventArgs PropertyArgsEndDate = new PropertyChangedEventArgs(nameof(EndDate));
			
			protected static readonly PropertyChangedEventArgs PropertyArgsEditionDate = new PropertyChangedEventArgs(nameof(EditionDate));
			protected static readonly PropertyChangedEventArgs PropertyArgsEditionDocument = new PropertyChangedEventArgs(nameof(EditionDocument));
			
			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVerified = new PropertyChangedEventArgs(nameof(IsVerified));
			#endregion
#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CMunicipalProgram"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CMunicipalProgram>();
				model.ToTable("municipal_program");
				model.HasKey(vs => vs.Id);
				model.HasIndex(vs => vs.Id).IsUnique();
				model.Ignore(vs => vs.InspectorObjectName);
				model.Ignore(vs => vs.InspectorTypeName);

				var property_name = model.Property(vs => vs.Name);
				property_name.HasColumnName("names");
				property_name.HasMaxLength(200);
				property_name.IsRequired();

				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");

				var property_sname = model.Property(vs => vs.ShortName);
				property_sname.HasColumnName("sname");
				property_sname.HasMaxLength(40);

				var property_desc = model.Property(vs => vs.Desc);
				property_desc.HasColumnName("desc");
				property_desc.HasMaxLength(200);

				var property_number = model.Property(vs => vs.Number);
				property_number.HasColumnName("number");
				property_number.HasMaxLength(10);

				var property_begin_date = model.Property(vs => vs.BeginDate);
				property_begin_date.HasColumnName("begin_date");
				//property_begin_date.HasDefaultValueSql("current_timestamp()");

				var property_end_date = model.Property(vs => vs.EndDate);
				property_end_date.HasColumnName("end_date");
				//property_end_date.HasDefaultValueSql("current_timestamp()");

				var property_edition_date = model.Property(vs => vs.EditionDate);
				property_edition_date.HasColumnName("edition_date");
				//property_edition_date.HasDefaultValueSql("current_timestamp()");

				var property_edition_document = model.Property(vs => vs.EditionDocument);
				property_edition_document.HasColumnName("edition_document");
				property_edition_document.HasMaxLength(200);

				var property_not_calculation = model.Property(vs => vs.NotCalculation);
				property_not_calculation.HasColumnName("not_calc");

				var property_verified = model.Property(vs => vs.IsVerified);
				property_verified.HasColumnName("verified");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mShortName;
			protected internal String mDesc;
			protected internal String mNumber;
			protected internal DateTime mBeginDate;
			protected internal DateTime mEndDate;

			// Редакция
			protected internal DateTime mEditionDate;
			protected internal String mEditionDocument;

			// Расчеты
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsVerified;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Краткое наименование муниципальной программы
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
			/// Описание программы
			/// </summary>
			public String? Desc
			{
				get { return (mDesc); }
				set
				{
					mDesc = value;
					NotifyPropertyChanged(PropertyArgsDesc);
				}
			}

			/// <summary>
			///  Условный номер программы
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
			/// Дата начало действия муниципальной программы 
			/// </summary>
			public DateTime BeginDate
			{
				get { return (mBeginDate); }
				set
				{
					mBeginDate = value;
					NotifyPropertyChanged(PropertyArgsBeginDate);
				}
			}

			/// <summary>
			/// Дата окончания действия муниципальной программы 
			/// </summary>
			public DateTime EndDate
			{
				get { return (mEndDate); }
				set
				{
					mEndDate = value;
					NotifyPropertyChanged(PropertyArgsEndDate);
				}
			}

			/// <summary>
			/// Дата редакции программы
			/// </summary>
			public DateTime? EditionDate
			{
				get { return (mEditionDate); }
				set
				{
					mEditionDate = value ?? DateTime.Now;
					NotifyPropertyChanged(PropertyArgsEditionDate);
				}
			}

			/// <summary>
			/// Документ утверждающий редакцию
			/// </summary>
			public String? EditionDocument
			{
				get { return (mEditionDocument); }
				set
				{
					mEditionDocument = value;
					NotifyPropertyChanged(PropertyArgsEditionDocument);
				}
			}

			//
			// СВЯЗАННЫЕ ОБЪЕКТЫ
			//
			/// <summary>
			/// Список подпрограм муниципальной программы
			/// </summary>
			public List<CMunicipalSubProgram> SubPrograms { get; set; } = new List<CMunicipalSubProgram>();

			/// <summary>
			/// Список индикаторов муниципальной программы
			/// </summary>
			public List<CMunicipalProgramIndicator> Indicators { get; set; } = new List<CMunicipalProgramIndicator>();

			/// <summary>
			/// Список мероприятий муниципальной программы
			/// </summary>
			public List<CMunicipalProgramActivity> Activities { get; set; } = new List<CMunicipalProgramActivity>();
			#endregion

			#region ======================================= СВОЙСТВА ILotusNotCalculation =============================
			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
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
			#endregion

			#region ======================================= СВОЙСТВА ILotusVerified ===================================
			/// <summary>
			/// Статус верификации объекта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Boolean IsVerified
			{
				get { return (mIsVerified); }
				set
				{
					mIsVerified = value;
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
			public CMunicipalProgram()
				: this("Муниципальная программа")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CMunicipalProgram(String name)
				: base(name)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Группирование индикаторов по указанным свойствам
			/// </summary>
			/// <param name="property_names">Набор свойств для группирования</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void GroupingIndicatorsBy(params String[] property_names)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Группирование мероприятий по указанным свойствам
			/// </summary>
			/// <param name="property_names">Набор свойств для группирования</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void GroupingActivitiesBy(params String[] property_names)
			{
			}
			#endregion

			#region ======================================= ФИНАСОВЫЕ ДАННЫЕ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение стоимости всех мероприятий по уровням бюджета
			/// </summary>
			/// <param name="budget_financing">Уровень бюджета</param>
			/// <returns>Стоимость мероприятий</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual String GetFinanceFromBudget(TBudgetFinancingSet budget_financing)
			{
				//Decimal total = 0;
				//for (Int32 i = 0; i < mEntities.Count; i++)
				//{
				//	total += mEntities[i].GetBudgetFinancingOfSet(budget_financing);
				//}

				//return ((total / 1000).ToString("N3", CultureInfo.CurrentCulture));
				return ("");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение стоимости всех мероприятий указанной программы и уровней бюджета
			/// </summary>
			/// <param name="program_name">Наименование подпрограммы</param>
			/// <param name="budget_financing">Уровень бюджета</param>
			/// <returns>Стоимость мероприятий</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual String GetFinanceFromProgram(String program_name, TBudgetFinancingSet budget_financing)
			{
				//Decimal total = 0;
				//for (Int32 i = 0; i < mEntities.Count; i++)
				//{
				//	if(mEntities[i].SubProgramName == program_name)
				//	{
				//		total += mEntities[i].GetBudgetFinancingOfSet(budget_financing);
				//	}
				//}

				//return ((total/1000).ToString("N3", CultureInfo.CurrentCulture));
				return ("");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение стоимости всех мероприятий по году и уровней бюджета
			/// </summary>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Уровень бюджета</param>
			/// <returns>Стоимость мероприятий</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual String GetFinanceFromYear(Int32 year, TBudgetFinancingSet budget_financing)
			{
				//Decimal total = 0;
				//for (Int32 i = 0; i < mEntities.Count; i++)
				//{
				//	if (mEntities[i].YearExecution == year)
				//	{
				//		total += mEntities[i].GetBudgetFinancingOfSet(budget_financing);
				//	}
				//}

				//return ((total/1000).ToString("N3", CultureInfo.CurrentCulture));
				return ("");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение стоимости всех мероприятий указанной программы, года и уровней бюджета
			/// </summary>
			/// <param name="program_name">Наименование подпрограммы</param>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Уровень бюджета</param>
			/// <returns>Стоимость мероприятий</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual String GetFinanceFromProgramAndYear(String program_name, Int32 year, TBudgetFinancingSet budget_financing)
			{
				//Decimal total = 0;
				//for (Int32 i = 0; i < mEntities.Count; i++)
				//{
				//	if (mEntities[i].SubProgramName == program_name && mEntities[i].YearExecution == year)
				//	{
				//		total += mEntities[i].GetBudgetFinancingOfSet(budget_financing);
				//	}
				//}

				//return ((total / 1000).ToString("N3", CultureInfo.CurrentCulture));
				return ("");
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================