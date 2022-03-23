//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема бюджета и финансов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGBaseFinancingPrice.cs
*		Класс определяющий понятие стоимости.
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
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityBaseFinancing
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий понятие стоимости
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[LotusSerializeData]
		public class CPrice : PropertyChangedBase, IComparable<CPrice>, ILotusSupportViewInspector, ILotusBudgetFinancing
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsBudget = new PropertyChangedEventArgs(nameof(Price));
			protected static readonly PropertyChangedEventArgs PropertyArgsLocalBudget = new PropertyChangedEventArgs(nameof(PriceLocal));
			protected static readonly PropertyChangedEventArgs PropertyArgsRegionalBudget = new PropertyChangedEventArgs(nameof(PriceRegional));
			protected static readonly PropertyChangedEventArgs PropertyArgsFederalBudget = new PropertyChangedEventArgs(nameof(PriceFederal));
			protected static readonly PropertyChangedEventArgs PropertyArgsExtraBudget = new PropertyChangedEventArgs(nameof(PriceExtra));
			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));

			/// <summary>
			/// Данные для сериализации
			/// </summary>
			private static CSerializeData mPriceSerializeData;
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
				if (mPriceSerializeData == null)
				{
					mPriceSerializeData = new CSerializeData(typeof(CPrice));
					mPriceSerializeData.AddProperty(nameof(PriceLocal));
					mPriceSerializeData.AddProperty(nameof(PriceRegional));
					mPriceSerializeData.AddProperty(nameof(PriceFederal));
					mPriceSerializeData.AddProperty(nameof(PriceExtra));
					mPriceSerializeData.AddProperty(nameof(NotCalculation));
				}

				return (mPriceSerializeData);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal String mName;

			// Основные параметры
			internal Decimal mLocalBudget;
			internal Decimal mRegionalBudget;
			internal Decimal mFederalBudget;
			internal Decimal mExtraBudget;

			// Расчеты
			internal Boolean mNotCalculation;

			// Владелей
			internal ILotusOwnerObject mOwner;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Наименование объекта стоимости
			/// </summary>
			[Browsable(false)]
			public String Name
			{
				get { return (mName); }
			}

			/// <summary>
			/// Общая стоимость
			/// </summary>
			[DisplayName("Стоимость")]
			[Description("Общая стоимость")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			public Decimal Price
			{
				get 
				{
					if (mNotCalculation)
					{
						return (0);
					}
					else
					{
						return (mLocalBudget + mRegionalBudget + mFederalBudget + mExtraBudget);
					}
				}
			}

			/// <summary>
			/// Местный бюджет
			/// </summary>
			[DisplayName("Местный бюджет")]
			[Description("Местный бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceLocal
			{
				get { return (mLocalBudget); }
				set
				{
					mLocalBudget = value;
					NotifyPropertyChanged(PropertyArgsLocalBudget);
					RaiseBudgetChanged();
					if (mOwner != null)
					{
						//mOwner.OnNotifyUpdated(this, nameof(Name), nameof(PriceLocal));
					}
				}
			}

			/// <summary>
			/// Областной бюджет
			/// </summary>
			[DisplayName("Областной бюджет")]
			[Description("Областной бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceRegional
			{
				get { return (mRegionalBudget); }
				set
				{
					mRegionalBudget = value;
					NotifyPropertyChanged(PropertyArgsRegionalBudget);
					RaiseBudgetChanged();
					if (mOwner != null)
					{
						//mOwner.OnNotifyUpdated(this, nameof(Name), nameof(PriceRegional));
					}
				}
			}

			/// <summary>
			/// Федеральный бюджет
			/// </summary>
			[DisplayName("Федеральный бюджет")]
			[Description("Федеральный бюджет")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceFederal
			{
				get { return (mFederalBudget); }
				set
				{
					mFederalBudget = value;
					NotifyPropertyChanged(PropertyArgsFederalBudget);
					RaiseBudgetChanged();
					if (mOwner != null)
					{
						//mOwner.OnNotifyUpdated(this, nameof(Name), nameof(PriceFederal));
					}
				}
			}

			/// <summary>
			/// Внебюджетные средства
			/// </summary>
			[DisplayName("Внебюджетные средства")]
			[Description("Внебюджетные средства")]
			[Category(XInspectorGroupDesc.Financing)]
			[LotusNumberFormat(XNumbers.Monetary)]
			[XmlAttribute]
			public Decimal PriceExtra
			{
				get { return (mExtraBudget); }
				set
				{
					mExtraBudget = value;
					NotifyPropertyChanged(PropertyArgsExtraBudget);
					RaiseBudgetChanged();
					if (mOwner != null)
					{
						//mOwner.OnNotifyUpdated(this, nameof(Name), nameof(PriceFederal));
					}
				}
			}

			/// <summary>
			/// Не учитывать
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
					RaiseBudgetChanged();
					if (mOwner != null)
					{
						//mOwner.OnNotifyUpdated(this, nameof(Name), nameof(NotCalculation));
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportViewInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			public String InspectorTypeName
			{
				get { return ("СТОИМОСТЬ"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			[Browsable(false)]
			public virtual String InspectorObjectName
			{
				get
				{
					return (Price.ToString(XNumbers.Monetary));
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPrice()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Наименование объекта стоимости</param>
			/// <param name="owner">Владелец</param>
			//---------------------------------------------------------------------------------------------------------
			public CPrice(String name, ILotusOwnerObject owner = null)
			{
				mName = name;
				mOwner = owner;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="local_buget">Местный бюджет</param>
			//---------------------------------------------------------------------------------------------------------
			public CPrice(Decimal local_buget)
			{
				mLocalBudget = local_buget;
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
			public Int32 CompareTo(CPrice other)
			{
				return (Price.CompareTo(other));
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

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение финансирование мероприятия.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseBudgetChanged()
			{
				NotifyPropertyChanged(PropertyArgsBudget);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
