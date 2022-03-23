//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема адресного хозяйства
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseAddressStreet.cs
*		Класс представляющий собой компонент адреса - элемент улицы в населённом пункте или местоположение 
*	вне границ населённого пункта.
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
		//! \addtogroup MunicipalityBaseAddress
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип улицы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TAddressStreetType
		{
			/// <summary>
			/// Тип улицы отсутствует
			/// </summary>
			None = -1,

			/// <summary>
			/// Улица
			/// </summary>
			Street = 0,

			/// <summary>
			/// Переулок
			/// </summary>
			Lane = 1,

			/// <summary>
			/// Микрорайон
			/// </summary>
			Microdistrict = 2
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой компонент адреса - элемент улицы в населённом пункте или местоположение 
		/// вне границ населённого пункта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CAddressStreet : CNameableId, IComparable<CAddressStreet>, ILotusSupportViewInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsStreetType = new PropertyChangedEventArgs(nameof(StreetType));
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CAddressStreet"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CAddressStreet>();
				model.ToTable("address_street");
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

				var property_street_type = model.Property(vs => vs.StreetType);
				property_street_type.HasColumnName("street_type");

				var property_village_id = model.Property(vs => vs.VillageId);
				property_village_id.HasColumnName("village_id");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TAddressStreetType mStreetType;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Краткое наименование
			/// </summary>
			[DisplayName("Тип улицы")]
			[Description("Тип улицы")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(2)]
			[XmlAttribute]
			public TAddressStreetType StreetType
			{
				get { return (mStreetType); }
				set
				{
					mStreetType = value;
					NotifyPropertyChanged(PropertyArgsStreetType);
				}
			}


			/// <summary>
			/// Внешний ключ для населённого пункта
			/// </summary>
			[Browsable(false)]
			public Int64 VillageId { get; set; }

			/// <summary>
			/// Навигационное свойство населённого пункта
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(VillageId))]
			public CAddressVillage Village { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportViewInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorTypeName
			{
				get { return ("УЛИЦА"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorObjectName
			{
				get
				{
					return (mName);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CAddressStreet()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование улицы</param>
			//---------------------------------------------------------------------------------------------------------
			public CAddressStreet(String name)
				: base(name)
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
			public Int32 CompareTo(CAddressStreet other)
			{
				return (Name.CompareTo(other));
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
