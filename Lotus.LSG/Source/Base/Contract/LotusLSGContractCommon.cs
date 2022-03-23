﻿//=====================================================================================================================
// Проект: Lotus.LSG
// Раздел: Базовый модуль
// Подраздел: Подсистема представления контрактов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGContractCommon.cs
*		Общие типы и структуры данных для подсистемы представления контрактов.
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
		//! \defgroup MunicipalityBaseContract Подсистема представления контрактов
		//! Подсистема представления контракта и его структурных частей
		//! \ingroup MunicipalityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения данных по контракту
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusContractData : ILotusNameable, ILotusIdentifierId, ILotusBudgetFinancing, ILotusNotCalculation, ILotusVerified
		{

		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================