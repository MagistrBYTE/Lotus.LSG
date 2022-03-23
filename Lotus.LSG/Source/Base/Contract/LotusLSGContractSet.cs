//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема представления контрактов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGContractSet.cs
*		Набор контрактов.
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
#if USE_WINDOWS
using Lotus.Windows;
#endif
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityBaseContract
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Набор контрактов
		/// </summary>
		/// <remarks>
		/// Применятся как средство группирования контрактов
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CContractSet : ListArray<ILotusContractData>, ILotusContractData, ILotusSupportViewInspector
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static readonly PropertyChangedEventArgs PropertyArgsId = new PropertyChangedEventArgs(nameof(Id));
			protected static readonly PropertyChangedEventArgs PropertyArgsPrice = new PropertyChangedEventArgs(nameof(Price));
			protected static readonly PropertyChangedEventArgs PropertyArgsPriceLocal = new PropertyChangedEventArgs(nameof(PriceLocal));
			protected static readonly PropertyChangedEventArgs PropertyArgsRegionalBudget = new PropertyChangedEventArgs(nameof(PriceRegional));
			protected static readonly PropertyChangedEventArgs PropertyArgsFederalBudget = new PropertyChangedEventArgs(nameof(PriceFederal));
			protected static readonly PropertyChangedEventArgs PropertyArgsExtraBudget = new PropertyChangedEventArgs(nameof(PriceExtra));
			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVerified = new PropertyChangedEventArgs(nameof(IsVerified));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mName = "";
			protected internal Int64 mId;

			// Расчеты
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsVerified;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наименование объекта
			/// </summary>
			public String Name
			{
				get { return (mName); }
				set
				{
					mName = value;
					NotifyPropertyChanged(PropertyArgsName);
				}
			}

			/// <summary>
			/// Уникальный идентификатор объекта
			/// </summary>
			[Browsable(false)]
			public Int64 Id
			{
				get { return (mId); }
				set
				{
					mId = value;
					NotifyPropertyChanged(PropertyArgsId);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusBudgetFinancing ============================
			//
			// ФИНАНСИРОВАНИЕ
			//
			/// <summary>
			/// Финансирование
			/// </summary>
			[DisplayName("Стоимость")]
			[LotusPropertyOrder(0)]
			[Description("Общая стоимость мероприятия")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[LotusCategoryOrder(2)]
			[XmlIgnore]
			public Decimal Price
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < mCount; i++)
					{
						if (mArrayOfItems[i].NotCalculation == false)
						{
							result += mArrayOfItems[i].Price;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Местный бюджет
			/// </summary>
			[DisplayName("Местный бюджет")]
			[LotusPropertyOrder(1)]
			[Description("Местный бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal PriceLocal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < mCount; i++)
					{
						if (mArrayOfItems[i].NotCalculation == false)
						{
							result += mArrayOfItems[i].PriceLocal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Областной бюджет
			/// </summary>
			[DisplayName("Областной бюджет")]
			[LotusPropertyOrder(2)]
			[Description("Областной бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal PriceRegional
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < mCount; i++)
					{
						if (mArrayOfItems[i].NotCalculation == false)
						{
							result += mArrayOfItems[i].PriceRegional;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			[DisplayName("Федеральный бюджет")]
			[LotusPropertyOrder(3)]
			[Description("Федеральный бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal PriceFederal
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < mCount; i++)
					{
						if (mArrayOfItems[i].NotCalculation == false)
						{
							result += mArrayOfItems[i].PriceFederal;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			[DisplayName("Внебюджетные средства")]
			[LotusPropertyOrder(4)]
			[Description("Внебюджетные средства")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlIgnore]
			public Decimal PriceExtra
			{
				get
				{
					Decimal result = 0;
					for (Int32 i = 0; i < mCount; i++)
					{
						if (mArrayOfItems[i].NotCalculation == false)
						{
							result += mArrayOfItems[i].PriceExtra;
						}
					}

					return (result);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusNotCalculation =============================
			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusVerified ===================================
			/// <summary>
			/// Статус верификации объекта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Boolean IsVerified
			{
				get { return (mIsVerified); }
				set
				{
					mIsVerified = value;
					NotifyPropertyChanged(PropertyArgsNotCalculation);
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportEditInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			public String InspectorTypeName
			{
				get { return ("НАБОР"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			public String InspectorObjectName
			{
				get
				{
					if (String.IsNullOrEmpty(mName))
					{
						return ("<Без имени>");
					}
					else
					{
						return (mName);
					}
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CContractSet()
			{
				IsNotify = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CContractSet(String name)
				: this()
			{
				mName = name;
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
			public Int32 CompareTo(CContractSet other)
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
				CContractSet clone = new CContractSet();
				clone.mName = mName;

				//for (Int32 i = 0; i < mArrayOfItems.Count; i++)
				//{
				//	clone.AddExistingModel(mArrayOfItems[i].Clone() as ILotusContractData);
				//}

				//clone.mNotCalculation = mNotCalculation;
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

		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
