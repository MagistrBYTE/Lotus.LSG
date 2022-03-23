//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Базовый модуль
// Подраздел: Подсистема субъектов гражданских правоотношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGSubjectCivilLegalEntity.cs
*		Определение данных для субъекта гражданских правоотношений – юридического лица.
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
		/// Тип юридического лица по отношению к праву собственности
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TLegalEntityOwnership>))]
		public enum TLegalEntityOwnership
		{
			/// <summary>
			/// Частное юридического лица
			/// </summary>
			[Description("Частное")]
			Private = 0,

			/// <summary>
			/// Муниципальное юридическое лицо
			/// </summary>
			[Description("Муниципальное")]
			Municipal = 1,

			/// <summary>
			/// Юридическое лицо субъекта РФ
			/// </summary>
			[Description("Субъекта РФ")]
			Regional = 2,

			/// <summary>
			/// Юридическое лицо Российской Федерации
			/// </summary>
			[Description("Федеральное")]
			Federal = 3
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий вспомогательную модель для работы с перечислением <see cref="TLegalEntityOwnership"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CLegalEntityOwnershipModel
		{
			public static readonly CLegalEntityOwnershipModel[] Data = new CLegalEntityOwnershipModel[4]
			{
				new CLegalEntityOwnershipModel()
				{
					Name = nameof(TLegalEntityOwnership.Private),
					Desc = TLegalEntityOwnership.Private.GetDescriptionOrName(),
					Value = TLegalEntityOwnership.Private
				},

				new CLegalEntityOwnershipModel()
				{
					Name = nameof(TLegalEntityOwnership.Municipal),
					Desc = TLegalEntityOwnership.Municipal.GetDescriptionOrName(),
					Value = TLegalEntityOwnership.Municipal
				},

				new CLegalEntityOwnershipModel()
				{
					Name = nameof(TLegalEntityOwnership.Regional),
					Desc = TLegalEntityOwnership.Regional.GetDescriptionOrName(),
					Value = TLegalEntityOwnership.Regional
				},

				new CLegalEntityOwnershipModel()
				{
					Name = nameof(TLegalEntityOwnership.Federal),
					Desc = TLegalEntityOwnership.Federal.GetDescriptionOrName(),
					Value = TLegalEntityOwnership.Federal
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
			public TLegalEntityOwnership Value { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип юридического лица
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TLegalEntityType>))]
		public enum TLegalEntityType
		{
			/// <summary>
			/// Общество с ограниченной ответственностью
			/// </summary>
			[Description("ООО")]
			LimitedСompany = 0,

			/// <summary>
			/// Учреждение
			/// </summary>
			[Description("Учреждение")]
			Institution = 1,
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий вспомогательную модель для работы с перечислением <see cref="TLegalEntityType"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CLegalEntityTypeModel
		{
			public static readonly CLegalEntityTypeModel[] Data = new CLegalEntityTypeModel[2]
			{
				new CLegalEntityTypeModel()
				{
					Name = nameof(TLegalEntityType.LimitedСompany),
					Desc = TLegalEntityType.LimitedСompany.GetDescriptionOrName(),
					Value = TLegalEntityType.LimitedСompany
				},

				new CLegalEntityTypeModel()
				{
					Name = nameof(TLegalEntityType.Institution),
					Desc = TLegalEntityType.Institution.GetDescriptionOrName(),
					Value = TLegalEntityType.Institution
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
			public TLegalEntityType Value { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для представления базового юридического лица
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILegalEntityBase
		{
			/// <summary>
			/// Основной государственный регистрационный номер
			/// </summary>
			String OGRN { get; set; }

			/// <summary>
			/// Код причины постановки
			/// </summary>
			String KPP { get; set; }

			/// <summary>
			/// Код общероссийского классификатора предприятий и организаций 
			/// </summary>
			String OKPO { get; set; }

			/// <summary>
			/// ОКВЭД
			/// </summary>
			String OKVED { get; set; }
			
			/// <summary>
			/// Имя руководителя
			/// </summary>
			String LeaderName { get; set; }

			/// <summary>
			/// Должность руководителя
			/// </summary>
			String LeaderPost { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения субъекта гражданских правоотношений – юридического лица
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CLegalEntityBase : CSubjectCivil, IComparable<CLegalEntityBase>, ILegalEntityBase, ILotusSupportViewInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsOGRN = new PropertyChangedEventArgs(nameof(OGRN));
			protected static readonly PropertyChangedEventArgs PropertyArgsKPP = new PropertyChangedEventArgs(nameof(KPP));
			protected static readonly PropertyChangedEventArgs PropertyArgsOKPO = new PropertyChangedEventArgs(nameof(OKPO));
			protected static readonly PropertyChangedEventArgs PropertyArgsOKVED = new PropertyChangedEventArgs(nameof(OKVED));
			protected static readonly PropertyChangedEventArgs PropertyArgsLeaderName = new PropertyChangedEventArgs(nameof(LeaderName));
			protected static readonly PropertyChangedEventArgs PropertyArgsLeaderPost = new PropertyChangedEventArgs(nameof(LeaderPost));
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CLegalEntityBase"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public new static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CLegalEntityBase>();
				model.HasBaseType<CSubjectCivil>();

				var property_ogrn = model.Property(vs => vs.OGRN);
				property_ogrn.HasColumnName("ogrn");            // Сопоставление с именем столбца
				property_ogrn.HasMaxLength(20);                 // Максимальная длина поля

				var property_kpp = model.Property(vs => vs.KPP);
				property_kpp.HasColumnName("kpp");            // Сопоставление с именем столбца
				property_kpp.HasMaxLength(20);                 // Максимальная длина поля

				var property_okpo = model.Property(vs => vs.OKPO);
				property_okpo.HasColumnName("okpo");            // Сопоставление с именем столбца
				property_okpo.HasMaxLength(16);                 // Максимальная длина поля

				var property_okved = model.Property(vs => vs.OKVED);
				property_okved.HasColumnName("okved");            // Сопоставление с именем столбца
				property_okved.HasMaxLength(16);                 // Максимальная длина поля

				var property_leader_name = model.Property(vs => vs.LeaderName);
				property_leader_name.HasColumnName("leader_name");            // Сопоставление с именем столбца
				property_leader_name.HasMaxLength(40);                 // Максимальная длина поля

				var property_leader_post = model.Property(vs => vs.LeaderPost);
				property_leader_post.HasColumnName("leader_post");            // Сопоставление с именем столбца
				property_leader_post.HasMaxLength(20);                 // Максимальная длина поля
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal String mOGRN;
			internal String mKPP;
			internal String mOKPO;
			internal String mOKVED;
			internal String mLeaderName;
			internal String mLeaderPost;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Основной государственный регистрационный номер
			/// </summary>
			[DisplayName("ОГРН")]
			[Description("Основной государственный регистрационный номер")]
			[Category(XInspectorGroupDesc.ID)]
			[XmlAttribute]
			public String OGRN
			{
				get { return (mOGRN); }
				set
				{
					mOGRN = value;
					NotifyPropertyChanged(PropertyArgsOGRN);
				}
			}

			/// <summary>
			/// Код причины постановки
			/// </summary>
			[DisplayName("КПП")]
			[Description("Код причины постановки")]
			[Category(XInspectorGroupDesc.ID)]
			[XmlAttribute]
			public String KPP
			{
				get { return (mKPP); }
				set
				{
					mKPP = value;
					NotifyPropertyChanged(PropertyArgsKPP);
				}
			}

			/// <summary>
			/// Код общероссийского классификатора предприятий и организаций
			/// </summary>
			[DisplayName("ОКПО")]
			[Description("Код общероссийского классификатора предприятий и организаций")]
			[Category(XInspectorGroupDesc.ID)]
			[XmlAttribute]
			public String? OKPO
			{
				get { return (mOKPO); }
				set
				{
					mOKPO = value;
					NotifyPropertyChanged(PropertyArgsOKPO);
				}
			}

			/// <summary>
			/// ОКВЭД
			/// </summary>
			[DisplayName("ОКВЭД")]
			[Description("ОКВЭД")]
			[Category(XInspectorGroupDesc.ID)]
			[XmlAttribute]
			public String? OKVED
			{
				get { return (mOKVED); }
				set
				{
					mOKVED = value;
					NotifyPropertyChanged(PropertyArgsOKVED);
				}
			}


			/// <summary>
			/// Имя руководителя
			/// </summary>
			[DisplayName("Имя руководителя")]
			[Description("Имя руководителя")]
			[Category(XInspectorGroupDesc.ID)]
			[XmlAttribute]
			public String? LeaderName
			{
				get { return (mLeaderName); }
				set
				{
					mLeaderName = value;
					NotifyPropertyChanged(PropertyArgsLeaderName);
				}
			}

			/// <summary>
			/// Должность руководителя
			/// </summary>
			[DisplayName("Должность руководителя")]
			[Description("Должность руководителя")]
			[Category(XInspectorGroupDesc.ID)]
			[XmlAttribute]
			public String? LeaderPost 
			{
				get { return (mLeaderPost); }
				set
				{
					mLeaderPost = value;
					NotifyPropertyChanged(PropertyArgsLeaderPost);
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
				get { return ("ЮРИДИЧЕСКОЕ ЛИЦО"); }
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
			public CLegalEntityBase()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование юридического лица</param>
			//---------------------------------------------------------------------------------------------------------
			public CLegalEntityBase(String name)
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
			public Int32 CompareTo(CLegalEntityBase other)
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
			/// <param name="legal_entity_base">Объект-источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(CLegalEntityBase legal_entity_base)
			{
				base.CopyParameters(legal_entity_base);

				if (legal_entity_base != null)
				{
					mOGRN = legal_entity_base.OGRN;
					mKPP = legal_entity_base.KPP;
					mOKPO = legal_entity_base.OKPO;
					mOKVED = legal_entity_base.OKVED;
					mLeaderName = legal_entity_base.LeaderName;
					mLeaderPost = legal_entity_base.LeaderPost;
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения субъекта гражданских правоотношений – юридического лица
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CLegalEntity : CLegalEntityBase, IComparable<CLegalEntity>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsEntityType = new PropertyChangedEventArgs(nameof(EntityType));
			protected static readonly PropertyChangedEventArgs PropertyArgsEntityOwnership = new PropertyChangedEventArgs(nameof(EntityOwnership));
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CLegalEntity"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public new static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CLegalEntity>();
				model.HasBaseType<CLegalEntityBase>();

				var property_entity_type = model.Property(vs => vs.EntityType);
				property_entity_type.HasColumnName("entity_type");            // Сопоставление с именем столбца
				property_entity_type.HasConversion(
					vs => (Int32)vs,
					vs => (TLegalEntityType)Enum.ToObject(typeof(TLegalEntityType), vs));  // Храним как числа

				var property_entity_ownership = model.Property(vs => vs.EntityOwnership);
				property_entity_ownership.HasColumnName("entity_ownership");            // Сопоставление с именем столбца
				property_entity_ownership.HasConversion(
					vs => (Int32)vs,
					vs => (TLegalEntityOwnership)Enum.ToObject(typeof(TLegalEntityOwnership), vs));  // Храним как числа
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TLegalEntityType mEntityType;
			internal TLegalEntityOwnership mEntityOwnership;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип юридического лица
			/// </summary>
			public TLegalEntityType EntityType
			{
				get { return (mEntityType); }
				set
				{
					mEntityType = value;
					NotifyPropertyChanged(PropertyArgsEntityType);
				}
			}

			/// <summary>
			/// Тип юридического лица по отношению к праву собственности
			/// </summary>
			public TLegalEntityOwnership EntityOwnership
			{
				get { return (mEntityOwnership); }
				set
				{
					mEntityOwnership = value;
					NotifyPropertyChanged(PropertyArgsEntityOwnership);
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
				get { return ("ЮРИДИЧЕСКОЕ ЛИЦО"); }
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
			public CLegalEntity()
			{
				mSubjectCivilType = TSubjectCivilType.LegalEntity;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование юридического лица</param>
			//---------------------------------------------------------------------------------------------------------
			public CLegalEntity(String name)
				: base(name)
			{
				mSubjectCivilType = TSubjectCivilType.LegalEntity;
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
			public Int32 CompareTo(CLegalEntity other)
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
			/// <param name="legal_entity">Объект-источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(CLegalEntity legal_entity)
			{
				base.CopyParameters(legal_entity);

				if (legal_entity != null)
				{
					mEntityType = legal_entity.EntityType;
					mEntityOwnership = legal_entity.EntityOwnership;
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
