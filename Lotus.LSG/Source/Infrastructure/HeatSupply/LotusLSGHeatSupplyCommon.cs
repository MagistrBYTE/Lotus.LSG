//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль инженерной инфраструктуры
// Подраздел: Подсистема теплоснабжения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGHeatSupplyCommon.cs
*		Общие типы и структуры данных подсистемы теплоснабжения.
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
		//! \defgroup MunicipalityInfrastructureHeat Подсистема теплоснабжения
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityInfrastructure
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Теплоснабжение
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CHeatSupply : CEngineeringElement
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsLength = new PropertyChangedEventArgs(nameof(Length));
			protected static PropertyChangedEventArgs PropertyArgsCountStationCenter = new PropertyChangedEventArgs(nameof(CountStationCenter));
			protected static PropertyChangedEventArgs PropertyArgsCountStationLocal = new PropertyChangedEventArgs(nameof(CountStationLocal));
			protected static PropertyChangedEventArgs PropertyArgsConsumplation = new PropertyChangedEventArgs(nameof(Consumplation));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TValueReal mLength;
			internal TValueInt mCountStationCenter;
			internal TValueInt mCountStationLocal;
			internal Double mConsumplation;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Протяжённость сетей централизованного теплоснабжения
			/// </summary>
			[DisplayName("Сети теплоснабжения, м")]
			[Description("Протяжённость сетей централизованного теплоснабжения")]
			[Category("Основные параметры")]
			//[Display(Name = "Сети теплоснабжения, м", Order = 1, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal Length
			{
				get { return (mLength); }
				set
				{
					mLength = value;
					NotifyPropertyChanged(PropertyArgsLength);
				}
			}

			/// <summary>
			/// Количество централизованных источников теплоснабжения
			/// </summary>
			[DisplayName("Кол-во центр. источников")]
			[Description("Количество централизованных источников теплоснабжения")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во центр. источников", Order = 2, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueIntTelerik), "Value")]
			public TValueInt CountStationCenter
			{
				get { return (mCountStationCenter); }
				set
				{
					mCountStationCenter = value;
					NotifyPropertyChanged(PropertyArgsCountStationCenter);
				}
			}

			/// <summary>
			/// Количество автономных источников теплоснабжения
			/// </summary>
			[DisplayName("Кол-во автон. источников")]
			[Description("Количество автономных источников теплоснабжения")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во автон. источников", Order = 3, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueIntTelerik), "Value")]
			public TValueInt CountStationLocal
			{
				get { return (mCountStationLocal); }
				set
				{
					mCountStationLocal = value;
					NotifyPropertyChanged(PropertyArgsCountStationLocal);
				}
			}

			/// <summary>
			/// Потребление тепла (Гкал/год)
			/// </summary>
			[DisplayName("Теплопотребление")]
			[Description("Потребление тепла (Гкал/год)")]
			[Category("Основные параметры")]
			//[Display(Name = "Теплопотребление", Order = 4, GroupName = "2. Основные параметры")]
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
			public CHeatSupply()
			{
				mName = "Теплоснабжение";
				mEngineeringType = TEngineeringType.HeatSupply;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CHeatSupply(String name)
					: base(name)
			{
				mEngineeringType = TEngineeringType.HeatSupply;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="heat_supply">Теплоснабжение</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CHeatSupply heat_supply)
			{
				Length += heat_supply.Length;
				CountStationCenter += heat_supply.CountStationCenter;
				CountStationLocal += heat_supply.CountStationLocal;
				Consumplation += heat_supply.Consumplation;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================