//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль инженерной инфраструктуры
// Подраздел: Подсистема электроснабжения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGPowerSupplyCommon.cs
*		Общие типы и структуры данных подсистемы электроснабжения.
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
		//! \defgroup MunicipalityInfrastructurePower Подсистема электроснабжения
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityInfrastructure
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Электроснабжение
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CPowerSupply : CEngineeringElement
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsLengthLow = new PropertyChangedEventArgs(nameof(LengthLow));
			protected static PropertyChangedEventArgs PropertyArgsLengthMiddle = new PropertyChangedEventArgs(nameof(LengthMiddle));
			protected static PropertyChangedEventArgs PropertyArgsLengthHigh = new PropertyChangedEventArgs(nameof(LengthHigh));
			protected static PropertyChangedEventArgs PropertyArgsCountSubstation = new PropertyChangedEventArgs(nameof(CountSubstation));
			protected static PropertyChangedEventArgs PropertyArgsConsumplationAll = new PropertyChangedEventArgs(nameof(ConsumplationAll));
			protected static PropertyChangedEventArgs PropertyArgsConsumplationPerson = new PropertyChangedEventArgs(nameof(ConsumplationPerson));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TValueReal mLengthLow;
			internal TValueReal mLengthMiddle;
			internal TValueReal mLengthHigh;
			internal TValueInt mCountSubstation;
			internal Double mConsumplationAll;
			internal Double mConsumplationPerson;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Протяжённость линий электропередач низкого напряжения (до 10 кВ)
			/// </summary>
			[DisplayName("ЛЭП (до 10 кВ), м")]
			[Description("Протяжённость линий электропередач низкого напряжения (до 10 кВ)")]
			[Category("Основные параметры")]
			//[Display(Name = "ЛЭП (до 10 кВ), м", Order = 1, GroupName = "2. Основные параметры")]
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
			/// Протяжённость линий электропередач среднего напряжения (10-35 кВ)
			/// </summary>
			[DisplayName("ЛЭП (10-35 кВ), м")]
			[Description("Протяжённость линий электропередач среднего напряжения (10-35 кВ)")]
			[Category("Основные параметры")]
			//[Display(Name = "ЛЭП (10-35 кВ), м", Order = 2, GroupName = "2. Основные параметры")]
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
			/// Протяжённость линий электропередач высокого напряжения (100-1100 кВ)
			/// </summary>
			[DisplayName("ЛЭП (110-1000 кВ), м")]
			[Description("Протяжённость линий электропередач высокого напряжения (100-1100 кВ)")]
			[Category("Основные параметры")]
			//[Display(Name = "ЛЭП (110-1000 кВ), м", Order = 3, GroupName = "2. Основные параметры")]
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
			/// Количество подстанций
			/// </summary>
			[DisplayName("Кол-во подстанций")]
			[Description("Количество подстанций")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во подстанций", Order = 4, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueIntTelerik), "Value")]
			public TValueInt CountSubstation
			{
				get { return (mCountSubstation); }
				set
				{
					mCountSubstation = value;
					NotifyPropertyChanged(PropertyArgsCountSubstation);
				}
			}

			/// <summary>
			/// Потребность в электроэнергии (млн. кВт. ч. /в год)
			/// </summary>
			[DisplayName("Энергопотребление")]
			[Description("Потребность в электроэнергии (млн. кВт. ч. /в год)")]
			[Category("Основные параметры")]
			//[Display(Name = "Энергопотребление", Order = 5, GroupName = "2. Основные параметры")]
			[XmlAttribute]
			public Double ConsumplationAll
			{
				get { return (mConsumplationAll); }
				set
				{
					mConsumplationAll = value;
					NotifyPropertyChanged(PropertyArgsConsumplationAll);
				}
			}

			/// <summary>
			/// Потребление электроэнергии на 1 чел. в год (кВт. ч.)
			/// </summary>
			[DisplayName("Расход электроэнергии")]
			[Description("Потребление электроэнергии на 1 чел. в год (кВт. ч.)")]
			[Category("Основные параметры")]
			//[Display(Name = "Расход электроэнергии", Order = 6, GroupName = "2. Основные параметры")]
			[XmlAttribute]
			public Double ConsumplationPerson
			{
				get { return (mConsumplationPerson); }
				set
				{
					mConsumplationPerson = value;
					NotifyPropertyChanged(PropertyArgsConsumplationPerson);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPowerSupply()
			{
				mName = "Электроснабжение";
				mEngineeringType = TEngineeringType.PowerSupply;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CPowerSupply(String name)
					: base(name)
			{
				mEngineeringType = TEngineeringType.PowerSupply;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="power_supply">Электроснабжение</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CPowerSupply power_supply)
			{
				LengthLow += power_supply.LengthLow;
				LengthMiddle += power_supply.LengthMiddle;
				LengthHigh += power_supply.LengthHigh;
				CountSubstation += power_supply.CountSubstation;
				ConsumplationAll += power_supply.ConsumplationAll;
				ConsumplationPerson += power_supply.ConsumplationPerson;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================