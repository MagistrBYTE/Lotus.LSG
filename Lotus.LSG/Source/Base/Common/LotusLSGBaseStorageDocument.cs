//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseStorageDocument.cs
*		Инфраструктура обеспечивавшая хранения документов в базе данных.
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
		//! \addtogroup MunicipalityBaseCommon
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс описывающий параметры хранения документа в базе данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CDocumentItem : CNameableId, IComparable<CDocumentItem>, ILotusCopyParameters, ILotusSupportEditInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			private static PropertyChangedEventArgs PropertyArgsContentType = new PropertyChangedEventArgs(nameof(ContentType));
			private static PropertyChangedEventArgs PropertyArgsOriginalName = new PropertyChangedEventArgs(nameof(OriginalName));
			private static PropertyChangedEventArgs PropertyArgsShortName = new PropertyChangedEventArgs(nameof(ShortName));
			private static PropertyChangedEventArgs PropertyArgsExtension = new PropertyChangedEventArgs(nameof(Extension));
			private static PropertyChangedEventArgs PropertyArgsSize = new PropertyChangedEventArgs(nameof(Size));
			private static PropertyChangedEventArgs PropertyArgsAuthorId = new PropertyChangedEventArgs(nameof(AuthorId));
			private static PropertyChangedEventArgs PropertyArgsDateLoaded = new PropertyChangedEventArgs(nameof(DateLoaded));

			private static PropertyChangedEventArgs PropertyArgsGroup = new PropertyChangedEventArgs(nameof(Group));
			private static PropertyChangedEventArgs PropertyArgsSubGroup = new PropertyChangedEventArgs(nameof(SubGroup));

			private static PropertyChangedEventArgs PropertyArgsIsImbedded = new PropertyChangedEventArgs(nameof(IsImbedded));
			private static PropertyChangedEventArgs PropertyArgsData = new PropertyChangedEventArgs(nameof(Data));
			private static PropertyChangedEventArgs PropertyArgsLinkUrlCloud = new PropertyChangedEventArgs(nameof(LinkUrlCloud));
			private static PropertyChangedEventArgs PropertyArgsLinkUrlZakupki = new PropertyChangedEventArgs(nameof(LinkUrlZakupki));

			/// <summary>
			/// Данные для сериализации
			/// </summary>
			private static CSerializeData mDocumentItemSerializeData;

			/// <summary>
			/// Описание свойств
			/// </summary>
			public readonly static CPropertyDesc[] DocumentItemPropertiesDesc = new CPropertyDesc[]
			{
				// Идентификация
				CPropertyDesc.OverrideDisplayNameAndDescription<CDocumentItem>(nameof(Name), "Наименование", "Наименование документа"),
			};
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных для сериализации
			/// </summary>
			/// <returns>Данные для сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CSerializeData GetSerializeData()
			{
				if (mDocumentItemSerializeData == null)
				{
					mDocumentItemSerializeData = new CSerializeData(typeof(CContract));
					mDocumentItemSerializeData.AddProperty(nameof(Name));
					mDocumentItemSerializeData.AddProperty(nameof(OriginalName));
					mDocumentItemSerializeData.AddProperty(nameof(ShortName));
					mDocumentItemSerializeData.AddProperty(nameof(Extension));
					mDocumentItemSerializeData.AddProperty(nameof(Size));
					mDocumentItemSerializeData.AddProperty(nameof(AuthorId));

					mDocumentItemSerializeData.AddProperty(nameof(Group));
					mDocumentItemSerializeData.AddProperty(nameof(SubGroup));

					mDocumentItemSerializeData.AddProperty(nameof(IsImbedded));
					mDocumentItemSerializeData.AddProperty(nameof(Data));
					mDocumentItemSerializeData.AddProperty(nameof(LinkUrlCloud));
					mDocumentItemSerializeData.AddProperty(nameof(LinkUrlZakupki));
					mDocumentItemSerializeData.AddProperty(nameof(Id));
				}

				return (mDocumentItemSerializeData);
			}
			#endregion

#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CDocumentItem"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CDocumentItem>();
				model.ToTable("document_item");
				model.HasKey(vs => vs.Id);
				model.HasIndex(vs => vs.Id).IsUnique();
				model.Ignore(vs => vs.InspectorObjectName);
				model.Ignore(vs => vs.InspectorTypeName);

				var property_name = model.Property(vs => vs.Name);
				property_name.HasColumnName("names");
				property_name.HasMaxLength(100);
				property_name.IsRequired();

				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");
				property_id.ValueGeneratedNever();

				var property_original_name = model.Property(vs => vs.OriginalName);
				property_original_name.HasColumnName("original_name");
				property_original_name.HasMaxLength(200);

				var property_short_name = model.Property(vs => vs.ShortName);
				property_short_name.HasColumnName("sname");
				property_short_name.HasMaxLength(20);

				var property_extension = model.Property(vs => vs.Extension);
				property_extension.HasColumnName("extension");
				property_extension.HasMaxLength(10);

				var property_size = model.Property(vs => vs.Size);
				property_extension.HasColumnName("size");

				var property_date_load = model.Property(vs => vs.DateLoad);
				property_date_load.HasColumnName("date_load");

				var property_author_id = model.Property(vs => vs.AuthorId);
				property_author_id.HasColumnName("author_id");

				var property_group = model.Property(vs => vs.Group);
				property_group.HasColumnName("group");
				property_group.HasMaxLength(20);

				var property_subgroup = model.Property(vs => vs.SubGroup);
				property_subgroup.HasColumnName("subgroup");
				property_subgroup.HasMaxLength(20);

				var property_is_imbedded = model.Property(vs => vs.IsImbedded);
				property_is_imbedded.HasColumnName("is_imbedded");

				var property_data = model.Property(vs => vs.Data);
				property_data.HasColumnName("data");

				var property_linkurlcloud = model.Property(vs => vs.LinkUrlCloud);
				property_linkurlcloud.HasColumnName("url_cloud");

				var property_linkurlzakupki = model.Property(vs => vs.LinkUrlZakupki);
				property_linkurlzakupki.HasColumnName("url_zakupki");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mContentType;
			protected internal String mOriginalName;
			protected internal String mShortName;
			protected internal String mExtension;
			protected internal Int32 mSize;
			protected internal String mAuthorId;
			protected internal DateTime mDateLoaded;

			// Группирование документа
			protected internal String mGroup;
			protected internal String mSubGroup;

			// Ссылки на размещение
			protected internal Boolean mIsImbedded;
			protected internal Byte[] mData;
			protected internal String mLinkUrlCloud;
			protected internal String mLinkUrlZakupki;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип документа
			/// </summary>
			[DisplayName("Тип документа")]
			[Description("Тип контента документа")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			[LotusCategoryOrder(1)]
			[XmlAttribute]
			public String ContentType
			{
				get { return (mContentType); }
				set
				{
					mContentType = value;
					NotifyPropertyChanged(PropertyArgsContentType);
				}
			}

			/// <summary>
			/// Оригинальное имя документа при загрузки
			/// </summary>
			[DisplayName("Оригинальное имя")]
			[Description("Оригинальное имя документа при загрузки")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			[XmlAttribute]
			public String OriginalName
			{
				get { return (mOriginalName); }
				set
				{
					mOriginalName = value;
					NotifyPropertyChanged(PropertyArgsOriginalName);
				}
			}

			/// <summary>
			/// Краткое имя документа
			/// </summary>
			[DisplayName("Краткое имя")]
			[Description("Краткое имя документа")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusCategoryOrder(2)]
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
			/// Расширение файла документа
			/// </summary>
			[DisplayName("Расширение")]
			[Description("Расширение файла документа")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(3)]
			[XmlAttribute]
			public String Extension
			{
				get { return (mExtension); }
				set
				{
					mExtension = value;
					NotifyPropertyChanged(PropertyArgsExtension);
				}
			}

			/// <summary>
			/// Размер документа в байтах 
			/// </summary>
			[DisplayName("Размер документа")]
			[Description("Размер документа в байтах ")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(4)]
			[XmlAttribute]
			public Int32 Size
			{
				get { return (mSize); }
				set
				{
					mSize = value;
					NotifyPropertyChanged(PropertyArgsSize);
				}
			}

			/// <summary>
			/// Идентификатор пользователя загрузивший данный документ
			/// </summary>
			[DisplayName("Идентификатор пользователя")]
			[Description("Идентификатор пользователя загрузивший данный документ")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(5)]
			[XmlAttribute]
			public String AuthorId
			{
				get { return (mAuthorId); }
				set
				{
					mAuthorId = value;
					NotifyPropertyChanged(PropertyArgsAuthorId);
				}
			}

			/// <summary>
			/// Дата загрузки документа
			/// </summary>
			[DisplayName("Дата загрузки")]
			[Description("Дата загрузки документа")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(6)]
			[XmlAttribute]
			public DateTime DateLoaded
			{
				get { return (mDateLoaded); }
				set
				{
					mDateLoaded = value;
					NotifyPropertyChanged(PropertyArgsDateLoaded);
				}
			}

			//
			// ГРУППИРОВАНИЕ ДОКУМЕНТА
			//
			/// <summary>
			/// Основная группа
			/// </summary>
			[DisplayName("Группа")]
			[Description("Основная группа")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(7)]
			[XmlAttribute]
			public String Group
			{
				get { return (mGroup); }
				set
				{
					mGroup = value;
					NotifyPropertyChanged(PropertyArgsGroup);
				}
			}

			/// <summary>
			/// Подгруппа
			/// </summary>
			[DisplayName("Подгруппа")]
			[Description("Подгруппа")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(8)]
			[XmlAttribute]
			public String SubGroup
			{
				get { return (mSubGroup); }
				set
				{
					mSubGroup = value;
					NotifyPropertyChanged(PropertyArgsSubGroup);
				}
			}

			//
			// ССЫЛКИ НА РАЗМЕЩЕНИЕ
			//
			/// <summary>
			/// Статус документа размещённого непосредственно в базе данных
			/// </summary>
			[DisplayName("Статус размещения")]
			[Description("Статус документа размещённого непосредственно в базе данных")]
			[Category(XInspectorGroupDesc.LinkPlace)]
			[LotusPropertyOrder(1)]
			[LotusCategoryOrder(2)]
			[XmlAttribute]
			public Boolean IsImbedded
			{
				get { return (mIsImbedded); }
				set
				{
					mIsImbedded = value;
					NotifyPropertyChanged(PropertyArgsIsImbedded);
				}
			}

			/// <summary>
			/// Данные документа
			/// </summary>
			[DisplayName("Данные документа")]
			[Description("Данные документа если он размещен непосредственно в базе данных")]
			[Category(XInspectorGroupDesc.LinkPlace)]
			[LotusPropertyOrder(2)]
			[XmlElement]
			public Byte[] Data
			{
				get { return (mData); }
				set
				{
					mData = value;
					NotifyPropertyChanged(PropertyArgsData);
				}
			}

			/// <summary>
			/// Полная ссылка на документ в облаке
			/// </summary>
			[DisplayName("Ссылка в облаке")]
			[Description("Полная ссылка на документ в облаке")]
			[Category(XInspectorGroupDesc.LinkPlace)]
			[LotusPropertyOrder(3)]
			[XmlAttribute]
			public String LinkUrlCloud
			{
				get { return (mLinkUrlCloud); }
				set
				{
					mLinkUrlCloud = value;
					NotifyPropertyChanged(PropertyArgsLinkUrlCloud);
				}
			}

			/// <summary>
			/// Полная ссылка на документ на сайте госзакупок
			/// </summary>
			[DisplayName("Ссылка на госзакупках")]
			[Description("Полная ссылка на документ на сайте госзакупок")]
			[Category(XInspectorGroupDesc.LinkPlace)]
			[LotusPropertyOrder(4)]
			[XmlAttribute]
			public String LinkUrlZakupki
			{
				get { return (mLinkUrlZakupki); }
				set
				{
					mLinkUrlZakupki = value;
					NotifyPropertyChanged(PropertyArgsLinkUrlZakupki);
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
				get { return ("ДОКУМЕНТ"); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CDocumentItem()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CDocumentItem(String name)
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
			public Int32 CompareTo(CDocumentItem other)
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
				CDocumentItem clone = new CDocumentItem();
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
			#endregion

			#region ======================================= МЕТОДЫ ILotusCopyParameters ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование параметров с указанного объекта
			/// </summary>
			/// <param name="source_object">Объект источник с которого будут скопированы параметры</param>
			/// <param name="parameters">Параметры копирования</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void CopyParameters(System.Object source_object, CParameters parameters)
			{
				if (source_object is CDocumentItem source)
				{
					Group = source.mGroup;
					SubGroup = source.mSubGroup;
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
				return (DocumentItemPropertiesDesc);
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
