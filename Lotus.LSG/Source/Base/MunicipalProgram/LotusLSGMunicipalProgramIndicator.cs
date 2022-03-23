//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема муниципальных программ
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGMunicipalProgramIndicator.cs
*		Целевые индикаторы и показатели муниципальной программы.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
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
		//! \addtogroup MunicipalityBaseProgram
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Целевой индикатор муниципальной программы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMunicipalProgramIndicator : CNameableId, IComparable<CMunicipalProgramIndicator>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			private static readonly PropertyChangedEventArgs PropertyArgsDesc = new PropertyChangedEventArgs(nameof(Desc));
			private static readonly PropertyChangedEventArgs PropertyArgsValueUnit = new PropertyChangedEventArgs(nameof(ValueUnit));
			#endregion
#if USE_EFC
			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="CMunicipalProgramIndicator"/>
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				var model = model_builder.Entity<CMunicipalProgramIndicator>();
				model.ToTable("municipal_indicator");
				model.HasKey(vs => vs.Id);
				model.HasIndex(vs => vs.Id).IsUnique();
				model.Ignore(vs => vs.InspectorObjectName);
				model.Ignore(vs => vs.InspectorTypeName);
				model.Ignore(vs => vs.ProgramName);
				model.Ignore(vs => vs.SubProgramName);

				var property_name = model.Property(vs => vs.Name);
				property_name.HasColumnName("names");
				property_name.HasMaxLength(40);
				property_name.IsRequired();

				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");

				var property_desc = model.Property(vs => vs.Desc);
				property_desc.HasColumnName("desc");

				var property_value_unit = model.Property(vs => vs.ValueUnit);
				property_value_unit.HasColumnName("value_unit");

				var property_program_id = model.Property(vs => vs.ProgramId);
				property_program_id.HasColumnName("program_id");

				var property_sub_program_id = model.Property(vs => vs.SubProgramId);
				property_sub_program_id.HasColumnName("sub_program_id");
			}
			#endregion
#endif

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mDesc;
			protected internal String mValueUnit;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Описание целевого индикатора
			/// </summary>
			[DisplayName("Описание")]
			[Description("Описание целевого индикатора")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(3)]
			[XmlAttribute]
			public String Desc
			{
				get { return (mDesc); }
				set
				{
					mDesc = value;
					NotifyPropertyChanged(PropertyArgsDesc);
					//this.NotifyOwnerUpdated(nameof(Desc));
				}
			}

			/// <summary>
			/// Единица измерения целевого показателя
			/// </summary>
			[DisplayName("Единица измерения")]
			[Description("Единица измерения целевого показателя")]
			[Category(XInspectorGroupDesc.ID)]
			[LotusPropertyOrder(7)]
			[XmlAttribute]
			public String ValueUnit
			{
				get { return (mValueUnit); }
				set
				{
					mValueUnit = value;
					NotifyPropertyChanged(PropertyArgsValueUnit);
					//this.NotifyOwnerUpdated(nameof(CurrentValue));
				}
			}

			//
			// МУНИЦИПАЛЬНАЯ ПРОГРАММА
			//
			/// <summary>
			/// Идентификатор муниципальной программы
			/// </summary>
			[Browsable(false)]
			public Int64? ProgramId { get; set; }

			/// <summary>
			/// Муниципальная программа
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(ProgramId))]
			public CMunicipalProgram Program { get; set; }

			/// <summary>
			/// Наименование муниципальной программы
			/// </summary>
			public String ProgramName
			{
				get
				{
					if (Program == null)
					{
						return ("");
					}
					else
					{
						return (Program.Name);
					}
				}
			}

			//
			// МУНИЦИПАЛЬНАЯ ПОДПРОГРАММА
			//
			/// <summary>
			/// Идентификатор муниципальной подпрограммы
			/// </summary>
			[Browsable(false)]
			public Int64? SubProgramId { get; set; }

			/// <summary>
			/// Муниципальная подпрограмма
			/// </summary>
			[Browsable(false)]
			[ForeignKey(nameof(SubProgramId))]
			public CMunicipalSubProgram SubProgram { get; set; }

			/// <summary>
			/// Наименование муниципальной подпрограммы
			/// </summary>
			public String SubProgramName
			{
				get
				{
					if (SubProgram == null)
					{
						return ("");
					}
					else
					{
						return (SubProgram.Name);
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportEditInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			public override String InspectorTypeName
			{
				get { return ("ЦЕЛЕВОЙ ИНДИКАТОР"); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMunicipalProgramIndicator()
				: this("Индикатор")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CMunicipalProgramIndicator(String name)
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
			public Int32 CompareTo(CMunicipalProgramIndicator other)
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
				CMunicipalProgramIndicator clone = new CMunicipalProgramIndicator();

				clone.mName = mName;
				clone.mDesc = mDesc;
				clone.mValueUnit = mValueUnit;

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
				return (mDesc);
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение имени объекта.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected override void RaiseNameChanged()
			{
				//NotifyPropertyChanged(PropertyArgsDisplayName);
				//this.NotifyOwnerUpdated(nameof(Name));
				//this.NotifyOwnerUpdated(nameof(DisplayName));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================