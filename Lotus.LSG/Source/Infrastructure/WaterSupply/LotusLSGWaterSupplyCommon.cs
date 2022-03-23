//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль инженерной инфраструктуры
// Подраздел: Подсистема водоснабжения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGWaterSupplyCommon.cs
*		Общие типы и структуры данных подсистемы водоснабжения.
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
		//! \defgroup MunicipalityInfrastructureWater Подсистема водоснабжения
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityInfrastructure
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Водоснабжение
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CWaterSupply : CEngineeringElement
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsLengthVillage = new PropertyChangedEventArgs(nameof(LengthVillage));
			protected static PropertyChangedEventArgs PropertyArgsLengthTrunk = new PropertyChangedEventArgs(nameof(LengthTrunk));
			protected static PropertyChangedEventArgs PropertyArgsCountSource = new PropertyChangedEventArgs(nameof(CountSource));
			protected static PropertyChangedEventArgs PropertyArgsConsumplationAll = new PropertyChangedEventArgs(nameof(ConsumplationAll));
			protected static PropertyChangedEventArgs PropertyArgsConsumplationDay = new PropertyChangedEventArgs(nameof(ConsumplationDay));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TValueReal mLengthVillage;
			internal TValueReal mLengthTrunk;
			internal TValueInt mCountSource;
			internal Double mConsumplationAll;
			internal Double mConsumplationDay;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Протяжённость внутрепоселковых водопроводных сетей
			/// </summary>
			[DisplayName("Поселковые сети, м")]
			[Description("Протяжённость внутрепоселковых водопроводных сетей")]
			[Category("Основные параметры")]
			//[Display(Name = "Поселковые сети, м", Order = 1, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal LengthVillage
			{
				get { return (mLengthVillage); }
				set
				{
					mLengthVillage = value;
					NotifyPropertyChanged(PropertyArgsLengthVillage);
				}
			}

			/// <summary>
			/// Протяжённость магистральных водопроводных сетей
			/// </summary>
			[DisplayName("Магистральные сети, м")]
			[Description("Протяжённость магистральных водопроводных сетей")]
			[Category("Основные параметры")]
			//[Display(Name = "Магистральные сети, м", Order = 2, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueRealTelerik), "Value")]
			public TValueReal LengthTrunk
			{
				get { return (mLengthTrunk); }
				set
				{
					mLengthTrunk = value;
					NotifyPropertyChanged(PropertyArgsLengthTrunk);
				}
			}

			/// <summary>
			/// Общее количество источников водоснабжения
			/// </summary>
			[DisplayName("Кол-во источников")]
			[Description("Общее количество источников водоснабжения")]
			[Category("Основные параметры")]
			//[Display(Name = "Кол-во источников", Order = 3, GroupName = "2. Основные параметры")]
			[XmlElement]
			//[Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(EditorValueIntTelerik), "Value")]
			public TValueInt CountSource
			{
				get { return (mCountSource); }
				set
				{
					mCountSource = value;
					NotifyPropertyChanged(PropertyArgsCountSource);
				}
			}

			/// <summary>
			/// Общее водопотребление (тыс. м3/сут)
			/// </summary>
			[DisplayName("Водопотребление")]
			[Description("Общее водопотребление")]
			[Category("Основные параметры")]
			//[Display(Name = "Водопотребление", Order = 4, GroupName = "2. Основные параметры")]
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
			/// Среднесуточное водопотребление (л/сут на чел)
			/// </summary>
			[DisplayName("Расход воды")]
			[Description("Среднесуточное водопотребление")]
			[Category("Основные параметры")]
			//[Display(Name = "Расход воды", Order = 4, GroupName = "2. Основные параметры")]
			[XmlAttribute]
			public Double ConsumplationDay
			{
				get { return (mConsumplationDay); }
				set
				{
					mConsumplationDay = value;
					NotifyPropertyChanged(PropertyArgsConsumplationDay);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CWaterSupply()
			{
				mName = "Водоснабжение";
				mEngineeringType = TEngineeringType.WaterSupply;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CWaterSupply(String name)
					: base(name)
			{
				mEngineeringType = TEngineeringType.WaterSupply;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="water_supply">Водоснабжение</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CWaterSupply water_supply)
			{
				LengthVillage += water_supply.LengthVillage;
				LengthTrunk += water_supply.LengthTrunk;
				CountSource += water_supply.CountSource;
				ConsumplationAll += water_supply.ConsumplationAll;
				ConsumplationDay += water_supply.ConsumplationDay;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================