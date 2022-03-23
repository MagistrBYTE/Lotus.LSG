//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusClientTemplate
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusISUDElement.cs
*		Элемент документа ИСОГД.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace ISUD
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityISUD
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Документ ИСОГД
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CISUDElement : CNameableID, IEquatable<CISUDElement>, IComparable<CISUDElement>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsIsPresent = new PropertyChangedEventArgs(nameof(IsPresent));
			protected static PropertyChangedEventArgs PropertyArgsTerritoryCode = new PropertyChangedEventArgs(nameof(TerritoryCode));
			protected static PropertyChangedEventArgs PropertyArgsFullName = new PropertyChangedEventArgs(nameof(FullName));
			protected static PropertyChangedEventArgs PropertyArgsTerritoryAction = new PropertyChangedEventArgs(nameof(TerritoryAction));
			protected static PropertyChangedEventArgs PropertyArgsDocumentCode = new PropertyChangedEventArgs(nameof(DocumentCode));
			protected static PropertyChangedEventArgs PropertyArgsApprovedSubject = new PropertyChangedEventArgs(nameof(ApprovedSubject));
			protected static PropertyChangedEventArgs PropertyArgsDateOfApproval = new PropertyChangedEventArgs(nameof(DateOfApproval));
			protected static PropertyChangedEventArgs PropertyArgsDateOfEntry = new PropertyChangedEventArgs(nameof(DateOfEntry));
			protected static PropertyChangedEventArgs PropertyArgsRegistrationNumber = new PropertyChangedEventArgs(nameof(RegistrationNumber));
			protected static PropertyChangedEventArgs PropertyArgsStatusAction = new PropertyChangedEventArgs(nameof(StatusAction));
			protected static PropertyChangedEventArgs PropertyArgsSpecialist = new PropertyChangedEventArgs(nameof(Specialist));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Boolean mIsPresent;
			internal String mTerritoryCode;
			internal String mFullName;
			internal String mTerritoryAction;
			internal String mDocumentCode;
			internal String mApprovedSubject;
			internal DateTime mDateOfApproval;
			internal DateTime mDateOfEntry;
			internal String mRegistrationNumber;
			internal Boolean mStatusAction;
			internal String mSpecialist;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Номер документа
			/// </summary>
			[Browsable(false)]
			public Boolean IsPresent
			{
				get { return (mIsPresent); }
				set
				{
					mIsPresent = value;
					NotifyPropertyChanged(PropertyArgsIsPresent);
				}
			}

			/// <summary>
			/// Номер документа
			/// </summary>
			[DisplayName("Код территории")]
			[Category("2) Основные параметры")]
			[Description("Код территории действия документа")]
			public String TerritoryCode
			{
				get { return (mTerritoryCode); }
				set
				{
					mTerritoryCode = value;
					NotifyPropertyChanged(PropertyArgsTerritoryCode);
					RaiseTerritoryCodeChanged();
				}
			}

			/// <summary>
			/// Полное имя документа
			/// </summary>
			[DisplayName("Полное имя документа")]
			[Category("2) Основные параметры")]
			[Description("Полное имя документа")]
			public String FullName
			{
				get { return (mFullName); }
				set
				{
					mFullName = value;
					NotifyPropertyChanged(PropertyArgsFullName);
					RaiseFullNameChanged();
				}
			}

			/// <summary>
			/// Территория действия документа
			/// </summary>
			[DisplayName("Территория действия")]
			[Category("2) Основные параметры")]
			[Description("Территория действия документа")]
			public String TerritoryAction
			{
				get { return (mTerritoryAction); }
				set
				{
					mTerritoryAction = value;
					NotifyPropertyChanged(PropertyArgsTerritoryAction);
					RaiseTerritoryActionChanged();
				}
			}

			/// <summary>
			/// Код документа
			/// </summary>
			[DisplayName("Код документа")]
			[Category("2) Основные параметры")]
			[Description("Код документа по таблицы классификатора документов")]
			public String DocumentCode
			{
				get { return (mDocumentCode); }
				set
				{
					mDocumentCode = value;
					NotifyPropertyChanged(PropertyArgsDocumentCode);
					RaiseDocumentCodeChanged();
				}
			}

			/// <summary>
			/// Утвердивший орган
			/// </summary>
			[DisplayName("Утвердивший орган")]
			[Category("2) Основные параметры")]
			[Description("Орган власти утвердивший данный документ")]
			public String ApprovedSubject
			{
				get { return (mApprovedSubject); }
				set
				{
					mApprovedSubject = value;
					NotifyPropertyChanged(PropertyArgsApprovedSubject);
					RaiseApprovedSubjectChanged();
				}
			}

			/// <summary>
			/// Дата утверждения документа
			/// </summary>
			[DisplayName("Дата утверждения")]
			[Category("2) Основные параметры")]
			[Description("Дата утверждения документа")]
			public DateTime DateOfApproval
			{
				get { return (mDateOfApproval); }
				set
				{
					mDateOfApproval = value;
					NotifyPropertyChanged(PropertyArgsDateOfApproval);
					RaiseDateOfApprovalChanged();
				}
			}

			/// <summary>
			/// Дата внесения
			/// </summary>
			[DisplayName("Дата внесения")]
			[Category("2) Основные параметры")]
			[Description("Дата внесения документа в ИСОГД")]
			public DateTime DateOfEntry
			{
				get { return (mDateOfEntry); }
				set
				{
					mDateOfEntry = value;
					NotifyPropertyChanged(PropertyArgsDateOfEntry);
					RaiseDateOfEntryChanged();
				}
			}

			/// <summary>
			/// Регистрационный номер
			/// </summary>
			[DisplayName("Регистрационный номер")]
			[Category("2) Основные параметры")]
			[Description("Регистрационный номер в ИСОГД")]
			public String RegistrationNumber
			{
				get { return (mRegistrationNumber); }
				set
				{
					mRegistrationNumber = value;
					NotifyPropertyChanged(PropertyArgsRegistrationNumber);
					RaiseRegistrationNumberChanged();
				}
			}

			/// <summary>
			/// Статус действия
			/// </summary>
			[DisplayName("Действующий")]
			[Category("2) Основные параметры")]
			[Description("Статус действия документа")]
			public Boolean StatusAction
			{
				get { return (mStatusAction); }
				set
				{
					mStatusAction = value;
					NotifyPropertyChanged(PropertyArgsStatusAction);
					RaiseStatusActionChanged();
				}
			}

			/// <summary>
			/// Специалист
			/// </summary>
			[DisplayName("Специалист")]
			[Category("2) Основные параметры")]
			[Description("Специалист внёсший документ в ИСОГД")]
			public String Specialist
			{
				get { return (mSpecialist); }
				set
				{
					mSpecialist = value;
					NotifyPropertyChanged(PropertyArgsSpecialist);
					RaiseSpecialistChanged();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CISUDElement()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя документа</param>
			//---------------------------------------------------------------------------------------------------------
			public CISUDElement(String name)
				:base(name)
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="other">Сравниваемый документ ИСОГД</param>
			/// <returns>Статус равенства документов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(CISUDElement other)
			{
				return (ID == other.ID);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение документов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый документ ИСОГД</param>
			/// <returns>Статус сравнения документов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CISUDElement other)
			{
				return (0);
			}
			#endregion

			#region ======================================= CЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение номера документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseTerritoryCodeChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseFullNameChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseTerritoryActionChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseDocumentCodeChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseApprovedSubjectChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseDateOfApprovalChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseDateOfEntryChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseRegistrationNumberChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseStatusActionChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса печати документа.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void RaiseSpecialistChanged()
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion

			#region ======================================= МЕТОДЫ ОТОБРАЖЕНИЯ ========================================
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ЭЛЕМЕНТАМИ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание нового элемента документа
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента указанного типа.
			/// Сам элемент НЕ добавляется в список элементов данного документа
			/// </remarks>
			/// <param name="type_name">Имя типа элемента</param>
			/// <param name="element_name">Имя элемента</param>
			/// <returns>Элемент документа</returns>
			//---------------------------------------------------------------------------------------------------------
			//public override IBaseElement CreateChildElement(String type_name, String element_name)
			//{
			//	CISUDElement element = new CISUDElement(element_name);
			//	return (element);
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов документа
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void SortChildElements()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Группировка элементов документа
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void GroupChildElements()
			{

			}
			#endregion

			#region ======================================= ОБРАБОТЧИКИ СОБЫТИЙ =======================================
			#endregion

			#region ======================================= МЕТОДЫ СЕРИАЛИЗАЦИИ =======================================
			////---------------------------------------------------------------------------------------------------------
			///// <summary>
			///// Запись свойств и данных документа в формат атрибутов XML
			///// </summary>
			///// <param name="xml_writer">Средство записи данных в формат XML</param>
			////---------------------------------------------------------------------------------------------------------
			//protected override void WriteBaseElementToAttribute(XmlWriter xml_writer)
			//{
			//	xml_writer.WriteStartElement("ISUDElement");
			//	xml_writer.WriteStringToAttribute("Name", Name);
			//	xml_writer.WriteStringToAttribute("Number", Number);
			//	xml_writer.WriteStringToAttribute("Group", Group);
			//	xml_writer.WriteLongToAttribute("ID", ID);

			//	xml_writer.WriteStringToAttribute("TerritoryCode", TerritoryCode);
			//	xml_writer.WriteStringToAttribute("FullName", FullName);
			//	xml_writer.WriteStringToAttribute("TerritoryAction", TerritoryAction);
			//	xml_writer.WriteStringToAttribute("DocumentCode", DocumentCode);
			//	xml_writer.WriteStringToAttribute("ApprovedSubject", ApprovedSubject);

			//	xml_writer.WriteDateTimeAttribute("DateOfApproval", DateOfApproval);
			//	xml_writer.WriteDateTimeAttribute("DateOfEntry", DateOfEntry);

			//	xml_writer.WriteStringToAttribute("RegistrationNumber", RegistrationNumber);
			//	xml_writer.WriteBooleanToAttribute("StatusAction", StatusAction);
			//	xml_writer.WriteStringToAttribute("Specialist", Specialist);
			//}

			////---------------------------------------------------------------------------------------------------------
			///// <summary>
			///// Чтение свойств и данных документа из формата атрибутов XML
			///// </summary>
			///// <param name="xml_reader">Средство чтения данных формата XML</param>
			////---------------------------------------------------------------------------------------------------------
			//protected override void ReadBaseElementFromAttribute(XmlReader xml_reader)
			//{
			//	Name = xml_reader.ReadStringFromAttribute("Name", Name);
			//	Number = xml_reader.ReadStringFromAttribute("Number", Number);
			//	Group = xml_reader.ReadStringFromAttribute("Group", Group);
			//	ID = xml_reader.ReadLongFromAttribute("ID", ID);

			//	mTerritoryCode = xml_reader.ReadStringFromAttribute("TerritoryCode", mTerritoryCode);
			//	mFullName = xml_reader.ReadStringFromAttribute("FullName", mFullName);
			//	mTerritoryAction = xml_reader.ReadStringFromAttribute("TerritoryAction", mTerritoryAction);
			//	mDocumentCode = xml_reader.ReadStringFromAttribute("DocumentCode", mDocumentCode);
			//	mApprovedSubject = xml_reader.ReadStringFromAttribute("ApprovedSubject", mApprovedSubject);

			//	mDateOfApproval = xml_reader.ReadDateTimeFromAttribute("DateOfApproval");
			//	mDateOfEntry = xml_reader.ReadDateTimeFromAttribute("DateOfEntry");

			//	mRegistrationNumber = xml_reader.ReadStringFromAttribute("RegistrationNumber", mRegistrationNumber);
			//	mStatusAction = xml_reader.ReadBooleanFromAttribute("StatusAction", mStatusAction);
			//	mSpecialist = xml_reader.ReadStringFromAttribute("Specialist", mSpecialist);
			//}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================