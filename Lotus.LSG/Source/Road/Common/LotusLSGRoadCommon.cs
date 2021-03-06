//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Дорожное хозяйство
// Подраздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRoadCommon.cs
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
		//! \defgroup MunicipalityRoad Модуль дорожного хозяйства
		//! Базовый модуль определяет общие данные для общей цифровизации управления
		//! \ingroup Municipality
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup MunicipalityRoadCommon Общая подсистема
		//! Общие данные и концепции характерные для различных полномочий
		//! \ingroup MunicipalityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Местоположение дороги
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TRoadPlaceType>))]
		public enum TRoadPlaceType
		{
			/// <summary>
			/// Внутрепоселковая дорога
			/// </summary>
			[Description("Внутрепоселковая")]
			Inside,

			/// <summary>
			/// Межпоселковая дорога
			/// </summary>
			[Description("Межпоселковая")]
			Between,

			/// <summary>
			/// Региональная
			/// </summary>
			[Description("Региональная")]
			Region
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип дорожного покрытия
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TRoadCoverageType>))]
		public enum TRoadCoverageType
		{
			/// <summary>
			/// Асфальтобетон
			/// </summary>
			[Description("Асфальтобетон")]
			Asphalt,

			/// <summary>
			/// Щебеночное
			/// </summary>
			[Description("Щебеночное")]
			CrushedStone,

			/// <summary>
			/// Грунтощебень
			/// </summary>
			[Description("Грунтощебень")]
			MacadamGround,

			/// <summary>
			/// Грунтовое
			/// </summary>
			[Description("Грунтовое")]
			Ground
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Категория дороги
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TRoadCategoryType>))]
		public enum TRoadCategoryType
		{
			/// <summary>
			/// Пятая категория
			/// </summary>
			[Description("V")]
			V,

			/// <summary>
			/// Четвертая категория
			/// </summary>
			[Description("VI")]
			VI,

			/// <summary>
			/// Третья категория
			/// </summary>
			[Description("III")]
			III
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интенсивность движение на дороги
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TRoadIntensityMode>))]
		public enum TRoadIntensityMode
		{
			/// <summary>
			/// Низкая интенсивность
			/// </summary>
			[Description("Низкая")]
			Low,

			/// <summary>
			/// Средняя интенсивность
			/// </summary>
			[Description("Средняя")]
			Middle,

			/// <summary>
			/// Высокая интенсивность
			/// </summary>
			[Description("Высокая")]
			Hight
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================