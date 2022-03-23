//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Базовый модуль
// Подраздел: Подсистема субъектов гражданских правоотношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGSubjectCivilPublicAuthority.cs
*		Определение данных для субъекта гражданских правоотношений – органа публичной власти.
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
		//! \addtogroup MunicipalityBaseSubjectCivil
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип органа публичной власти
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TPublicAuthorityType>))]
		public enum TPublicAuthorityType
		{
			/// <summary>
			/// Сельское поселение
			/// </summary>
			[Description("Сельское поселение")]
			Village = 0,

			/// <summary>
			/// Муниципальный район
			/// </summary>
			[Description("Муниципальный район")]
			Municipal = 1,

			/// <summary>
			/// Субъект РФ
			/// </summary>
			[Description("Субъект РФ")]
			Regional = 2,

			/// <summary>
			/// Федеральный уровень
			/// </summary>
			[Description("РФ")]
			Federal = 3
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий вспомогательную модель для работы с перечислением <see cref="TPublicAuthorityType"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CPublicAuthorityTypeModel
		{
			public static readonly CPublicAuthorityTypeModel[] Data = new CPublicAuthorityTypeModel[4]
			{
				new CPublicAuthorityTypeModel()
				{
					Name = nameof(TPublicAuthorityType.Village),
					Desc = TPublicAuthorityType.Village.GetDescriptionOrName(),
					Value = TPublicAuthorityType.Village
				},

				new CPublicAuthorityTypeModel()
				{
					Name = nameof(TPublicAuthorityType.Municipal),
					Desc = TPublicAuthorityType.Municipal.GetDescriptionOrName(),
					Value = TPublicAuthorityType.Municipal
				},

				new CPublicAuthorityTypeModel()
				{
					Name = nameof(TPublicAuthorityType.Regional),
					Desc = TPublicAuthorityType.Regional.GetDescriptionOrName(),
					Value = TPublicAuthorityType.Regional
				},

				new CPublicAuthorityTypeModel()
				{
					Name = nameof(TPublicAuthorityType.Federal),
					Desc = TPublicAuthorityType.Federal.GetDescriptionOrName(),
					Value = TPublicAuthorityType.Federal
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
			public TPublicAuthorityType Value { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения субъекта гражданских правоотношений – органа публичной власти
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CPublicAuthority : CLegalEntityBase, IComparable<CPublicAuthority>
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Андреевское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Andreyevskoye = new CPublicAuthority() 
			{ 
				Id = 1000, 
				Name = "Администрация Андреевского сельского поселения", 
				ShortName = "Администрация Андреевского СП",
				INN = "7427003567",
				OGRN = "1027401514436",
				KPP = "745801001",
			};

			/// <summary>
			/// Атамановское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Atamanovskoye = new CPublicAuthority() 
			{ 
				Id = 1002, 
				Name = "Администрация Атамановского сельского поселения", 
				ShortName = "Администрация Атамановского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Белокаменское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Belokamenskoye = new CPublicAuthority() 
			{ 
				Id = 1003, 
				Name = "Администрация Белокаменского сельского поселения", 
				ShortName = "Администрация Белокаменского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Боровское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Borovskoye = new CPublicAuthority() 
			{ 
				Id = 1004, 
				Name = "Администрация Боровского сельского поселения", 
				ShortName = "Администрация Боровского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Брединское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Bredinskoye = new CPublicAuthority() 
			{ 
				Id = 1005, 
				Name = "Администрация Брединского сельского поселения", 
				ShortName = "Администрация Брединского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Калининское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Kalininskoye = new CPublicAuthority() 
			{ 
				Id = 1006, 
				Name = "Администрация Калининского сельского поселения", 
				ShortName = "Администрация Калининского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Княженское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Knyazhenskoye = new CPublicAuthority() 
			{ 
				Id = 1007, 
				Name = "Администрация Княженского сельского поселения", 
				ShortName = "Администрация Княженского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Комсомольское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Komsomolskoye = new CPublicAuthority() 
			{ 
				Id = 1008, 
				Name = "Администрация Комсомольского сельского поселения", 
				ShortName = "Администрация Комсомольского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Наследницкое сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Naslednitskoye = new CPublicAuthority() 
			{
				Id = 1009, 
				Name = "Администрация Наследницкого сельского поселения", 
				ShortName = "Администрация Наследницкого СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Павловское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Pavlovskoye = new CPublicAuthority() 
			{ 
				Id = 1010, 
				Name = "Администрация Павловского сельского поселения",
				ShortName = "Администрация Павловского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Рымникское сельское поселение
			/// </summary>
			public static readonly CPublicAuthority Rymnikskoye = new CPublicAuthority() 
			{ 
				Id = 1011, 
				Name = "Администрация Рымникского сельского поселения", 
				ShortName = "Администрация Рымникского СП",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Администрация района
			/// </summary>
			public static readonly CPublicAuthority DistrictAdmin = new CPublicAuthority() 
			{ 
				Id = 1012, 
				Name = "Администрация Брединского муниципального района", 
				ShortName = "Администрация района",
				INN = "7427004708",
				OGRN = "1027401514425",
				KPP = "745801001",
			};

			/// <summary>
			/// Органы власти района
			/// </summary>
			public static readonly CPublicAuthority[] DistrictAuthorities = new CPublicAuthority[]
			{
				Andreyevskoye,
				Atamanovskoye,
				Belokamenskoye,
				Borovskoye,
				Bredinskoye,
				Kalininskoye,
				Knyazhenskoye,
				Komsomolskoye,
				Naslednitskoye,
				Pavlovskoye,
				Rymnikskoye,
				DistrictAdmin
			};
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsPublicType = new PropertyChangedEventArgs(nameof(PublicType));

			/// <summary>
			/// Данные для сериализации
			/// </summary>
			private static CSerializeData mPublicAuthoritySerializeData;
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных для сериализации
			/// </summary>
			/// <returns>Данные для сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			public new static CSerializeData GetSerializeData()
			{
				if (mPublicAuthoritySerializeData == null)
				{
					mPublicAuthoritySerializeData = new CSerializeData(typeof(CPublicAuthority));
					mPublicAuthoritySerializeData.AddProperty(nameof(Name));
					mPublicAuthoritySerializeData.AddProperty(nameof(ShortName));
					mPublicAuthoritySerializeData.AddProperty(nameof(INN));
					mPublicAuthoritySerializeData.AddProperty(nameof(OGRN));
					mPublicAuthoritySerializeData.AddProperty(nameof(KPP));
					mPublicAuthoritySerializeData.AddProperty(nameof(OKPO));
					mPublicAuthoritySerializeData.AddProperty(nameof(OKVED));
					mPublicAuthoritySerializeData.AddProperty(nameof(LeaderName));
					mPublicAuthoritySerializeData.AddProperty(nameof(LeaderPost));
					mPublicAuthoritySerializeData.AddProperty(nameof(Id));
					mPublicAuthoritySerializeData.AddProperty(nameof(SubjectCivilType));
					mPublicAuthoritySerializeData.AddProperty(nameof(PublicType));
				}

				return (mPublicAuthoritySerializeData);
			}
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CPublicAuthority"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public new static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CPublicAuthority>();
				model.HasBaseType<CLegalEntityBase>();

				var property_public_type = model.Property(vs => vs.PublicType);
				property_public_type.HasColumnName("public_type");            // Сопоставление с именем столбца

				model.HasData(DistrictAuthorities);
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TPublicAuthorityType mPublicType;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип органа публичной власти
			/// </summary>
			public TPublicAuthorityType PublicType
			{
				get { return (mPublicType); }
				set
				{
					mPublicType = value;
					NotifyPropertyChanged(PropertyArgsPublicType);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportViewInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public override String InspectorTypeName
			{
				get { return ("ОРГАН ВЛАСТИ"); }
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
			public CPublicAuthority()
			{
				mSubjectCivilType = TSubjectCivilType.Public;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование юридического лица</param>
			//---------------------------------------------------------------------------------------------------------
			public CPublicAuthority(String name)
				: base(name)
			{
				mSubjectCivilType = TSubjectCivilType.Public;
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
			public Int32 CompareTo(CPublicAuthority other)
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

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="public_authority">Объект-источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(CPublicAuthority public_authority)
			{
				base.CopyParameters(public_authority);

				if (public_authority != null)
				{
					mPublicType = public_authority.PublicType;
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
