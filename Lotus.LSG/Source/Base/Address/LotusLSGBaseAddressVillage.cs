//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема адресного хозяйства
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseAddressVillage.cs
*		Класс представляющий собой компонент адреса - населённый пункт.
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
		/// Тип населенного пункта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TAddressVillageType
		{
			/// <summary>
			/// Город
			/// </summary>
			Town = 0,

			/// <summary>
			/// Поселок
			/// </summary>
			Township = 1,

			/// <summary>
			/// Село
			/// </summary>
			Village = 2,

			/// <summary>
			/// Железнодорожный разъезд
			/// </summary>
			RailwaySiding = 3
		}


		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой компонент адреса - населённый пункт
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CAddressVillage : CNameableId, IComparable<CAddressVillage>, ILotusSupportViewInspector
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Андреевский
			/// </summary>
			public static readonly CAddressVillage Andreevsky = new CAddressVillage(CAddressVillageSettlement.Andreyevskoye.Id, 100, "Андреевский");

			/// <summary>
			/// Мариинский
			/// </summary>
			public static readonly CAddressVillage Mariinskiy = new CAddressVillage(CAddressVillageSettlement.Andreyevskoye.Id, 101, "Мариинский");

			/// <summary>
			/// Атамановский
			/// </summary>
			public static readonly CAddressVillage Atamanovskiy = new CAddressVillage(CAddressVillageSettlement.Atamanovskoye.Id, 200, "Атамановский");

			/// <summary>
			/// Степной
			/// </summary>
			public static readonly CAddressVillage Stepnoy = new CAddressVillage(CAddressVillageSettlement.Atamanovskoye.Id, 201, "Степной");
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsVillageType = new PropertyChangedEventArgs(nameof(VillageType));
			protected static readonly PropertyChangedEventArgs PropertyArgsOKTMO = new PropertyChangedEventArgs(nameof(OKTMO));
			protected static readonly PropertyChangedEventArgs PropertyArgsOKATO = new PropertyChangedEventArgs(nameof(OKATO));
			protected static readonly PropertyChangedEventArgs PropertyArgsPostalCode = new PropertyChangedEventArgs(nameof(PostalCode));
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CAddressVillage"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CAddressVillage>();
				model.ToTable("address_village");
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
				property_id.ValueGeneratedNever();

				var property_village_type = model.Property(vs => vs.VillageType);
				property_village_type.HasColumnName("village_type");

				var property_oktmo = model.Property(vs => vs.OKTMO);
				property_oktmo.HasColumnName("oktmo");

				var property_okato = model.Property(vs => vs.OKATO);
				property_okato.HasColumnName("okato");

				var property_village_settlement_id = model.Property(vs => vs.VillageSettlementId);
				property_village_settlement_id.HasColumnName("village_sett_id");

				// Данные
				model.HasData(Andreevsky,
					Mariinskiy,
					Atamanovskiy,
					Stepnoy);
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TAddressVillageType mVillageType;
			internal String mOKTMO;
			internal String mOKATO;
			internal String mPostalCode;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип населённого пункта
			/// </summary>
			[DisplayName("Тип населённого пункта")]
			[Description("Тип населённого пункта")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(3)]
			[XmlAttribute]
			public TAddressVillageType VillageType
			{
				get { return (mVillageType); }
				set
				{
					mVillageType = value;
					NotifyPropertyChanged(PropertyArgsVillageType);
				}
			}

			/// <summary>
			/// Код ОКТМО
			/// </summary>
			[DisplayName("Код ОКТМО")]
			[Description("Код ОКТМО")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(4)]
			[XmlAttribute]
			public String? OKTMO
			{
				get { return (mOKTMO); }
				set
				{
					mOKTMO = value;
					NotifyPropertyChanged(PropertyArgsOKTMO);
				}
			}

			/// <summary>
			/// Код ОКАТО
			/// </summary>
			[DisplayName("Код ОКАТО")]
			[Description("Код ОКАТО")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(5)]
			[XmlAttribute]
			public String? OKATO
			{
				get { return (mOKATO); }
				set
				{
					mOKATO = value;
					NotifyPropertyChanged(PropertyArgsOKATO);
				}
			}

			/// <summary>
			/// Почтовый индекс
			/// </summary>
			[DisplayName("Почтовый индекс")]
			[Description("Почтовый индекс")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(5)]
			[XmlAttribute]
			public String? PostalCode
			{
				get { return (mPostalCode); }
				set
				{
					mPostalCode = value;
					NotifyPropertyChanged(PropertyArgsPostalCode);
				}
			}

			/// <summary>
			/// Внешний ключ для сельского поселения
			/// </summary>
			[Browsable(false)]
			public Int64 VillageSettlementId { get; set; }

			/// <summary>
			/// Навигационное свойство сельского поселения
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(VillageSettlementId))]
			public CAddressVillageSettlement VillageSettlement { get; set; }

			/// <summary>
			/// Список улиц
			/// </summary>
			[Browsable(false)]
			public List<CAddressStreet> Streets { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportViewInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorTypeName
			{
				get { return ("НАСЕЛЁННЫЙ ПУНКТ"); }
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
			public CAddressVillage()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование населённого пункта</param>
			//---------------------------------------------------------------------------------------------------------
			public CAddressVillage(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="village_settlement_id">Внешний ключ для сельского поселения</param>
			/// <param name="name">Наименование населённого пункта</param>
			//---------------------------------------------------------------------------------------------------------
			public CAddressVillage(Int64 village_settlement_id, String name)
				: base(name)
			{
				VillageSettlementId = village_settlement_id;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="village_settlement_id">Внешний ключ для сельского поселения</param>
			/// <param name="id">Индекс(ключ) населённого пункта</param>
			/// <param name="name">Наименование населённого пункта</param>
			//---------------------------------------------------------------------------------------------------------
			public CAddressVillage(Int64 village_settlement_id, Int32 id, String name)
				: base(name)
			{
				VillageSettlementId = village_settlement_id;
				mId = id;
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
			public Int32 CompareTo(CAddressVillage other)
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
