//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Дорожное хозяйство
// Подраздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRoadInfrastructure.cs
*		Дорожно-транспортная инфраструктура.
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
		//! \addtogroup MunicipalityRoadCommon
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой информацию о дорожной инфраструктуре определённого типа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CRoadInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsTotalLength = new PropertyChangedEventArgs(nameof(TotalLength));
			protected static PropertyChangedEventArgs PropertyArgsAsphalt = new PropertyChangedEventArgs(nameof(Asphalt));
			protected static PropertyChangedEventArgs PropertyArgsCrushedStone = new PropertyChangedEventArgs(nameof(CrushedStone));
			protected static PropertyChangedEventArgs PropertyArgsMacadamGround = new PropertyChangedEventArgs(nameof(MacadamGround));
			protected static PropertyChangedEventArgs PropertyArgsGround = new PropertyChangedEventArgs(nameof(Ground));
			protected static PropertyChangedEventArgs PropertyArgsPlace = new PropertyChangedEventArgs(nameof(Place));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal TRoadPlaceType mPlace;
			internal ObservableCollection<CRoadElement> mRoadElements;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Общая протяжённость автодорог по принадлежности
			/// </summary>
			[DisplayName("Общая протяжённость")]
			[Description("Общая протяжённость автодорог по принадлежности")]
			[Category("Основные параметры")]
			//[Display(Name = "Общая протяжённость", Order = 0, GroupName = "2. Основные параметры")]
			[XmlIgnore]
			public Double TotalLength
			{
				get
				{
					Double result = 0;
					for (Int32 i = 0; i < mRoadElements.Count; i++)
					{
						if (!mRoadElements[i].NotCalculation)
						{
							result += mRoadElements[i].Length;
						}
					}

					return (result);
				}
			}

			/// <summary>
			/// Асфальтобетон
			/// </summary>
			[DisplayName("Асфальтобетон")]
			[Description("Асфальтобетонное покрытие")]
			[Category("Основные параметры")]
			//[Display(Name = "Асфальтобетон", Order = 1, GroupName = "2. Основные параметры")]
			[XmlIgnore]
			public Double Asphalt
			{
				get
				{
					return (GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.Asphalt));
				}
			}

			/// <summary>
			/// Щебеночное
			/// </summary>
			[DisplayName("Щебеночное")]
			[Description("Щебеночное покрытие")]
			[Category("Основные параметры")]
			//[Display(Name = "Щебеночное", Order = 2, GroupName = "2. Основные параметры")]
			[XmlIgnore]
			public Double CrushedStone
			{
				get
				{
					return (GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.CrushedStone));
				}
			}

			/// <summary>
			/// Грунтощебень
			/// </summary>
			[DisplayName("Грунтощебень")]
			[Description("Грунтощебеночное покрытие")]
			[Category("Основные параметры")]
			//[Display(Name = "Грунтощебень", Order = 3, GroupName = "2. Основные параметры")]
			[XmlIgnore]
			public Double MacadamGround
			{
				get
				{
					return (GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.MacadamGround));
				}
			}

			/// <summary>
			/// Грунтовое
			/// </summary>
			[DisplayName("Грунтовое")]
			[Description("Грунтовое покрытие")]
			[Category("Основные параметры")]
			//[Display(Name = "Грунтовое", Order = 4, GroupName = "2. Основные параметры")]
			[XmlIgnore]
			public Double Ground
			{
				get
				{
					return (GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.Ground));
				}
			}

			/// <summary>
			/// Местоположение
			/// </summary>
			[DisplayName("Местоположение")]
			[Description("Местоположение автодорог")]
			[Category("Основные параметры")]
			//[Display(Name = "Местоположение", Order = 5, GroupName = "2. Основные параметры")]
			[XmlAttribute]
			public TRoadPlaceType Place
			{
				get { return (mPlace); }
				set
				{
					mPlace = value;
					NotifyPropertyChanged(PropertyArgsPlace);
				}
			}

			/// <summary>
			/// Список всех автодорог
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CRoadElement> RoadElements
			{
				get { return (mRoadElements); }
				set
				{
					mRoadElements = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CRoadInfrastructure()
			{
				mRoadElements = new ObservableCollection<CRoadElement>();
				mRoadElements.CollectionChanged += OnElementsChanged;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="place">Местоположение дороги</param>
			//---------------------------------------------------------------------------------------------------------
			public CRoadInfrastructure(TRoadPlaceType place)
					: this()
			{
				mPlace = place;
				mName = place.GetDescriptionOrName();
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение элементов
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private void OnElementsChanged(Object sender, NotifyCollectionChangedEventArgs args)
			{
				for (Int32 i = 0; i < mRoadElements.Count; i++)
				{
					mRoadElements[i].RoadInfra = this;
				}
				UpdateData();
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateData()
			{
				NotifyPropertyChanged(PropertyArgsTotalLength);
				NotifyPropertyChanged(PropertyArgsAsphalt);
				NotifyPropertyChanged(PropertyArgsCrushedStone);
				NotifyPropertyChanged(PropertyArgsMacadamGround);
				NotifyPropertyChanged(PropertyArgsGround);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="road_infrastructure">Дорожная инфраструктура</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CRoadInfrastructure road_infrastructure)
			{
				for (Int32 i = 0; i < road_infrastructure.RoadElements.Count; i++)
				{
					RoadElements.Add(road_infrastructure.RoadElements[i]);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление длины автодороги по типу покрытия
			/// </summary>
			/// <param name="coverage_type">Тип покрытия</param>
			/// <returns>Длина автодороги</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType coverage_type)
			{
				Double result = 0;
				for (Int32 i = 0; i < mRoadElements.Count; i++)
				{
					if (mRoadElements[i].NotCalculation) continue;

					if (mRoadElements[i].Coverage == coverage_type && mRoadElements[i].StatusUrban != TStatusUrban.Planned)
					{
						result += mRoadElements[i].Length;
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление длины автодороги по типу покрытия
			/// </summary>
			/// <param name="coverage_type">Тип покрытия</param>
			/// <returns>Длина автодороги</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetTotalLengthFromCoverageTypePlanned(TRoadCoverageType coverage_type)
			{
				Double result = 0;
				for (Int32 i = 0; i < mRoadElements.Count; i++)
				{
					if (mRoadElements[i].NotCalculation) continue;

					if (mRoadElements[i].Coverage == coverage_type && mRoadElements[i].StatusUrban == TStatusUrban.Abolished)
					{
						result -= mRoadElements[i].Length;
					}
					if (mRoadElements[i].Coverage == coverage_type && mRoadElements[i].StatusUrban != TStatusUrban.Abolished)
					{
						result += mRoadElements[i].Length;
					}
				}

				return (result);
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ЭЛЕМЕНТАМИ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение источника данных связанного с этим объектом
			/// </summary>
			/// <returns>Источник данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Object GetItemSource()
			{
				return (mRoadElements);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание и добавление созданного элемента в список дочерних элементов
			/// </summary>
			/// <remarks>
			/// Происходит создание элемента указанного типа и добавление его в список дочерних элементов
			/// </remarks>
			/// <returns>Структурный элемент документа</returns>
			//---------------------------------------------------------------------------------------------------------
			public override CUrbanPlanningItem AddChildNewElement()
			{
				CRoadElement road_element = new CRoadElement("Дорога", mPlace);
				road_element.RoadInfra = this;
				mRoadElements.Add(road_element);
				return (road_element);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление существующего элемента
			/// </summary>
			/// <remarks>
			/// Происходит только добавление существующего элемента. Если элемент принадлежит другому элементу,
			/// то элемент будет сначала удален из его списка
			/// </remarks>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности добавления</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean AddChildExistingElement(CUrbanPlanningItem element)
			{
				CRoadElement road_element = element as CRoadElement;
				if (road_element != null)
				{
					road_element.RoadInfra = this;
					mRoadElements.Add(road_element);
					return (true);
				}
				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из списка дочерних элементов
			/// </summary>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean RemoveChildElement(CUrbanPlanningItem element)
			{
				CRoadElement road_element = element as CRoadElement;
				if (road_element != null)
				{
					mRoadElements.Remove(road_element);
					return (true);
				}
				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка дочерних элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void SortChildElements()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Группировка дочерних элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void GroupChildElements()
			{

			}
			#endregion

			#region ======================================= МЕТОДЫ СЕРИАЛИЗАЦИИ =======================================
			//-------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей
			/// </summary>
			/// <param name="parent">Родительский объект</param>
			//-------------------------------------------------------------------------------------------------------------
			public override void OnUpdateLink(CUrbanPlanningItem parent)
			{
				for (Int32 i = 0; i < mRoadElements.Count; i++)
				{
					mRoadElements[i].OnUpdateLink(this);
				}
				UpdateData();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================