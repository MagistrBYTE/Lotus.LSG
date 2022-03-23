//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема адресного хозяйства
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseAddressVillageSettlement.cs
*		Класс представляющий собой компонент адреса - сельское поселение.
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
		/// Класс представляющий собой компонент адреса - сельское поселение
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CAddressVillageSettlement : CNameableId, IComparable<CAddressVillageSettlement>, ILotusSupportViewInspector
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Андреевское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Andreyevskoye = new CAddressVillageSettlement() 
			{ 
				Id = 1, 
				Name = "Андреевское сельское поселение", 
				ShortName = "Андреевское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Атамановское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Atamanovskoye = new CAddressVillageSettlement() 
			{ 
				Id = 2, 
				Name = "Атамановское сельское поселение", 
				ShortName = "Атамановское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Белокаменское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Belokamenskoye = new CAddressVillageSettlement() 
			{ 
				Id = 3, 
				Name = "Белокаменское сельское поселение", 
				ShortName = "Белокаменское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Боровское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Borovskoye = new CAddressVillageSettlement() 
			{
				Id = 4, 
				Name = "Боровское сельское поселение", 
				ShortName = "Боровское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Брединское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Bredinskoye = new CAddressVillageSettlement() 
			{
				Id = 5, 
				Name = "Брединское сельское поселение", 
				ShortName = "Брединское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Калининское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Kalininskoye = new CAddressVillageSettlement() 
			{ 
				Id = 6, 
				Name = "Калининское сельское поселение", 
				ShortName = "Калининское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Княженское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Knyazhenskoye = new CAddressVillageSettlement() 
			{
				Id = 7, 
				Name = "Княженское сельское поселение", 
				ShortName = "Княженское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Комсомольское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Komsomolskoye = new CAddressVillageSettlement() 
			{
				Id = 8, 
				Name = "Комсомольское сельское поселение", 
				ShortName = "Комсомольское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Наследницкое сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Naslednitskoye = new CAddressVillageSettlement() 
			{ 
				Id = 9, 
				Name = "Наследницкое сельское поселение", 
				ShortName = "Наследницкое СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Павловское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Pavlovskoye = new CAddressVillageSettlement() 
			{
				Id = 10, 
				Name = "Павловское сельское поселение", 
				ShortName = "Павловское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Рымникское сельское поселение
			/// </summary>
			public static readonly CAddressVillageSettlement Rymnikskoye = new CAddressVillageSettlement() 
			{
				Id = 11, 
				Name = "Рымникское сельское поселение", 
				ShortName = "Рымникское СП",
				VillageSettlementType = "Cельское поселение"
			};

			/// <summary>
			/// Краткие названия сельских поселений
			/// </summary>
			public static readonly String[] ShortNames = new String[]
			{
				"Андреевское СП",
				"Атамановское СП",
				"Белокаменское СП",
				"Боровское СП",
				"Брединское СП",
				"Калининское СП",
				"Княженское СП",
				"Комсомольское СП",
				"Наследницкое СП",
				"Павловское СП",
				"Рымникское СП"
			};
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsShortName = new PropertyChangedEventArgs(nameof(ShortName));
			protected static readonly PropertyChangedEventArgs PropertyArgsVillageSettlementType = new PropertyChangedEventArgs(nameof(VillageSettlementType));
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CAddressVillageSettlement"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CAddressVillageSettlement>();
				model.ToTable("address_village_settlement");
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

				var property_sname = model.Property(vs => vs.ShortName);
				property_sname.HasColumnName("sname");
				property_sname.HasMaxLength(40);

				var property_villagesettlementtype = model.Property(vs => vs.VillageSettlementType);
				property_villagesettlementtype.HasColumnName("village_type");

				// Данные
				model.HasData(Andreyevskoye,
					Atamanovskoye,
					Belokamenskoye,
					Borovskoye,
					Bredinskoye,
					Kalininskoye,
					Knyazhenskoye,
					Komsomolskoye,
					Naslednitskoye,
					Pavlovskoye,
					Rymnikskoye);
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal String mShortName;
			internal String mVillageSettlementType;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Краткое наименование
			/// </summary>
			[DisplayName("Краткое наименование")]
			[Description("Краткое наименование")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(1)]
			[XmlAttribute]
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
			/// Население
			/// </summary>
			[DisplayName("Тип поселения")]
			[Description("Тип поселения")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(3)]
			[XmlIgnore]
			public String VillageSettlementType
			{
				get { return (mVillageSettlementType); }
				set
				{
					mVillageSettlementType = value;
					NotifyPropertyChanged(PropertyArgsVillageSettlementType);
				}
			}

			/// <summary>
			/// Список населённых пунктов
			/// </summary>
			[Browsable(false)]
			public List<CAddressVillage> Villages { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportViewInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorTypeName
			{
				get { return ("СЕЛЬСКОЕ ПОСЕЛЕНИЕ"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorObjectName
			{
				get
				{
					return (mShortName);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CAddressVillageSettlement()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование сельского поселения</param>
			//---------------------------------------------------------------------------------------------------------
			public CAddressVillageSettlement(String name)
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
			public Int32 CompareTo(CAddressVillageSettlement other)
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
