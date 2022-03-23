//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль градостроительства
// Подраздел: Подсистема земельных отношений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGUrbanPlanningLandTransfer.cs
*		Определение объектов перевода.
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
		//! \addtogroup MunicipalityPlanLand
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Объект перевода
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CTransfer : CUrbanPlanningItemPoint, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsFromCategory = new PropertyChangedEventArgs(nameof(FromCategory));
			protected static PropertyChangedEventArgs PropertyArgsToCategory = new PropertyChangedEventArgs(nameof(ToCategory));
			protected static PropertyChangedEventArgs PropertyArgsToGroup = new PropertyChangedEventArgs(nameof(ToGroup));
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			protected static PropertyChangedEventArgs PropertyArgsPurposes = new PropertyChangedEventArgs(nameof(Purposes));
			protected static PropertyChangedEventArgs PropertyArgsJustification = new PropertyChangedEventArgs(nameof(Justification));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TLandCategory mFromCategory;
			internal TLandCategory mToCategory;
			internal String mToGroup;
			internal Double mArea;
			internal String mPurposes;
			internal String mJustification;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Существующая категория земли
			/// </summary>
			[DisplayName("Существующая категория")]
			[Description("Существующая категория земли территории")]
			[Category("Основные параметры")]
			//[Display(Name = "Существующая категория", Order = 0, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public TLandCategory FromCategory
			{
				get { return (mFromCategory); }
				set
				{
					mFromCategory = value;
					NotifyPropertyChanged(PropertyArgsFromCategory);
				}
			}

			/// <summary>
			/// Планируемая категория земли
			/// </summary>
			[DisplayName("Планируемая категория")]
			[Description("Планируемая категория земли территории")]
			[Category("Основные параметры")]
			//[Display(Name = "Планируемая категория", Order = 1, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public TLandCategory ToCategory
			{
				get { return (mToCategory); }
				set
				{
					mToCategory = value;
					NotifyPropertyChanged(PropertyArgsToCategory);
				}
			}

			/// <summary>
			/// Конкретная группа куда переводиться участок
			/// </summary>
			[DisplayName("Конкретная группа")]
			[Description("Конкретная группа куда переводиться участок")]
			[Category("Основные параметры")]
			//[Display(Name = "Конкретная группа", Order = 2, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public String ToGroup
			{
				get { return (mToGroup); }
				set
				{
					mToGroup = value;
					NotifyPropertyChanged(PropertyArgsToGroup);
				}
			}

			/// <summary>
			/// Площадь элемента территории
			/// </summary>
			[DisplayName("Площадь, га")]
			[Description("Площадь элемента территории")]
			[Category("Основные параметры")]
			//[Display(Name = "Площадь, га", Order = 3, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public Double Area
			{
				get { return (mArea); }
				set
				{
					mArea = value;
					NotifyPropertyChanged(PropertyArgsArea);
				}
			}

			/// <summary>
			/// Цель перевода
			/// </summary>
			[DisplayName("Цель перевода")]
			[Description("Цель перевода")]
			[Category("Основные параметры")]
			//[Display(Name = "Цель перевода", Order = 4, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public String Purposes
			{
				get { return (mPurposes); }
				set
				{
					mPurposes = value;
					NotifyPropertyChanged(PropertyArgsPurposes);
				}
			}

			/// <summary>
			/// Обоснование перевода
			/// </summary>
			[DisplayName("Обоснование перевода")]
			[Description("Обоснование перевода категории земли")]
			[Category("Основные параметры")]
			//[Display(Name = "Обоснование перевода", Order = 5, GroupName = "3. Основные параметры")]
			[XmlAttribute]
			public String Justification
			{
				get { return (mJustification); }
				set
				{
					mJustification = value;
					NotifyPropertyChanged(PropertyArgsJustification);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTransfer()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CTransfer(String name)
				:this()
			{
				mName = name;
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
				return (null);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Все объекты перевода
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CTransferInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal ObservableCollection<CTransfer> mTransfers;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Список всех объектов перевода
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CTransfer> Transfers
			{
				get { return (mTransfers); }
				set
				{
					mTransfers = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTransferInfrastructure()
			{
				mTransfers = new ObservableCollection<CTransfer>();
				mName = "Все объекты перевода";
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение данных
			/// </summary>
			/// <param name="transfer_infrastructure">Все объекты перевода</param>
			//---------------------------------------------------------------------------------------------------------
			public void Union(CTransferInfrastructure transfer_infrastructure)
			{
				for (Int32 i = 0; i < transfer_infrastructure.Transfers.Count; i++)
				{
					mTransfers.Add(transfer_infrastructure.Transfers[i]);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение изменения баланса определнной категории земли
			/// </summary>
			/// <param name="category">Категория земли</param>
			/// <returns>Изменение</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetChangedLand(TLandCategory category)
			{
				Double result = 0;

				for (Int32 i = 0; i < mTransfers.Count; i++)
				{
					if(mTransfers[i].FromCategory == category)
					{
						result -= mTransfers[i].Area;
					}
					if (mTransfers[i].ToCategory == category)
					{
						result += mTransfers[i].Area;
					}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение изменения баланса определнной категории земли
			/// </summary>
			/// <param name="land">Элемент территории</param>
			/// <returns>Изменение</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetChangedLand(CLandElement land)
			{
				Double result = 0;

				for (Int32 i = 0; i < mTransfers.Count; i++)
				{
					//if (mTransfers[i].FromCategory == land.Land.Category && mTransfers[i].Group == land.Name)
					//{
					//	result -= mTransfers[i].Area;
					//}
					//if (mTransfers[i].ToCategory == land.Land.Category && mTransfers[i].ToGroup == land.Name)
					//{
					//	result += mTransfers[i].Area;
					//}
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение площади убывания
			/// </summary>
			/// <param name="land">Земля</param>
			/// <returns>Площадь убывания</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetLandFrom(CLandElement land)
			{
				Double result = 0;
				for (Int32 i = 0; i < mTransfers.Count; i++)
				{
					//if (mTransfers[i].FromCategory == land.Land.Category && mTransfers[i].Group == land.Name)
					//{
					//	result += mTransfers[i].Area;
					//}
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение площади убывания
			/// </summary>
			/// <param name="land">Земля</param>
			/// <returns>Площадь убывания</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetLandTo(CLandElement land)
			{
				Double result = 0;
				for (Int32 i = 0; i < mTransfers.Count; i++)
				{
					//if (mTransfers[i].ToCategory == land.Land.Category && mTransfers[i].ToGroup == land.Name)
					//{
					//	result += mTransfers[i].Area;
					//}
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
				return (mTransfers);
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
				CTransfer land_transfer = new CTransfer("Новый элемент");
				mTransfers.Add(land_transfer);
				return (land_transfer);
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
				CTransfer land_transfer = element as CTransfer;
				if (land_transfer != null)
				{
					mTransfers.Add(land_transfer);
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
				CTransfer land_transfer = element as CTransfer;
				if (land_transfer != null)
				{
					mTransfers.Remove(land_transfer);
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
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================