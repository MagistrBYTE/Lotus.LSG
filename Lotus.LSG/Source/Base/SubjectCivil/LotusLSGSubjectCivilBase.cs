//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Базовый модуль
// Подраздел: Подсистема субъектов гражданских правоотношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGSubjectCivilBase.cs
*		Определение базового субъекта гражданских правоотношений.
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
		//! \addtogroup MunicipalityBaseSubjectCivil
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения базового субъекта гражданских правоотношений
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CSubjectCivil : CNameableId, IComparable<CSubjectCivil>, ILotusSupportViewInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsShortName = new PropertyChangedEventArgs(nameof(ShortName));
			protected static readonly PropertyChangedEventArgs PropertyArgsINN = new PropertyChangedEventArgs(nameof(INN));
			protected static readonly PropertyChangedEventArgs PropertyArgsSubjectCivilType = new PropertyChangedEventArgs(nameof(SubjectCivilType));
			#endregion
#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CSubjectCivil"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CSubjectCivil>();
				model.ToTable("subject_civil");				// Имя таблицы
				model.HasKey(vs => vs.Id);					// Первичный ключ
				model.HasIndex(vs => vs.Id).IsUnique();		// Для увеличения производительности поиска в базе данных применяются индексы. Уникальность индексов
				model.Ignore(vs => vs.InspectorObjectName);	// Игнорируем указанные свойства
				model.Ignore(vs => vs.InspectorTypeName);   // Игнорируем указанные свойства

				var property_name = model.Property(vs => vs.Name);	
				property_name.HasColumnName("names");			// Сопоставление с именем столбца
				property_name.HasMaxLength(200);				// Максимальная длина поля
				property_name.IsRequired();						// Поле обязательно должно иметь значение

				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");               // Сопоставление с именем столбца

				var property_sname = model.Property(vs => vs.ShortName);
				property_sname.HasColumnName("sname");         // Сопоставление с именем столбца
				property_sname.HasMaxLength(40);               // Максимальная длина поля

				var property_inn = model.Property(vs => vs.INN);
				property_inn.HasColumnName("inn");            // Сопоставление с именем столбца
				property_inn.HasMaxLength(20);                // Максимальная длина поля

				var property_civil_type = model.Property(vs => vs.SubjectCivilType);
				property_civil_type.HasColumnName("civil_type");            // Сопоставление с именем столбца
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal String mShortName;
			internal String mINN;
			internal TSubjectCivilType mSubjectCivilType;
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
			/// Индивидуальный налоговый номер
			/// </summary>
			[DisplayName("ИНН")]
			[Description("Индивидуальный налоговый номер")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(2)]
			[XmlAttribute]
			public String INN
			{
				get { return (mINN); }
				set
				{
					mINN = value;
					NotifyPropertyChanged(PropertyArgsINN);
				}
			}

			/// <summary>
			/// Базовый тип субъекта гражданских правоотношений
			/// </summary>
			[DisplayName("Тип субъекта")]
			[Description("Базовый тип субъекта гражданских правоотношений")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(4)]
			[XmlAttribute]
			public TSubjectCivilType SubjectCivilType
			{
				get { return (mSubjectCivilType); }
				set
				{
					mSubjectCivilType = value;
					NotifyPropertyChanged(PropertyArgsSubjectCivilType);
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
				get { return ("ЛИЦО"); }
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
			public CSubjectCivil()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование лица</param>
			//---------------------------------------------------------------------------------------------------------
			public CSubjectCivil(String name)
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
			public Int32 CompareTo(CSubjectCivil other)
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
			/// <param name="subject_civil">Объект-источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(CSubjectCivil subject_civil)
			{
				if(subject_civil != null)
				{
					mName = subject_civil.Name;
					mShortName = subject_civil.ShortName;
					mINN = subject_civil.INN;
					mSubjectCivilType = subject_civil.SubjectCivilType;
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
