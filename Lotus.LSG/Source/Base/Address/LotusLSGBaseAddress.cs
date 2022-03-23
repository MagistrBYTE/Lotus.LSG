//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема адресного хозяйства
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseAddress.cs
*		Класс для определения информации о структуре адреса или местоположения объекта.
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
		//! \defgroup MunicipalityBaseAddress Подсистема адресного хозяйства
		//! Подсистема адресного хозяйства обеспечивает идентификацию объектов на основе адреса и местоположения. 
		//! Основана на ФИАС.
		//! \ingroup MunicipalityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип адресуемого элемента
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TAddressElementType
		{
			/// <summary>
			/// Земельный участок
			/// </summary>
			Landplot = 0,

			/// <summary>
			/// Объект капитального строительства
			/// </summary>
			Building = 1
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения информации о структуре адреса или местоположении объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CAddressElement : CIdentifierId, IComparable<CAddressElement>, ILotusSupportViewInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsElementType = new PropertyChangedEventArgs(nameof(ElementType));
			protected static readonly PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static readonly PropertyChangedEventArgs PropertyArgsCadastralNumber = new PropertyChangedEventArgs(nameof(CadastralNumber));
			protected static readonly PropertyChangedEventArgs PropertyArgsCode = new PropertyChangedEventArgs(nameof(Code));
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CAddressElement"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CAddressElement>();
				model.ToTable("address_item");
				model.HasKey(vs => vs.Id);
				model.HasIndex(vs => vs.Id).IsUnique();
				model.Ignore(vs => vs.InspectorObjectName);
				model.Ignore(vs => vs.InspectorTypeName);

				var property_number = model.Property(vs => vs.Number);
				property_number.HasColumnName("number");
				property_number.HasMaxLength(20);
				property_number.IsRequired();

				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");

				var property_element_type = model.Property(vs => vs.ElementType);
				property_element_type.HasColumnName("element_type");

				var property_cadastral_number = model.Property(vs => vs.CadastralNumber);
				property_cadastral_number.HasColumnName("cadastral_number");

				var property_сode = model.Property(vs => vs.Code);
				property_сode.HasColumnName("сode");

				var property_street_id = model.Property(vs => vs.StreetId);
				property_street_id.HasColumnName("street_id");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TAddressElementType mElementType;
			internal String mNumber;
			internal String mCadastralNumber;
			internal String mCode;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип адресуемого элемента
			/// </summary>
			[DisplayName("Тип элемента")]
			[Description("Тип адресуемого элемента")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(1)]
			[XmlAttribute]
			public TAddressElementType ElementType
			{
				get { return (mElementType); }
				set
				{
					mElementType = value;
					NotifyPropertyChanged(PropertyArgsElementType);
				}
			}

			/// <summary>
			/// Номер адресуемого элемента
			/// </summary>
			[DisplayName("Номер")]
			[Description("Номер адресуемого элемента")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(2)]
			[XmlAttribute]
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
			/// Кадастровый номер
			/// </summary>
			[DisplayName("Кадастровый номер")]
			[Description("Кадастровый номер")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(5)]
			[XmlAttribute]
			public String CadastralNumber
			{
				get { return (mCadastralNumber); }
				set
				{
					mCadastralNumber = value;
					NotifyPropertyChanged(PropertyArgsCadastralNumber);
				}
			}

			/// <summary>
			/// Уникальный номер адреса
			/// </summary>
			[DisplayName("Уникальный номер адреса")]
			[Description("Уникальный номер адреса объекта адресации в государственном адресном реестре")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(5)]
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
			/// Внешний ключ для улицы
			/// </summary>
			[Browsable(false)]
			public Int64 StreetId { get; set; }

			/// <summary>
			/// Навигационное свойство для улицы
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(StreetId))]
			public CAddressStreet Street { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportViewInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public String InspectorTypeName
			{
				get { return ("АДРЕС"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public String InspectorObjectName
			{
				get
				{
					return (mNumber);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CAddressElement()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="number">Номер адресуемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public CAddressElement(String number)
			{
				mNumber = number;
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
			public Int32 CompareTo(CAddressElement other)
			{
				return (mNumber.CompareTo(other));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (InspectorObjectName);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
