//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема муниципальных программ
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGMunicipalProgramSub.cs
*		Определение концепции муниципальной подпрограммы.
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
using System.ComponentModel.DataAnnotations.Schema;
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
		/// Муниципальная подпрограмма
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMunicipalSubProgram : CNameableId, ILotusNotCalculation
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsShortName = new PropertyChangedEventArgs(nameof(ShortName));
			protected static readonly PropertyChangedEventArgs PropertyArgsDesc = new PropertyChangedEventArgs(nameof(Desc));
			protected static readonly PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));

			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVerified = new PropertyChangedEventArgs(nameof(IsVerified));
			#endregion
#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CMunicipalSubProgram"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CMunicipalSubProgram>();
				model.ToTable("municipal_sub_program");
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

				var property_program_id = model.Property(vs => vs.ProgramId);
				property_program_id.HasColumnName("program_id");

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

			// Расчеты
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsVerified;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Краткое наименование муниципальной подпрограммы
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
			/// Описание подпрограммы
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
			///  Условный номер подпрограммы
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

			//
			// СВЯЗАННЫЕ ОБЪЕКТЫ
			//
			/// <summary>
			/// Идентификатор муниципальной программы
			/// </summary>
			public Int64? ProgramId { get; set; }

			/// <summary>
			/// Муниципальная программа
			/// </summary>
			public CMunicipalProgram Program { get; set; }

			/// <summary>
			/// Наименование муниципальной программы
			/// </summary>
			public String ProgramName
			{
				get
				{
					if (Program == null)
					{
						return ("");
					}
					else
					{
						return (Program.Name);
					}
				}
			}

			/// <summary>
			/// Список индикаторов муниципальной подпрограммы
			/// </summary>
			public List<CMunicipalProgramIndicator> Indicators { get; set; } = new List<CMunicipalProgramIndicator>();

			/// <summary>
			/// Список мероприятий муниципальной подпрограммы
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
			public CMunicipalSubProgram()
				: this("Муниципальная программа")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CMunicipalSubProgram(String name)
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