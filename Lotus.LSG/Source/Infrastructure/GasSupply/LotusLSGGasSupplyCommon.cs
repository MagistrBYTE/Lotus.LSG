//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль инженерной инфраструктуры
// Подраздел: Подсистема газоснабжения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGGasSupplyCommon.cs
*		Общие типы и структуры данных подсистемы газоснабжения.
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
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityInfrastructureGas Подсистема газоснабжения
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityInfrastructure
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Газоснабжение
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CGasSupply : CEngineeringElement
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsLengthLow = new PropertyChangedEventArgs(nameof(LengthLow));
			protected static PropertyChangedEventArgs PropertyArgsLengthMiddle = new PropertyChangedEventArgs(nameof(LengthMiddle));
			protected static PropertyChangedEventArgs PropertyArgsLengthHigh = new PropertyChangedEventArgs(nameof(LengthHigh));
			protected static PropertyChangedEventArgs PropertyArgsCountStation = new PropertyChangedEventArgs(nameof(CountStation));
			protected static PropertyChangedEventArgs PropertyArgsConsumplation = new PropertyChangedEventArgs(nameof(Consumplation));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TValueReal mLengthLow;
			internal TValueReal mLengthMiddle;
			internal TValueReal mLengthHigh;
			internal TValueInt mCountStation;
			internal Double mConsumplation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Протяжённость газопровода низкого давления
			/// </summary>
			[DisplayName("Газопровод низкого давления, м")]
			[Description("Протяжённость газопровода низкого давления")]
			[Category("Основные параметры")]
			//[Display(Name = "Газопровод низкого давления, м", Order = 1, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal LengthLow
			{
				get { return (mLengthLow); }
				set
				{
					mLengthLow = value;
					NotifyPropertyChanged(PropertyArgsLengthLow);
				}
			}

			/// <summary>
			/// Протяжённость газопровода среднего давления
			/// </summary>
			[DisplayName("Газопровод среднего давления, м")]
			[Description("Протяжённость газопровода среднего давления")]
			[Category("Основные параметры")]
			//[Display(Name = "Газопровод среднего давления, м", Order = 2, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal LengthMiddle
			{
				get { return (mLengthMiddle); }
				set
				{
					mLengthMiddle = value;
					NotifyPropertyChanged(PropertyArgsLengthMiddle);
				}
			}

			/// <summary>
			/// Протяжённость газопровода высокого давления
			/// </summary>
			[DisplayName("Газопровод высокого давления, м")]
			[Description("Протяжённость газопровода высокого давления")]
			[Category("Основные параметры")]
			//[Display(Name = "Газопровод высокого давления, м", Order = 3, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal LengthHigh
			{
				get { return (mLengthHigh); }
				set
				{
					mLengthHigh = value;
					NotifyPropertyChanged(PropertyArgsLengthMiddle);
				}
			}

			/// <summary>
			/// Количество газорегуляторных пунктов
			/// </summary>
			[DisplayName("Кол-во ГРП")]
			[Description("Количество газорегуляторных пунктов")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во ГРП", Order = 4, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueIntTelerik), "Value")]
			public TValueInt CountStation
			{
				get { return (mCountStation); }
				set
				{
					mCountStation = value;
					NotifyPropertyChanged(PropertyArgsCountStation);
				}
			}

			/// <summary>
			/// Потребность газа (млн. м3/год)
			/// </summary>
			[DisplayName("Газопотребление")]
			[Description("Потребность газа (млн. м3/год)")]
			[Category("Основные параметры")]
			//[Display(Name = "Газопотребление", Order = 5, GroupName = "2. Основные параметры")]
			[XmlAttribute]
			public Double Consumplation
			{
				get { return (mConsumplation); }
				set
				{
					mConsumplation = value;
					NotifyPropertyChanged(PropertyArgsConsumplation);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CGasSupply()
			{
				mName = "Газоснабжение";
				mEngineeringType = TEngineeringType.GasSupply;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CGasSupply(String name)
					: base(name)
			{
				mEngineeringType = TEngineeringType.GasSupply;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="gas_supply">Газоснабжение</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CGasSupply gas_supply)
			{
				LengthLow += gas_supply.LengthLow;
				LengthMiddle += gas_supply.LengthMiddle;
				LengthHigh += gas_supply.LengthHigh;
				CountStation += gas_supply.CountStation;
				Consumplation += gas_supply.Consumplation;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================