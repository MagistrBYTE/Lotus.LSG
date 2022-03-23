//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема исполнения контракта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGCertificateCompletion.cs
*		Акт выполненных работ - документ подтверждающий выполненные работы.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
		//! \addtogroup MunicipalityBaseContract
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Акт выполненных работ - документ подтверждающий выполненные работы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[Table("certificate_completion")]
		public class CCertificateCompletion : CNameableId, IComparable<CCertificateCompletion>, ILotusCopyParameters, 
			ILotusBudgetFinancing, ILotusNotCalculation, ILotusVerified
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static readonly PropertyChangedEventArgs PropertyArgsGroup = new PropertyChangedEventArgs(nameof(Group));
			protected static readonly PropertyChangedEventArgs PropertyArgsValue = new PropertyChangedEventArgs(nameof(Value));
			protected static readonly PropertyChangedEventArgs PropertyArgsValueUnit = new PropertyChangedEventArgs(nameof(ValueUnit));

			protected static readonly PropertyChangedEventArgs PropertyArgsBeginPeriodDate = new PropertyChangedEventArgs(nameof(BeginPeriodDate));
			protected static readonly PropertyChangedEventArgs PropertyArgsEndPeriodDate = new PropertyChangedEventArgs(nameof(EndPeriodDate));
			protected static readonly PropertyChangedEventArgs PropertyArgsСlosingDate = new PropertyChangedEventArgs(nameof(ClosingDate));

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
			/// Конфигурирование модели для типа <see cref="CCertificateCompletion"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CCertificateCompletion>();
				model.ToTable("certificate_completion");
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

				var property_number = model.Property(vs => vs.Number);
				property_number.HasColumnName("number");
				property_number.HasMaxLength(10);

				var property_group = model.Property(vs => vs.Group);
				property_group.HasColumnName("group");
				property_group.HasMaxLength(20);

				var property_planed_value = model.Property(vs => vs.Value);
				property_planed_value.HasColumnName("value");

				var property_value_unit = model.Property(vs => vs.ValueUnit);
				property_value_unit.HasColumnName("value_unit");

				var property_begin_period = model.Property(vs => vs.BeginPeriodDate);
				property_begin_period.HasColumnName("begin_period");

				var property_end_period = model.Property(vs => vs.EndPeriodDate);
				property_end_period.HasColumnName("end_period");

				model.Ignore(vs => vs.Price);
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

				var property_contract_id = model.Property(vs => vs.ContractId);
				property_contract_id.HasColumnName("contract_id");

				var property_not_calculation = model.Property(vs => vs.NotCalculation);
				property_not_calculation.HasColumnName("not_calc");

				var property_verified = model.Property(vs => vs.IsVerified);
				property_verified.HasColumnName("verified");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mNumber;
			protected internal String mGroup;
			protected internal Double mValue;
			protected internal String mValueUnit;

			// Сроки
			protected internal DateTime mBeginPeriodDate = DateTime.UtcNow;
			protected internal DateTime mEndPeriodDate = DateTime.UtcNow;
			protected internal DateTime mClosingDate = DateTime.UtcNow;

			// Финансы
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
			/// Условный номер акта
			/// </summary>
			[DisplayName("Номер")]
			[Description("Условный номер акта")]
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
			/// Группирование актов
			/// </summary>
			[DisplayName("Группа")]
			[Description("Группирование актов")]
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
			/// Значение
			/// </summary>
			[DisplayName("Значение")]
			[Description("Значение")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(4)]
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
			[Category(XInspectorGroupDesc.Params)]
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
			// СРОКИ
			//
			/// <summary>
			/// Дата начала периода
			/// </summary>
			[DisplayName("Начало периода")]
			[Description("Дата начала периода")]
			[Category(XInspectorGroupDesc.Date)]
			[LotusPropertyOrder(0)]
			[LotusCategoryOrder(2)]
			public DateTime? BeginPeriodDate
			{
				get { return (mBeginPeriodDate); }
				set
				{
					mBeginPeriodDate = value ?? DateTime.Now;
					NotifyPropertyChanged(PropertyArgsBeginPeriodDate);
				}
			}

			/// <summary>
			/// Дата окончания периода
			/// </summary>
			[DisplayName("Окончание периода")]
			[Description("Дата окончания периода")]
			[Category(XInspectorGroupDesc.Date)]
			[LotusPropertyOrder(1)]
			public DateTime? EndPeriodDate
			{
				get { return (mEndPeriodDate); }
				set
				{
					mEndPeriodDate = value ?? DateTime.Now;
					NotifyPropertyChanged(PropertyArgsEndPeriodDate);
				}
			}

			/// <summary>
			/// Дата закрытия акта
			/// </summary>
			[DisplayName("Дата закрытия")]
			[Description("Дата закрытия акта")]
			[Category(XInspectorGroupDesc.Date)]
			[LotusPropertyOrder(2)]
			public DateTime? ClosingDate
			{
				get { return (mClosingDate); }
				set
				{
					mClosingDate = value ?? DateTime.Now;
					NotifyPropertyChanged(PropertyArgsСlosingDate);
				}
			}

			//
			// КОНТРАКТ
			//
			/// <summary>
			/// Идентификатор контракта
			/// </summary>
			[Browsable(false)]
			public Int64? ContractId { get; set; }

			/// <summary>
			/// Контракт
			/// </summary>
			[ForeignKey(nameof(ContractId))]
			public CContract? CContract { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА ILotusBudgetFinancing ============================
			//
			// ФИНАНСИРОВАНИЕ
			//
			/// <summary>
			/// Общая цена закрытия
			/// </summary>
			[DisplayName("Цена")]
			[Description("Общая цена закрытия")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[LotusCategoryOrder(3)]
			[XmlIgnore]
			[NotMapped]
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
				get { return ("АКТ"); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CCertificateCompletion()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CCertificateCompletion(String name)
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
			public Int32 CompareTo(CCertificateCompletion other)
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
				CCertificateCompletion clone = new CCertificateCompletion();
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
			/// Изменение финансирование акта.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseBudgetChanged()
			{
				NotifyPropertyChanged(PropertyArgsPrice);
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
				if (source_object is CCertificateCompletion completion)
				{
					Number = completion.mNumber;
					Group = completion.mGroup;

					Value = completion.Value;
					ValueUnit = completion.ValueUnit;

					BeginPeriodDate = completion.BeginPeriodDate;
					EndPeriodDate = completion.EndPeriodDate;
					ClosingDate = completion.EndPeriodDate;

					PriceLocal = completion.PriceLocal;
					PriceRegional = completion.PriceRegional;
					PriceFederal = completion.PriceFederal;
					PriceExtra = completion.PriceExtra;
				}
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
