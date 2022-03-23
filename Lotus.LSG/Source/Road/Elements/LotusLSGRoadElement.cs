﻿//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Дорожное хозяйство
// Подраздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRoadElement.cs
*		Общие типы и структуры данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.ComponentModel;
using System.Xml;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityRoadCommon
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип элемента улично-дорожной сети
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TRoadElementType>))]
		public enum TRoadElementType
		{
			/// <summary>
			/// Базовый элемент
			/// </summary>
			[Description("Базовый элемент")]
			Element,

			/// <summary>
			/// Примыкание/пересечение
			/// </summary>
			[Description("Примыкание/пересечение")]
			Junction,

			/// <summary>
			/// Дорожный знак
			/// </summary>
			[Description("Дорожный знак")]
			Sign,

			/// <summary>
			/// Дорожная разметка
			/// </summary>
			[Description("Дорожная разметка")]
			Marking,

			/// <summary>
			/// Элемент дорожного освещения
			/// </summary>
			[Description("Элемент дорожного освещения")]
			Lighting,

			/// <summary>
			/// Элемент дорожного ограждения
			/// </summary>
			[Description("Элемент дорожного ограждения")]
			Fence,

			/// <summary>
			/// Искусственная неровность
			/// </summary>
			[Description("Искусственная неровность")]
			Hump,

			/// <summary>
			/// Автобусная остановка
			/// </summary>
			[Description("Автобусная остановка")]
			Busstop,

			/// <summary>
			/// Пешеходный переход
			/// </summary>
			[Description("Пешеходный переход")]
			Crosswalk,

			/// <summary>
			/// Пешеходный тротуар
			/// </summary>
			[Description("Пешеходный тротуар")]
			Sidewalks,

			/// <summary>
			/// Светофор
			/// </summary>
			[Description("Светофор")]
			TrafficLight
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================