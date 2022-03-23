//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Базовый модуль
// Подраздел: Подсистема субъектов гражданских правоотношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGSubjectCivilIndividual.cs
*		Определение данных для субъекта гражданских правоотношений – индивидуального предпринимателя.
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
		/// Тип индивидуальной деятельности
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TIndividualType>))]
		public enum TIndividualType
		{
			/// <summary>
			/// Индивидуальный предприниматель
			/// </summary>
			[Description("Индивидуальный предприниматель")]
			Businessman = 0,

			/// <summary>
			/// Глава крестьянского (фермерского) хозяйства
			/// </summary>
			[Description("Глава КФХ")]
			KFH = 1
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий вспомогательную модель для работы с перечислением <see cref="TIndividualType"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CIndividualTypeModel
		{
			public static readonly CIndividualTypeModel[] Data = new CIndividualTypeModel[2]
			{
				new CIndividualTypeModel()
				{
					Name = nameof(TIndividualType.Businessman),
					Desc = TIndividualType.Businessman.GetDescriptionOrName(),
					Value = TIndividualType.Businessman
				},

				new CIndividualTypeModel()
				{
					Name = nameof(TIndividualType.KFH),
					Desc = TIndividualType.KFH.GetDescriptionOrName(),
					Value = TIndividualType.KFH
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
			public TIndividualType Value { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для определения субъекта гражданских правоотношений – индивидульного предпринматеял
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CIndividualPerson : CSubjectCivil, IComparable<CIndividualPerson>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsIndividualType = new PropertyChangedEventArgs(nameof(IndividualType));
			protected static readonly PropertyChangedEventArgs PropertyArgsOGRN = new PropertyChangedEventArgs(nameof(OGRN));
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
				var model = model_builder.Entity<CIndividualPerson>();
				model.HasBaseType<CSubjectCivil>();

				var property_ogrn = model.Property(vs => vs.OGRN);
				property_ogrn.HasColumnName("ogrn");            // Сопоставление с именем столбца
				property_ogrn.HasMaxLength(20);                 // Максимальная длина поля

				var property_individual_type = model.Property(vs => vs.IndividualType);
				property_individual_type.HasColumnName("individual_type");            // Сопоставление с именем столбца
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal String mOGRN;
			internal TIndividualType mIndividualType;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Основной государственный регистрационный номер
			/// </summary>
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
			/// Тип индивидуальной деятельности
			/// </summary>
			public TIndividualType IndividualType
			{
				get { return (mIndividualType); }
				set
				{
					mIndividualType = value;
					NotifyPropertyChanged(PropertyArgsIndividualType);
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
				get { return ("ИНДИВИДУАЛЬНАЯ ДЕЯТЕЛЬНОСТЬ"); }
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
			public CIndividualPerson()
			{
				mSubjectCivilType = TSubjectCivilType.Person;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование юридического лица</param>
			//---------------------------------------------------------------------------------------------------------
			public CIndividualPerson(String name)
				: base(name)
			{
				mSubjectCivilType = TSubjectCivilType.Person;
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
			public Int32 CompareTo(CIndividualPerson other)
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
			/// <param name="individual_person">Объект-источник с которого будут скопированы параметры</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyParameters(CIndividualPerson individual_person)
			{
				base.CopyParameters(individual_person);

				if (individual_person != null)
				{
					mOGRN = individual_person.OGRN;
					mIndividualType = individual_person.IndividualType;
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
