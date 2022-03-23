//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема муниципальных программ
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGMunicipalProgramActivity.cs
*		Мероприятия муниципальной программы.
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
using System.ComponentModel.DataAnnotations.Schema;
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
		/// Определение общей концепции мероприятия или эго этапа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface IMunicipalProgramActivity : ILotusNameable, ILotusIdentifierId, ILotusBudgetFinancingNotCalculation
		{
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Этап исполнения мероприятия 
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TProgramActivityStage>))]
		public enum TProgramActivityStage
		{
			/// <summary>
			/// Мероприятие только запланировано
			/// </summary>
			[Description("Запланировано")]
			Planned,

			/// <summary>
			/// Мероприятие исполняется
			/// </summary>
			[Description("Исполняется")]
			Performed,

			/// <summary>
			/// Мероприятие исполнено
			/// </summary>
			[Description("Исполнено")]
			Completed,

			/// <summary>
			/// Мероприятие отменено
			/// </summary>
			[Description("Отменено")]
			Canceled,

			/// <summary>
			/// Мероприятие не исполнено
			/// </summary>
			[Description("Не исполнено")]
			Notexecuted
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип мероприятия с точки зрения финансов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TProgramActivityFinance>))]
		public enum TProgramActivityFinance
		{
			/// <summary>
			/// Организационный
			/// </summary>
			[Description("Организационный")]
			Organizational,

			/// <summary>
			/// Финансовый
			/// </summary>
			[Description("Финансовый")]
			Financial
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий вспомогательную модель для работы с перечислением <see cref="TProgramActivityStage"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CProgramActivityStageModel
		{
			public static readonly CProgramActivityStageModel[] Data = new CProgramActivityStageModel[5]
			{
				new CProgramActivityStageModel()
				{
					Name = nameof(TProgramActivityStage.Planned),
					Desc = TProgramActivityStage.Planned.GetDescriptionOrName(),
					Value = TProgramActivityStage.Planned
				},

				new CProgramActivityStageModel()
				{
					Name = nameof(TProgramActivityStage.Performed),
					Desc = TProgramActivityStage.Performed.GetDescriptionOrName(),
					Value = TProgramActivityStage.Performed
				},

				new CProgramActivityStageModel()
				{
					Name = nameof(TProgramActivityStage.Completed),
					Desc = TProgramActivityStage.Completed.GetDescriptionOrName(),
					Value = TProgramActivityStage.Completed
				},

				new CProgramActivityStageModel()
				{
					Name = nameof(TProgramActivityStage.Canceled),
					Desc = TProgramActivityStage.Canceled.GetDescriptionOrName(),
					Value = TProgramActivityStage.Canceled
				},

				new CProgramActivityStageModel()
				{
					Name = nameof(TProgramActivityStage.Notexecuted),
					Desc = TProgramActivityStage.Notexecuted.GetDescriptionOrName(),
					Value = TProgramActivityStage.Notexecuted
				}
			};

			/// <summary>
			/// Имя
			/// </summary>
			public String Name { get; set; }

			/// <summary>
			/// Описание
			/// </summary>
			public String Desc { get; set; }

			/// <summary>
			/// Значение
			/// </summary>
			public TProgramActivityStage Value { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Мероприятие муниципальной программы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMunicipalProgramActivity : CNameableId, IMunicipalProgramActivity, IComparable<CMunicipalProgramActivity>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsStage = new PropertyChangedEventArgs(nameof(Stage));
			protected static readonly PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static readonly PropertyChangedEventArgs PropertyArgsDesc = new PropertyChangedEventArgs(nameof(Desc));
			protected static readonly PropertyChangedEventArgs PropertyArgsGroup = new PropertyChangedEventArgs(nameof(Group));
			protected static readonly PropertyChangedEventArgs PropertyArgsSubGroup = new PropertyChangedEventArgs(nameof(SubGroup));
			protected static readonly PropertyChangedEventArgs PropertyArgsBeginDate = new PropertyChangedEventArgs(nameof(BeginDate));
			protected static readonly PropertyChangedEventArgs PropertyArgsEndDate = new PropertyChangedEventArgs(nameof(EndDate));

			protected static readonly PropertyChangedEventArgs PropertyArgsPrice = new PropertyChangedEventArgs(nameof(Price));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceLocal = new PropertyChangedEventArgs(nameof(PriceLocal));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceRegional = new PropertyChangedEventArgs(nameof(PriceRegional));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceFederal = new PropertyChangedEventArgs(nameof(PriceFederal));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceExtra = new PropertyChangedEventArgs(nameof(PriceExtra));


			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVerified = new PropertyChangedEventArgs(nameof(IsVerified));
			#endregion
#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CMunicipalProgramActivity"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CMunicipalProgramActivity>();
				model.ToTable("municipal_activity");
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

				var property_number = model.Property(vs => vs.Number);
				property_number.HasColumnName("number");
				property_number.HasMaxLength(10);

				var property_desc = model.Property(vs => vs.Desc);
				property_desc.HasColumnName("desc");
				property_desc.HasMaxLength(200);

				var property_group = model.Property(vs => vs.Group);
				property_group.HasColumnName("group");
				property_group.HasMaxLength(40);

				var property_subgroup = model.Property(vs => vs.SubGroup);
				property_subgroup.HasColumnName("subgroup");
				property_subgroup.HasMaxLength(10);

				var property_begin_date = model.Property(vs => vs.BeginDate);
				property_begin_date.HasColumnName("begin_date");

				var property_end_date = model.Property(vs => vs.EndDate);
				property_end_date.HasColumnName("end_date");

				var property_activity_mode = model.Property(vs => vs.Stage);
				property_activity_mode.HasColumnName("stage");

				var property_planed_value = model.Property(vs => vs.PlanedValue);
				property_planed_value.HasColumnName("planed_value");

				var property_program_id = model.Property(vs => vs.ProgramId);
				property_program_id.HasColumnName("program_id");

				var property_sub_program_id = model.Property(vs => vs.SubProgramId);
				property_sub_program_id.HasColumnName("sub_program_id");

				var property_indicator_id = model.Property(vs => vs.IndicatorId);
				property_indicator_id.HasColumnName("indicator_id");

				var property_responsible_executor_id = model.Property(vs => vs.ExecutorId);
				property_responsible_executor_id.HasColumnName("executor_id");

				var property_activity_id = model.Property(vs => vs.ActivityId);
				property_activity_id.HasColumnName("activity_id");

				var property_price_local = model.Property(vs => vs.PriceLocal);
				property_price_local.HasColumnName("price_local");
				property_price_local.HasColumnType("decimal(12, 2)");

				var property_price_regional = model.Property(vs => vs.PriceRegional);
				property_price_regional.HasColumnName("price_regional");
				property_price_regional.HasColumnType("decimal(12, 2)");

				var property_price_federal = model.Property(vs => vs.PriceFederal);
				property_price_federal.HasColumnName("price_federal");
				property_price_federal.HasColumnType("decimal(12, 2)");

				var property_price_extra = model.Property(vs => vs.PriceExtra);
				property_price_extra.HasColumnName("price_extra");
				property_price_extra.HasColumnType("decimal(12, 2)");

				var property_not_calculation = model.Property(vs => vs.NotCalculation);
				property_not_calculation.HasColumnName("not_calc");

				var property_verified = model.Property(vs => vs.IsVerified);
				property_verified.HasColumnName("verified");

				model.Ignore(vs => vs.Year);

				model.Ignore(vs => vs.ProgramName);
				model.Ignore(vs => vs.ProgramShortName);

				model.Ignore(vs => vs.SubProgramName);
				model.Ignore(vs => vs.SubProgramShortName);

				model.Ignore(vs => vs.IndicatorName);
				
				model.Ignore(vs => vs.ExecutorName);
				model.Ignore(vs => vs.ExecutorShortName);

				model.Ignore(vs => vs.ContractPrice);
				model.Ignore(vs => vs.ContractPriceLocal);
				model.Ignore(vs => vs.ContractPriceRegional);
				model.Ignore(vs => vs.ContractPriceFederal);
				model.Ignore(vs => vs.ContractPriceExtra);

				model.Ignore(vs => vs.Closure);
				model.Ignore(vs => vs.ClosureLocal);
				model.Ignore(vs => vs.ClosureRegional);
				model.Ignore(vs => vs.ClosureFederal);
				model.Ignore(vs => vs.ClosureExtra);

				model.Ignore(vs => vs.Appropriation);
				model.Ignore(vs => vs.AppropriationLocal);
				model.Ignore(vs => vs.AppropriationRegional);
				model.Ignore(vs => vs.AppropriationFederal);
				model.Ignore(vs => vs.AppropriationExtra);

				model.HasOne(x => x.ParentActivity)
					 .WithMany(x => x.Activities)
					 .HasForeignKey(x => x.ActivityId)
					 .IsRequired(false)
					 .OnDelete(DeleteBehavior.Restrict);
			}
			#endregion
#endif
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal TProgramActivityStage mStage;
			protected internal String mNumber;
			protected internal String mDesc;
			protected internal String mGroup;
			protected internal String mSubGroup;
			protected internal DateTime mBeginDate;
			protected internal DateTime mEndDate;

			// Финансирование
			protected internal Decimal mPriceLocal;
			protected internal Decimal mPriceRegional;
			protected internal Decimal mPriceFederal;
			protected internal Decimal mPriceExtra;

			// Расчеты
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsVerified;

			// Выполнение
			protected internal Boolean mIsDone;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Этап исполнения мероприятия 
			/// </summary>
			public TProgramActivityStage Stage
			{
				get { return (mStage); }
				set
				{
					mStage = value;
					NotifyPropertyChanged(PropertyArgsStage);
				}
			}

			/// <summary>
			/// Условный номер мероприятия
			/// </summary>
			public String? Number
			{
				get { return (mNumber); }
				set
				{
					mNumber = value;
					NotifyPropertyChanged(PropertyArgsNumber);
				}
			}

			/// <summary>
			/// Описание мероприятия
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
			/// Группа к которой относиться мероприятие
			/// </summary>
			public String? Group
			{
				get { return (mGroup); }
				set
				{
					mGroup = value;
					NotifyPropertyChanged(PropertyArgsGroup);
				}
			}

			/// <summary>
			/// Подгруппа к которой относиться мероприятие
			/// </summary>
			public String? SubGroup
			{
				get { return (mSubGroup); }
				set
				{
					mSubGroup = value;
					NotifyPropertyChanged(PropertyArgsSubGroup);
				}
			}

			/// <summary>
			/// Год выполнения мероприятия
			/// </summary>
			public Int32 Year
			{
				get { return (mBeginDate.Year); }
				set
				{
					mBeginDate = new DateTime(value, mBeginDate.Month, mBeginDate.Day);
					NotifyPropertyChanged(PropertyArgsBeginDate);
				}
			}

			/// <summary>
			/// Начало выполнения мероприятия
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
			/// Окончание выполнения мероприятия
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

			//
			// ОТВЕТСТВЕННЫЙ ИСПОЛНИТЕЛЬ
			//
			/// <summary>
			/// Идентификатор ответственного исполнителя
			/// </summary>
			public Int64? ExecutorId { get; set; }

			/// <summary>
			/// Ответственный исполнитель
			/// </summary>
			[ForeignKey(nameof(ExecutorId))]
			public CSubjectCivil? Executor { get; set; }

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
			/// Краткое наименование ответственного исполнителя
			/// </summary>
			public String ExecutorShortName
			{
				get
				{
					if (Executor == null)
					{
						return ("");
					}
					else
					{
						return (Executor.ShortName);
					}
				}
			}

			//
			// МУНИЦИПАЛЬНАЯ ПРОГРАММА
			//
			/// <summary>
			/// Идентификатор муниципальной программы
			/// </summary>
			public Int64? ProgramId { get; set; }

			/// <summary>
			/// Муниципальная программа
			/// </summary>
			[ForeignKey(nameof(ProgramId))]
			public CMunicipalProgram? Program { get; set; }

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
			/// Краткое наименование муниципальной программы
			/// </summary>
			public String ProgramShortName
			{
				get
				{
					if (Program == null)
					{
						return ("");
					}
					else
					{
						return (Program.ShortName);
					}
				}
			}

			//
			// МУНИЦИПАЛЬНАЯ ПОДПРОГРАММА
			//
			/// <summary>
			/// Идентификатор муниципальной подпрограммы
			/// </summary>
			public Int64? SubProgramId { get; set; }

			/// <summary>
			/// Муниципальная подпрограмма
			/// </summary>
			[ForeignKey(nameof(SubProgramId))]
			public CMunicipalSubProgram? SubProgram { get; set; }

			/// <summary>
			/// Наименование муниципальной подпрограммы
			/// </summary>
			public String SubProgramName
			{
				get
				{
					if (SubProgram == null)
					{
						return ("");
					}
					else
					{
						return (SubProgram.Name);
					}
				}
			}

			/// <summary>
			/// Краткое наименование муниципальной подпрограммы
			/// </summary>
			public String SubProgramShortName
			{
				get
				{
					if (SubProgram == null)
					{
						return ("");
					}
					else
					{
						return (SubProgram.ShortName);
					}
				}
			}

			//
			// ИНДИКАТОР МУНИЦИПАЛЬНОЙ ПРОГРАММЫ/ПОДПРОГРАММЫ
			//
			/// <summary>
			/// Идентификатор индикатора муниципальной подпрограммы
			/// </summary>
			public Int64? IndicatorId { get; set; }

			/// <summary>
			/// Индикатор муниципальной программы/подпрограммы
			/// </summary>
			[ForeignKey(nameof(IndicatorId))]
			public CMunicipalProgramIndicator? Indicator { get; set; }

			/// <summary>
			/// Наименование индикатора муниципальной подпрограммы
			/// </summary>
			public String IndicatorName
			{
				get
				{
					if (Indicator == null)
					{
						return ("");
					}
					else
					{
						return (Indicator.Name);
					}
				}
			}

			//
			// КОНТРАКТЫ
			//
			/// <summary>
			/// Список контрактов по мероприятию
			/// </summary>
			public List<CContract> Contracts { get; set; } = new List<CContract>();

			//
			// МЕРОПРИЯТИЯ
			//
			/// <summary>
			/// Идентификатор родительского мероприятия
			/// </summary>
			public Int64? ActivityId { get; set; }

			/// <summary>
			/// Родительское мероприятие
			/// </summary>
			public CMunicipalProgramActivity? ParentActivity { get; set; }

			/// <summary>
			/// Список дочерних мероприятий
			/// </summary>
			public List<CMunicipalProgramActivity> Activities { get; set; } = new List<CMunicipalProgramActivity>();

			//
			// ПЛАНОВЫЕ ПОКАЗАТЕЛИ
			//
			/// <summary>
			/// Плановый показатель мероприятия
			/// </summary>
			public Double PlanedValue { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА КОНТРАКТЫ ========================================
			/// <summary>
			/// Стоимость мероприятия в соответствии с заключённом контрактом
			/// </summary>
			public Decimal ContractPrice
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].Price;
						}
					}

                    for (Int32 i = 0; i < Activities.Count; i++)
                    {
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ContractPrice;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость мероприятия в соответствии с заключённом контрактом для местного бюджета
			/// </summary>
			public Decimal ContractPriceLocal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].PriceLocal;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ContractPriceLocal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость мероприятия в соответствии с заключённом контрактом для областного бюджета
			/// </summary>
			public Decimal ContractPriceRegional
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].PriceRegional;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ContractPriceRegional;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость мероприятия в соответствии с заключённом контрактом для федерального бюджета
			/// </summary>
			public Decimal ContractPriceFederal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].PriceFederal;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ContractPriceFederal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость мероприятия в соответствии с заключённом контрактом для внебюджетных средств
			/// </summary>
			public Decimal ContractPriceExtra
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].PriceExtra;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ContractPriceExtra;
						}
					}

					return (result);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ИСПОЛНЕНИЕ =======================================
			/// <summary>
			/// Стоимость исполнения мероприятия в соответствии с заключённом контрактом
			/// </summary>
			public Decimal Closure
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].Closure;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].Closure;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость исполнения мероприятия в соответствии с заключённом контрактом для местного бюджета
			/// </summary>
			public Decimal ClosureLocal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].ClosureLocal;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ClosureLocal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость исполнения мероприятия в соответствии с заключённом контрактом для областного бюджета
			/// </summary>
			public Decimal ClosureRegional
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].ClosureRegional;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ClosureRegional;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость исполнения мероприятия в соответствии с заключённом контрактом для федерального бюджета
			/// </summary>
			public Decimal ClosureFederal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].ClosureFederal;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ClosureFederal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Стоимость исполнения мероприятия в соответствии с заключённом контрактом для внебюджетных средств
			/// </summary>
			public Decimal ClosureExtra
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < Contracts.Count; i++)
					{
						if (Contracts[i].NotCalculation == false)
						{
							result += Contracts[i].ClosureExtra;
						}
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].ClosureExtra;
						}
					}

					return (result);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ИСПОЛНЕНИЕ =======================================
			/// <summary>
			/// Ассигнования
			/// </summary>
			public Decimal Appropriation
			{
				get
				{
					Decimal result = 0;
					switch (mStage)
                    {
                        case TProgramActivityStage.Planned:
                            {
								result += Price;
							}
                            break;
                        case TProgramActivityStage.Performed:
                            {
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].Price;
									}
								}
							}
                            break;
                        case TProgramActivityStage.Completed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].Closure;
									}
								}
							}
							break;
                        case TProgramActivityStage.Canceled:
                            break;
                        case TProgramActivityStage.Notexecuted:
                            break;
                        default:
                            break;
                    }

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].Appropriation;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Ассигнования для местного бюджета
			/// </summary>
			public Decimal AppropriationLocal
			{
				get
				{
					Decimal result = 0;
					switch (mStage)
					{
						case TProgramActivityStage.Planned:
							{
								result += PriceLocal;
							}
							break;
						case TProgramActivityStage.Performed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].PriceLocal;
									}
								}
							}
							break;
						case TProgramActivityStage.Completed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].ClosureLocal;
									}
								}
							}
							break;
						case TProgramActivityStage.Canceled:
							break;
						case TProgramActivityStage.Notexecuted:
							break;
						default:
							break;
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].AppropriationLocal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Ассигнования для областного бюджета
			/// </summary>
			public Decimal AppropriationRegional
			{
				get
				{
					Decimal result = 0;
					switch (mStage)
					{
						case TProgramActivityStage.Planned:
							{
								result += PriceRegional;
							}
							break;
						case TProgramActivityStage.Performed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].PriceRegional;
									}
								}
							}
							break;
						case TProgramActivityStage.Completed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].ClosureRegional;
									}
								}
							}
							break;
						case TProgramActivityStage.Canceled:
							break;
						case TProgramActivityStage.Notexecuted:
							break;
						default:
							break;
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].AppropriationRegional;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Ассигнования для федерального бюджета
			/// </summary>
			public Decimal AppropriationFederal
			{
				get
				{
					Decimal result = 0;
					switch (mStage)
					{
						case TProgramActivityStage.Planned:
							{
								result += PriceFederal;
							}
							break;
						case TProgramActivityStage.Performed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].PriceFederal;
									}
								}
							}
							break;
						case TProgramActivityStage.Completed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].ClosureFederal;
									}
								}
							}
							break;
						case TProgramActivityStage.Canceled:
							break;
						case TProgramActivityStage.Notexecuted:
							break;
						default:
							break;
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].AppropriationFederal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Ассигнования для внебюджетных средств
			/// </summary>
			public Decimal AppropriationExtra
			{
				get
				{
					Decimal result = 0;
					switch (mStage)
					{
						case TProgramActivityStage.Planned:
							{
								result += PriceExtra;
							}
							break;
						case TProgramActivityStage.Performed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].PriceExtra;
									}
								}
							}
							break;
						case TProgramActivityStage.Completed:
							{
								for (Int32 i = 0; i < Contracts.Count; i++)
								{
									if (Contracts[i].NotCalculation == false)
									{
										result += Contracts[i].ClosureExtra;
									}
								}
							}
							break;
						case TProgramActivityStage.Canceled:
							break;
						case TProgramActivityStage.Notexecuted:
							break;
						default:
							break;
					}

					for (Int32 i = 0; i < Activities.Count; i++)
					{
						if (Activities[i].NotCalculation == false)
						{
							result += Activities[i].AppropriationExtra;
						}
					}

					return (result);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusBudgetFinancing ============================
			/// <summary>
			/// Финансирование
			/// </summary>
			[DisplayName("Цена")]
			[Description("Цена контракта")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[LotusCategoryOrder(3)]
			[XmlIgnore]
			public Decimal Price
			{
				get { return (mPriceLocal + mPriceRegional + mPriceFederal + mPriceExtra); }
			}

			/// <summary>
			/// Местный бюджет
			/// </summary>
			[DisplayName("Местный бюджет")]
			[Description("Местный бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceLocal
			{
				get { return (mPriceLocal); }
				set
				{
					mPriceLocal = value;
					NotifyPropertyChanged(PropertyArgsPriceLocal);
					RaiseBudgetChanged();
				}
			}

			/// <summary>
			/// Областной бюджет
			/// </summary>
			[DisplayName("Областной бюджет")]
			[Description("Областной бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceRegional
			{
				get { return (mPriceRegional); }
				set
				{
					mPriceRegional = value;
					NotifyPropertyChanged(PropertyArgsPriceRegional);
					RaiseBudgetChanged();
				}
			}

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			[DisplayName("Федеральный бюджет")]
			[Description("Федеральный бюджет")]
			[LotusNumberFormat(XNumbers.Monetary)]
			[Category(XInspectorGroupDesc.Financing)]
			[XmlAttribute]
			public Decimal PriceFederal
			{
				get { return (mPriceFederal); }
				set
				{
					mPriceFederal = value;
					NotifyPropertyChanged(PropertyArgsPriceFederal);
					RaiseBudgetChanged();
				}
			}

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			[DisplayName("Внебюджетные")]
			[Description("Внебюджетные средства")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceExtra
			{
				get { return (mPriceExtra); }
				set
				{
					mPriceExtra = value;
					NotifyPropertyChanged(PropertyArgsPriceExtra);
					RaiseBudgetChanged();
				}
			}
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

			#region ======================================= СВОЙСТВА ILotusSupportEditInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			public override String InspectorTypeName
			{
				get { return ("МЕРОПРИЯТИЕ"); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMunicipalProgramActivity()
				: this("Мероприятие")
			{
				mBeginDate = DateTime.Now;
				mEndDate = mBeginDate + TimeSpan.FromDays(100);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CMunicipalProgramActivity(String name)
				: base(name)
			{
				mBeginDate = DateTime.Now;
				mEndDate = mBeginDate + TimeSpan.FromDays(100);
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
			public Int32 CompareTo(CMunicipalProgramActivity other)
			{
				return (mName.CompareTo(other.Name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object Clone()
			{
				CMunicipalProgramActivity clone = new CMunicipalProgramActivity();
				clone.CopyParameters(this, null);
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
				return (mName);
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение финансирование контракта.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseBudgetChanged()
			{
				NotifyPropertyChanged(PropertyArgsPrice);
				//this.NotifyOwnerUpdated(nameof(Price));
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			/// <param name="parameters">Параметры копирования</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void CopyParameters(System.Object source_object, CParameters? parameters)
			{
				if (source_object is CMunicipalProgramActivity activity)
				{
					mStage = activity.Stage;
					mNumber = activity.Number;
					mDesc = activity.Desc;
					mGroup = activity.Group;
					mSubGroup = activity.SubGroup;
					mBeginDate = activity.BeginDate;
					mEndDate = activity.EndDate;

					PlanedValue = activity.PlanedValue;

					ProgramId = activity.ProgramId;
					SubProgramId = activity.SubProgramId;
					IndicatorId = activity.IndicatorId;
					ExecutorId = activity.ExecutorId;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion

			#region ======================================= МЕТОДЫ ОФОРМЛЕНИЯ =========================================
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для расширения функциональных возможностей типа <see cref="CMunicipalProgramActivity"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionMunicipalProgramActivity
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной программы
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="program_id">Идентификатор программы</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double GetPlannedValueForSubProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 sub_program_id, Int64 indicator_id)
			{
				Double result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false && 
								activity.SubProgramId == sub_program_id &&
								activity.IndicatorId == indicator_id)
							{
								result += activity.PlanedValue;
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной программы
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="program_id">Идентификатор программы</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double GetPlannedValueForSubProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 sub_program_id, Int64 indicator_id, Int32 year)
			{
				if(year == 2026)
				{
					return (activities.GetPlannedValueForSubProgram(sub_program_id, indicator_id));
				}

				Double result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.SubProgramId == sub_program_id &&
								activity.IndicatorId == indicator_id &&
								activity.BeginDate.Year == year)
							{
								result += activity.PlanedValue;
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной программы
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="program_id">Идентификатор программы</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAppropriationsForProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 program_id, TBudgetFinancing budget_financing)
			{
				Decimal result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.ProgramId == program_id)
							{
								switch (budget_financing)
								{
									case TBudgetFinancing.Common:
										{
											result += activity.Appropriation;
										}
										break;
									case TBudgetFinancing.Local:
										{
											result += activity.AppropriationLocal;
										}
										break;
									case TBudgetFinancing.Regional:
										{
											result += activity.AppropriationRegional;
										}
										break;
									case TBudgetFinancing.Federal:
										break;
									case TBudgetFinancing.Extra:
										break;
									default:
										break;
								}
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной программы за определённый год
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="program_id">Идентификатор программы</param>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAppropriationsForProgram(this IList<CMunicipalProgramActivity> activities,
				Int32 program_id, Int32 year, TBudgetFinancing budget_financing)
			{
				Decimal result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.BeginDate.Year == year &&
								activity.ProgramId == program_id)
							{
								switch (budget_financing)
								{
									case TBudgetFinancing.Common:
										{
											result += activity.Appropriation;
										}
										break;
									case TBudgetFinancing.Local:
										{
											result += activity.AppropriationLocal;
										}
										break;
									case TBudgetFinancing.Regional:
										{
											result += activity.AppropriationRegional;
										}
										break;
									case TBudgetFinancing.Federal:
										break;
									case TBudgetFinancing.Extra:
										break;
									default:
										break;
								}
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной программы за определённый год
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="program_id">Идентификатор программы</param>
			/// <param name="executor_id">Идентификатор ответсвенного исполнителя</param>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAppropriationsForProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 program_id, Int64 executor_id, Int32 year, TBudgetFinancing budget_financing)
			{
				Decimal result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.BeginDate.Year == year &&
								activity.ProgramId == program_id &&
								activity.ExecutorId == executor_id)
							{
								switch (budget_financing)
								{
									case TBudgetFinancing.Common:
										{
											result += activity.Appropriation;
										}
										break;
									case TBudgetFinancing.Local:
										{
											result += activity.AppropriationLocal;
										}
										break;
									case TBudgetFinancing.Regional:
										{
											result += activity.AppropriationRegional;
										}
										break;
									case TBudgetFinancing.Federal:
										break;
									case TBudgetFinancing.Extra:
										break;
									default:
										break;
								}
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной подпрограммы
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="sub_program_id">Идентификатор подпрограммы</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAppropriationsForSubProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 sub_program_id, TBudgetFinancing budget_financing)
			{
				Decimal result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.SubProgramId == sub_program_id)
							{
								switch (budget_financing)
								{
									case TBudgetFinancing.Common:
										{
											result += activity.Appropriation;
										}
										break;
									case TBudgetFinancing.Local:
										{
											result += activity.AppropriationLocal;
										}
										break;
									case TBudgetFinancing.Regional:
										{
											result += activity.AppropriationRegional;
										}
										break;
									case TBudgetFinancing.Federal:
										break;
									case TBudgetFinancing.Extra:
										break;
									default:
										break;
								}
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной подпрограммы за определённый год
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="sub_program_id">Идентификатор подпрограммы</param>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAppropriationsForSubProgram(this IList<CMunicipalProgramActivity> activities, 
				Int64 sub_program_id, Int32 year, TBudgetFinancing budget_financing)
			{
				Decimal result = 0;

				if(activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if(activity != null)
						{
							if(activity.NotCalculation == false &&
								activity.BeginDate.Year == year &&
								activity.SubProgramId == sub_program_id)
							{
								switch (budget_financing)
								{
									case TBudgetFinancing.Common:
										{
											result += activity.Appropriation;
										}
										break;
									case TBudgetFinancing.Local:
										{
											result += activity.AppropriationLocal;
										}
										break;
									case TBudgetFinancing.Regional:
										{
											result += activity.AppropriationRegional;
										}
										break;
									case TBudgetFinancing.Federal:
										break;
									case TBudgetFinancing.Extra:
										break;
									default:
										break;
								}
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной подпрограммы за определённый год
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="sub_program_id">Идентификатор подпрограммы</param>
			/// <param name="executor_id">Идентификатор ответсвенного исполнителя</param>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAppropriationsForSubProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 sub_program_id, Int64 executor_id, Int32 year, TBudgetFinancing budget_financing)
			{
				Decimal result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.BeginDate.Year == year &&
								activity.SubProgramId == sub_program_id &&
								activity.ExecutorId == executor_id)
							{
								switch (budget_financing)
								{
									case TBudgetFinancing.Common:
										{
											result += activity.Appropriation;
										}
										break;
									case TBudgetFinancing.Local:
										{
											result += activity.AppropriationLocal;
										}
										break;
									case TBudgetFinancing.Regional:
										{
											result += activity.AppropriationRegional;
										}
										break;
									case TBudgetFinancing.Federal:
										break;
									case TBudgetFinancing.Extra:
										break;
									default:
										break;
								}
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ассигнований для указанной подпрограммы за определённый год
			/// </summary>
			/// <param name="activities">Список мероприятий</param>
			/// <param name="sub_program_id">Идентификатор подпрограммы</param>
			/// <param name="executor_id">Идентификатор ответсвенного исполнителя</param>
			/// <param name="year">Год</param>
			/// <param name="budget_financing">Тип уровня бюджета</param>
			/// <returns>Ассигнования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double GetIndicatorsValueForProgram(this IList<CMunicipalProgramActivity> activities,
				Int64 program_id, Int64 indicator_id)
			{
				Double result = 0;

				if (activities != null)
				{
					for (Int32 i = 0; i < activities.Count; i++)
					{
						CMunicipalProgramActivity activity = activities[i];
						if (activity != null)
						{
							if (activity.NotCalculation == false &&
								activity.ProgramId == program_id &&
								activity.IndicatorId == indicator_id)
							{
								result += activity.PlanedValue;
							}
						}
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// 
			/// </summary>
			/// <param name="activities"></param>
			/// <param name="name"></param>
			/// <param name="year"></param>
			/// <param name="indicator_id"></param>
			/// <param name="index"></param>
			/// <returns></returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMunicipalProgramActivity Find(this IList<CMunicipalProgramActivity> activities, String name,
				Int32 year, Int64 indicator_id, out Int32 index)
			{
				for (Int32 i = 0; i < activities.Count; i++)
				{
					if (activities[i].Name == name &&
						activities[i].BeginDate.Year == year &&
						activities[i].IndicatorId == indicator_id)
					{
						index = i;
						return (activities[i]);
					}
				}

				index = -1;
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// 
			/// </summary>
			/// <param name="activities"></param>
			/// <param name="name"></param>
			/// <param name="year"></param>
			/// <param name="executor_id"></param>
			/// <param name="indicator_id"></param>
			/// <param name="index"></param>
			/// <returns></returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMunicipalProgramActivity Find(this IList<CMunicipalProgramActivity> activities, String name,
				Int32 year, Int64? executor_id, Int64 indicator_id, out Int32 index)
			{
				for (Int32 i = 0; i < activities.Count; i++)
				{
					if (activities[i].Name == name &&
						activities[i].ExecutorId == executor_id &&
						activities[i].BeginDate.Year == year &&
						activities[i].IndicatorId == indicator_id)
					{
						index = i;
						return (activities[i]);
					}
				}

				index = -1;
				return (null);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================