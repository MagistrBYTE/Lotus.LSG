//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема представления контрактов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGContract.cs
*		Контракт.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
		//! \addtogroup MunicipalityBaseContract
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Контракт
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CContract : CNameableId, IComparable<CNameableId>, ILotusContractData, ILotusCopyParameters,
			ILotusSupportEditInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsSubject = new PropertyChangedEventArgs(nameof(Subject));
			protected static readonly PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static readonly PropertyChangedEventArgs PropertyArgsGroup = new PropertyChangedEventArgs(nameof(Group));
			protected static readonly PropertyChangedEventArgs PropertyArgsSubGroup = new PropertyChangedEventArgs(nameof(SubGroup));
			protected static readonly PropertyChangedEventArgs PropertyArgsValue = new PropertyChangedEventArgs(nameof(Value));
			protected static readonly PropertyChangedEventArgs PropertyArgsValueUnit = new PropertyChangedEventArgs(nameof(ValueUnit));

			protected static readonly PropertyChangedEventArgs PropertyArgsDateOfConclusion = new PropertyChangedEventArgs(nameof(DateOfConclusion));
			protected static readonly PropertyChangedEventArgs PropertyArgsDeadline = new PropertyChangedEventArgs(nameof(Deadline));
			protected static readonly PropertyChangedEventArgs PropertyArgsDateOfClose = new PropertyChangedEventArgs(nameof(DateOfClose));

			protected static readonly PropertyChangedEventArgs PropertyArgsPrice = new PropertyChangedEventArgs(nameof(Price));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceLocal = new PropertyChangedEventArgs(nameof(PriceLocal));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceRegional = new PropertyChangedEventArgs(nameof(PriceRegional));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceFederal = new PropertyChangedEventArgs(nameof(PriceFederal));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceExtra = new PropertyChangedEventArgs(nameof(PriceExtra));

			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVerified = new PropertyChangedEventArgs(nameof(IsVerified));

			/// <summary>
			/// Описание свойств
			/// </summary>
			public readonly static CPropertyDesc[] ContractPropertiesDesc = new CPropertyDesc[]
			{
				// Идентификация
				CPropertyDesc.OverrideDisplayNameAndDescription<CContract>(nameof(Name), "Наименование", "Наименование контракта"),
			};
			#endregion
#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CContract"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CContract>();
				model.ToTable("contract");
				model.HasKey(vs => vs.Id);
				model.HasIndex(vs => vs.Id).IsUnique();
				model.Ignore(vs => vs.InspectorObjectName);
				model.Ignore(vs => vs.InspectorTypeName);

				var property_name = model.Property(vs => vs.Name);
				property_name.HasColumnName("names");
				property_name.HasMaxLength(40);
				property_name.IsRequired();

				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");

				var property_subject = model.Property(vs => vs.Subject);
				property_subject.HasColumnName("subject");
				property_subject.HasMaxLength(400);

				var property_number = model.Property(vs => vs.Number);
				property_number.HasColumnName("number");
				property_number.HasMaxLength(10);

				var property_group = model.Property(vs => vs.Group);
				property_group.HasColumnName("group");
				property_group.HasMaxLength(20);

				var property_subgroup = model.Property(vs => vs.SubGroup);
				property_subgroup.HasColumnName("subgroup");
				property_subgroup.HasMaxLength(20);

				var property_value = model.Property(vs => vs.Value);
				property_value.HasColumnName("value");

				var property_value_unit = model.Property(vs => vs.ValueUnit);
				property_value_unit.HasColumnName("value_unit");

				var property_date_conclusion = model.Property(vs => vs.DateOfConclusion);
				property_date_conclusion.HasColumnName("date_conclusion");

				var property_deadline = model.Property(vs => vs.Deadline);
				property_deadline.HasColumnName("deadline");

				var property_customer_id = model.Property(vs => vs.CustomerId);
				property_customer_id.HasColumnName("customer_id");

				var property_contractor_id = model.Property(vs => vs.ContractorId);
				property_contractor_id.HasColumnName("contractor_id");

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

				model.Ignore(vs => vs.CustomerName);
				model.Ignore(vs => vs.ContractorName);
				model.Ignore(vs => vs.DateOfClose);
				model.Ignore(vs => vs.Price);
				model.Ignore(vs => vs.Closure);
				model.Ignore(vs => vs.ClosureLocal);
				model.Ignore(vs => vs.ClosureRegional);
				model.Ignore(vs => vs.ClosureFederal);
				model.Ignore(vs => vs.ClosureExtra);
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mSubject;
			protected internal String mNumber;
			protected internal String mGroup;
			protected internal String mSubGroup;
			protected internal Double mValue;
			protected internal String mValueUnit;

			// Сроки контракта
			protected internal DateTime mDateOfConclusion = DateTime.Now;
			protected internal Int32 mDeadline;

			// Финансирование
			protected internal Decimal mPriceLocal;
			protected internal Decimal mPriceRegional;
			protected internal Decimal mPriceFederal;
			protected internal Decimal mPriceExtra;

			// Расчеты
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsVerified;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Предмет контракта
			/// </summary>
			[DisplayName("Предмет")]
			[Description("Предмет контракта")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			[LotusCategoryOrder(1)]
			[XmlAttribute]
			public String? Subject
			{
				get { return (mSubject); }
				set
				{
					mSubject = value;
					NotifyPropertyChanged(PropertyArgsSubject);
				}
			}

			/// <summary>
			/// Условный номер контракта
			/// </summary>
			[DisplayName("Номер")]
			[Description("Условный номер контракта")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(2)]
			[XmlAttribute]
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
			/// Основная группа контрактов
			/// </summary>
			[DisplayName("Группа")]
			[Description("Основная группа контрактов")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(3)]
			[XmlAttribute]
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
			/// Подгруппа контрактов
			/// </summary>
			[DisplayName("Подгруппа")]
			[Description("Подгруппа контрактов")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(4)]
			[XmlAttribute]
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
			/// Основной показатель целевого индикатора
			/// </summary>
			[DisplayName("Цель")]
			[Description("Основной показатель целевого индикатора")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(5)]
			[XmlAttribute]
			public Double? Value
			{
				get { return (mValue); }
				set
				{
					mValue = value ?? 1;
					NotifyPropertyChanged(PropertyArgsValue);
				}
			}

			/// <summary>
			/// Единица измерения целевого показателя
			/// </summary>
			[DisplayName("Единица измерения")]
			[Description("Единица измерения целевого показателя")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(7)]
			[XmlAttribute]
			public String? ValueUnit
			{
				get { return (mValueUnit); }
				set
				{
					mValueUnit = value;
					NotifyPropertyChanged(PropertyArgsValueUnit);
				}
			}

			//
			// СРОКИ КОНТРАКТА
			//
			/// <summary>
			/// Дата заключения контракта
			/// </summary>
			[DisplayName("Дата заключения")]
			[Description("Дата заключения контракта")]
			[Category(XInspectorGroupDesc.Date)]
			[LotusPropertyOrder(0)]
			[LotusCategoryOrder(2)]
			public DateTime? DateOfConclusion
			{
				get { return (mDateOfConclusion); }
				set
				{
					mDateOfConclusion = value ?? DateTime.Now;
					NotifyPropertyChanged(PropertyArgsDateOfConclusion);
					NotifyPropertyChanged(PropertyArgsDateOfClose);
				}
			}

			/// <summary>
			/// Сроки выполнения работ
			/// </summary>
			[DisplayName("Срок, дн")]
			[Description("Сроки выполнения работ")]
			[Category(XInspectorGroupDesc.Date)]
			[LotusPropertyOrder(1)]
			public Int32? Deadline
			{
				get { return (mDeadline); }
				set
				{
					mDeadline = value ?? 100;
					NotifyPropertyChanged(PropertyArgsDeadline);
					NotifyPropertyChanged(PropertyArgsDateOfClose);
				}
			}

			/// <summary>
			/// Сроки закрытия работ
			/// </summary>
			[DisplayName("Дата закрытия")]
			[Description("Сроки закрытия работ")]
			[Category(XInspectorGroupDesc.Date)]
			[LotusPropertyOrder(2)]
			public DateTime? DateOfClose
			{
				get { return (mDateOfConclusion + TimeSpan.FromDays(Deadline ?? 100)); }
				set
				{
					mDeadline = (value - DateOfConclusion).GetValueOrDefault().Days;
					NotifyPropertyChanged(PropertyArgsDeadline);
					NotifyPropertyChanged(PropertyArgsDateOfClose);
				}
			}

			//
			// ЗАКАЗЧИК
			//
			/// <summary>
			/// Идентификатор заказчика
			/// </summary>
			[Browsable(false)]
			public Int64? CustomerId { get; set; }

			/// <summary>
			/// Заказчик
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(CustomerId))]
			public CSubjectCivil? Customer
			{
				get;
				set;
			}

			/// <summary>
			/// Имя заказчика
			/// </summary>
			[DisplayName("Заказчик")]
			[Description("Заказчик")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(5)]
			public String CustomerName
			{
				get
				{
					if (Customer == null)
					{
						return ("");
					}
					else
					{
						return (Customer.Name);
					}
				}
			}

			//
			// ПОДРЯДЧИК
			//
			/// <summary>
			/// Идентификатор подрядчика
			/// </summary>
			[Browsable(false)]
			public Int64? ContractorId { get; set; }

			/// <summary>
			/// Подрядчик
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(ContractorId))]
			public CSubjectCivil? Contractor { get; set; }

			/// <summary>
			/// Имя заказчика
			/// </summary>
			[DisplayName("Подрядчик")]
			[Description("Подрядчик")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(6)]
			public String ContractorName
			{
				get
				{
					if(Contractor == null)
					{
						return ("");
					}
					else
					{
						return (Contractor.Name);
					}
				}
			}

			//
			// ЗАКРЫТИЕ КОНТРАКТА
			//
			/// <summary>
			/// Финансирование
			/// </summary>
			[DisplayName("Цена")]
			[Description("Цена контракта")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[LotusCategoryOrder(3)]
			[XmlIgnore]
			public Decimal Closure
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < CertificateCompletions.Count; i++)
					{
						if (CertificateCompletions[i] != null && CertificateCompletions[i].NotCalculation == false)
						{
							result += CertificateCompletions[i].Price;
						}
					}
					return result;
				}
			}

			/// <summary>
			/// Местный бюджет
			/// </summary>
			[DisplayName("Местный бюджет")]
			[Description("Местный бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal ClosureLocal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < CertificateCompletions.Count; i++)
					{
						if (CertificateCompletions[i] != null && CertificateCompletions[i].NotCalculation == false)
						{
							result += CertificateCompletions[i].PriceLocal;
						}
					}
					return result;
				}
			}

			/// <summary>
			/// Областной бюджет
			/// </summary>
			[DisplayName("Областной бюджет")]
			[Description("Областной бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal ClosureRegional
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < CertificateCompletions.Count; i++)
					{
						if (CertificateCompletions[i] != null && CertificateCompletions[i].NotCalculation == false)
						{
							result += CertificateCompletions[i].PriceRegional;
						}
					}
					return result;
				}
			}

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			[DisplayName("Федеральный бюджет")]
			[Description("Федеральный бюджет")]
			[LotusNumberFormat(XNumbers.Monetary)]
			[Category(XInspectorGroupDesc.Financing)]
			[XmlIgnore]
			public Decimal ClosureFederal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < CertificateCompletions.Count; i++)
					{
						if (CertificateCompletions[i] != null && CertificateCompletions[i].NotCalculation == false)
						{
							result += CertificateCompletions[i].PriceFederal;
						}
					}
					return result;
				}
			}

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			[DisplayName("Внебюджетные")]
			[Description("Внебюджетные средства")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal ClosureExtra
			{
				get 
				{
					Decimal result = 0;
                    for (Int32 i = 0; i < CertificateCompletions.Count; i++)
                    {
						if(CertificateCompletions[i] != null && CertificateCompletions[i].NotCalculation == false)
                        {
							result += CertificateCompletions[i].PriceExtra;
						}
                    }
					return result;
				}
			}

			/// <summary>
			/// Список актов выполненных работ
			/// </summary>
			[Browsable(false)]
			public virtual IList<CCertificateCompletion> CertificateCompletions { get; set; } = new List<CCertificateCompletion>();
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
			//
			// ПОДДЕРЖКА ИНСПЕКТОРА СВОЙСТВ
			//
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorTypeName
			{
				get { return ("КОНТРАКТ"); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CContract()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CContract(String name)
				: this()
			{
				mName = name;
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
			public Int32 CompareTo(CContract other)
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
				CContract clone = new CContract();
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
			//---------------------------------------------------------------------------------------------------------
			public virtual void CopyParameters(System.Object source_object, CParameters? parameters)
			{
				if (source_object is CContract contract)
				{
					Subject = contract.Subject;
					Number = contract.Number;
					Group = contract.Group;
					SubGroup = contract.SubGroup;

					DateOfConclusion = contract.DateOfConclusion;
					Deadline = contract.Deadline;

					Value = contract.Value;
					ValueUnit = contract.ValueUnit;

					PriceLocal = contract.mPriceLocal;
					PriceRegional = contract.mPriceRegional;
					PriceFederal = contract.mPriceFederal;
					PriceExtra = contract.mPriceExtra;

					CustomerId = contract.CustomerId;
					ContractorId = contract.ContractorId;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSupportEditInspector =========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить массив описателей свойств объекта
			/// </summary>
			/// <returns>Массив описателей</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CPropertyDesc[] GetPropertiesDesc()
			{
				return (ContractPropertiesDesc);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Закрытие контракта по его цене
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ClosureFromPrice()
			{
				//mClosureLocal = mPriceLocal;
				//mClosureRegional = mPriceRegional;
				//mClosureFederal = mPriceFederal;
				//mClosureExtra = mPriceExtra;
				//mClosureValue = mValue;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
