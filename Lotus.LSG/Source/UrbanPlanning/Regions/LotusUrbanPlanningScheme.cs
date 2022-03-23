//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusClientTemplate
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUrbanPlanningScheme.cs
*		Схема территориального планирования.
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
		//! \addtogroup MunicipalityPlanRegions
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Населенный пункт
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[TypeConverter(typeof(CSchemeTypeConverter))]
		public class CScheme : CUrbanPlanningItem, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Основные параметры
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Double mArea = 507600;

			// Все земли по категориям
			internal CLandInfrastructure mLandInfra;

			// Транспортная инфраструктура
			internal CRoadInfrastructure mRoadInfra;

			// Сельские поселения
			internal CVillageSettlementInfrastructure mVSInfra;

			// Список всех объектов
			internal ObservableCollection<CUrbanPlanningItem> mItems;

			// Связанное имя файла
			internal String mFileName;
			internal String mPathFile;

			// Сводные данные
			internal CLandInfrastructure mAllLandInfra;
			internal CTransferInfrastructure mAllTransfers;
			internal CEngineeringInfrastructure mAllEngineeringInfra;
			internal CSpecialInfrastructure mAllSpecialInfra;
			internal CRoadInfrastructure mAllRoadInfra;
			internal CHousingInfrastructure mAllHousingInfra;
			internal CSocialInfrastructure mAllSocialInfra;

			// Служебные данные
			//internal Word.Range mCurrentRange;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Площадь района
			/// </summary>
			[DisplayName("Площадь")]
			[Description("Площадь района, га")]
			[Category("Территория")]
			public Double Area
			{
				get { return (mArea); }
			}

			/// <summary>
			/// Все земли по категориям
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandInfrastructure LandInfra
			{
				get { return (mLandInfra); }
				set
				{
					mLandInfra = value;
				}
			}

			/// <summary>
			/// Транспортная инфраструктура
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CRoadInfrastructure RoadInfra
			{
				get { return (mRoadInfra); }
				set
				{
					mRoadInfra = value;
				}
			}

			/// <summary>
			/// Сельские поселения
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CVillageSettlementInfrastructure VSInfra
			{
				get { return (mVSInfra); }
				set
				{
					mVSInfra = value;
				}
			}

			/// <summary>
			/// Все объекты
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public ObservableCollection<CUrbanPlanningItem> Items
			{
				get
				{
					if(mItems == null)
					{
						mItems = new ObservableCollection<CUrbanPlanningItem>();
						//mItems.Add(mLandInfra);
						mItems.Add(mRoadInfra);
						mItems.Add(mVSInfra);
					}
					return (mItems);
				}
			}

			//
			// НАСЕЛЕНИЕ
			//
			/// <summary>
			/// Общая численность населения
			/// </summary>
			[DisplayName("Численность")]
			[Description("Общая численность населения")]
			[Category("Население")]
			[XmlIgnore]
			public TValueInt PopulationNumber
			{
				get
				{
					TValueInt value = new TValueInt();
					for (Int32 i = 0; i < mVSInfra.VillageSettlements.Count; i++)
					{
						value += mVSInfra.VillageSettlements[i].PopulationNumber;
					}

					return (value);
				}
			}

			/// <summary>
			/// Плотность населения
			/// </summary>
			[DisplayName("Плотность")]
			[Description("Средняя плотность населения")]
			[Category("Население")]
			[XmlIgnore]
			public Double PopulationDensity
			{
				get
				{
					Int32 count_population = 0;
					Double count_area = 0;
					for (Int32 i = 0; i < mVSInfra.VillageSettlements.Count; i++)
					{
						count_population += mVSInfra.VillageSettlements[i].PopulationNumber.Current;
						count_area += mVSInfra.VillageSettlements[i].Area;
					}

					return (count_population / count_area);
				}
			}

			/// <summary>
			/// Численность населения моложе трудоспособного возраста
			/// </summary>
			[DisplayName("Моложе труд/возраста")]
			[Description("Численность населения моложе трудоспособного возраста")]
			[Category("Население")]
			[XmlIgnore]
			public Int32 PopulationUnderWorking
			{
				get
				{
					Int32 value = 0;
					for (Int32 i = 0; i < mVSInfra.VillageSettlements.Count; i++)
					{
						value += mVSInfra.VillageSettlements[i].PopulationUnderWorking;
					}

					return (value);
				}
			}

			/// <summary>
			/// Численность населения трудоспособного возраста
			/// </summary>
			[DisplayName("Трудовой возраст")]
			[Description("Численность населения трудоспособного возраста")]
			[Category("Население")]
			[XmlIgnore]
			public Int32 PopulationWorking
			{
				get
				{
					Int32 value = 0;
					for (Int32 i = 0; i < mVSInfra.VillageSettlements.Count; i++)
					{
						value += mVSInfra.VillageSettlements[i].PopulationWorking;
					}

					return (value);
				}
			}

			/// <summary>
			/// Численность населения старше трудоспособного возраста
			/// </summary>
			[DisplayName("Старше труд/возраста")]
			[Description("Численность населения старше трудоспособного возраста")]
			[Category("Население")]
			[XmlIgnore]
			public Int32 PopulationOverWorking
			{
				get
				{
					Int32 value = 0;
					for (Int32 i = 0; i < mVSInfra.VillageSettlements.Count; i++)
					{
						value += mVSInfra.VillageSettlements[i].PopulationOverWorking;
					}

					return (value);
				}
			}

			/// <summary>
			/// Связанное имя файла
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public String FileName
			{
				get { return (mFileName); }
				set { mFileName = value; }
			}

			/// <summary>
			/// Путь к файлу
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public String PathFile
			{
				get { return (mPathFile); }
				set { mPathFile = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CScheme()
				: base()
			{
				mLandInfra = new CLandInfrastructure();
				//mLandInfra.SetParentArea(this);
				mRoadInfra = new CRoadInfrastructure(TRoadPlaceType.Inside);
				mRoadInfra.Name = "Областные дороги";
				mVSInfra = new CVillageSettlementInfrastructure();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CScheme(String name)
					: this()
			{
				mName = name;
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение всех данных для экспорта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void GetAllDataToExport()
			{
				mAllLandInfra = new CLandInfrastructure();
				mAllTransfers = new CTransferInfrastructure();
				mAllEngineeringInfra = new CEngineeringInfrastructure();
				mAllSpecialInfra = new CSpecialInfrastructure();
				mAllRoadInfra = new CRoadInfrastructure();
				mAllHousingInfra = new CHousingInfrastructure();
				mAllSocialInfra = new CSocialInfrastructure();

				for (Int32 i = 0; i < VSInfra.VillageSettlements.Count; i++)
				{
					VSInfra.VillageSettlements[i].GetAllDataToExport();

					//mAllLandInfra.Union(VSInfra.VillageSettlements[i].LandInfrastructure);
					mAllTransfers.Union(VSInfra.VillageSettlements[i].mTransfers);
					mAllEngineeringInfra.Union(VSInfra.VillageSettlements[i].EngineeringInfrastructure);
					mAllSpecialInfra.Union(VSInfra.VillageSettlements[i].AllSpecialInfra);
					mAllRoadInfra.Union(VSInfra.VillageSettlements[i].AllRoadInfra);
					mAllHousingInfra.Union(VSInfra.VillageSettlements[i].AllHousingInfra);
					mAllSocialInfra.Union(VSInfra.VillageSettlements[i].AllSocialInfra);
				}

				mAllRoadInfra.Union(mRoadInfra);
				mAllHousingInfra.ComputePercentProviding();
			}
			#endregion

			#region ======================================= МЕТОДЫ WORD ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание документов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateWordDocument()
			//{
			//	Word.Application app = new Word.Application();
			//	app.Visible = true;

			//	Word.Document doc = app.Documents.Add();
			//	doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4;
			//	doc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

			//	Object start = 0;
			//	Object end = 0;
			//	mCurrentRange = doc.Range(ref start, ref end);

			//	GetAllDataToExport();

			//	CreateTechnicalEconomicTable(doc);
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание таблицы технико-экономических показателей
			/// </summary>
			/// <param name="doc">Документ Word</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateTechnicalEconomicTable(Word.Document doc)
			//{
			//	// Название
			//	var paragraph = doc.Paragraphs.Add();
			//	paragraph.Range.Text = "Основные технико-экономические показатели";
			//	paragraph.SetHeaderStyle();
			//	paragraph.Range.InsertParagraphAfter();

			//	// Таблица
			//	paragraph = doc.Paragraphs.Add();
			//	paragraph.Range.Text = "Dummy";
			//	paragraph.SetDefaultStyle();
			//	Word.Table table_economic = doc.Tables.Add(paragraph.Range, 1, 5);
			//	table_economic.Borders.Enable = -1;

			//	table_economic.Cell(1, 1).Range.Text = "№\n/п/п";
			//	table_economic.Cell(1, 2).Range.Text = "Наименование показателя";
			//	table_economic.Cell(1, 3).Range.Text = "Ед.\nизм.";
			//	table_economic.Cell(1, 4).Range.Text = "Современное\nсостояние";
			//	table_economic.Cell(1, 5).Range.Text = "Расчетный\nсрок";

			//	// Стиль заголовков
			//	for (Int32 i = 1; i <= 5; i++) table_economic.Cell(1, i).SetHeaderStyle();

			//	//
			//	// 
			//	//
			//	Int32 row = 2;
			//	table_economic.Rows.Add();

			//	table_economic.Cell(row, 1).Range.Text = "1";
			//	table_economic.Cell(row, 2).Range.Text = "2";
			//	table_economic.Cell(row, 3).Range.Text = "3";
			//	table_economic.Cell(row, 4).Range.Text = "4";
			//	table_economic.Cell(row, 5).Range.Text = "5";
			//	for (Int32 i = 1; i <= 5; i++) table_economic.Cell(2, i).SetDefaultStyle();

			//	//
			//	// I.ТЕРРИТОРИЯ
			//	//
			//	CreateLand(ref row, table_economic, "I");

			//	//
			//	// II.НАСЕЛЕНИЯ
			//	//
			//	CreatePopulution(ref row, table_economic, "II");


			//	//
			//	// III.СПЕЦИАЛЬНЫЕ ТЕРРИТОРИИ
			//	//
			//	CreateSpecial(ref row, table_economic, "III");


			//	//
			//	// IV.ТРАНСПОРТНАЯ ИНФРАСТРУКТУРА
			//	//
			//	CreateTransport(ref row, table_economic, "IV");

			//	//
			//	// V.ИНЖЕНЕРНАЯ ИНФРАСТРУКТУРА
			//	//
			//	CreateEngineering(ref row, table_economic, "V");

			//	//
			//	// VI.ЖИЛИЩНАЯ ИНФРАСТРУКТУРА
			//	//
			//	CreateHousing(ref row, table_economic, "6");

			//	//
			//	// VII. СОЦИАЛЬНАЯ ИНФРАСТРУКТУРА
			//	//
			//	CreateSocial(ref row, table_economic, "7");
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по землям
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateLand(ref Int32 row, Word.Table table, String number)
			//{
			//	//
			//	// ТЕРРИТОРИЯ
			//	//
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "I. ТЕРРИТОРИЯ";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	row++;
			//	table.Cell(row, 1).Range.Text = "1";
			//	table.Cell(row, 2).Range.Text = "Площадь сельского поселения";
			//	table.Cell(row, 3).Range.Text = "га";
			//	table.Cell(row, 4).Range.Text = Area.ToString("F1");
			//	table.Cell(row, 5).Range.Text = Area.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(2, i).SetDefaultStyle();

			//	//CreateLand(ref row, table, mAllLandInfra.AgriculturalLand, "1.1");
			//	//CreateLand(ref row, table, mAllLandInfra.SettlementsLands, "1.2");
			//	//CreateLand(ref row, table, mAllLandInfra.IndustrialLand, "1.3");
			//	//CreateLand(ref row, table, mAllLandInfra.LandsForest, "1.4");
			//	//CreateLand(ref row, table, mAllLandInfra.LandsWater, "1.5");
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по землям
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="land">Земля</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateLand(ref Int32 row, Word.Table table, CLand land, String number)
			//{
			//	// Строка данных
			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number;
			//	table.Cell(row, 2).Range.Text = "Площадь " + land.Name.Replace("Земли", "земель");
			//	table.Cell(row, 3).Range.Text = "га";
			//	table.Cell(row, 4).Range.Text = land.Area.ToString("F1");
			//	table.Cell(row, 5).Range.Text = (land.Area + mAllTransfers.GetChangedLand(land.Category)).ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(2, i).SetDefaultStyle();

			//	// Строка для процентов
			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "";
			//	table.Cell(row, 2).Range.Text = "";
			//	table.Cell(row, 3).Range.Text = "%";
			//	table.Cell(row, 4).Range.Text = land.Percent.ToString("F2");
			//	table.Cell(row, 5).Range.Text = land.Percent.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(2, i).SetDefaultStyle();

			//	// Добавляем еще одну пустую строку
			//	//row++;
			//	//table.Rows.Add();

			//	// Предыдущие строки объединяем
			//	table.Cell(row - 1, 1).MergeVertical(1);
			//	table.Cell(row - 1, 2).MergeVertical(1);

			//	for (Int32 i = 0; i < land.LandElements.Count; i++)
			//	{
			//		row++;
			//		table.Rows.Add();
			//		table.Cell(row, 1).Range.Text = number + "." + (i + 1).ToString();
			//		table.Cell(row, 2).Range.Text = land.LandElements[i].Name;
			//		table.Cell(row, 3).Range.Text = "га";
			//		table.Cell(row, 4).Range.Text = land.LandElements[i].Area.ToString("F1");
			//		table.Cell(row, 5).Range.Text = (land.LandElements[i].Area + mAllTransfers.GetChangedLand(land.LandElements[i])).ToString("F1");

			//		row++;
			//		table.Rows.Add();
			//		table.Cell(row, 1).Range.Text = "";
			//		table.Cell(row, 2).Range.Text = "";
			//		table.Cell(row, 3).Range.Text = "%";
			//		table.Cell(row, 4).Range.Text = land.LandElements[i].Percent.ToString("F2");
			//		table.Cell(row, 5).Range.Text = "";

			//		// Предыдущие строки объединяем
			//		table.Cell(row - 1, 1).MergeVertical(1);
			//		table.Cell(row - 1, 2).MergeVertical(1);
			//	}
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по по специальным территориям
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateSpecial(ref Int32 row, Word.Table table, String number)
			//{
			//	//
			//	// ТЕРРИТОРИЯ
			//	//
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "III. ТЕРРИТОРИИ СПЕЦИАЛЬНОГО НАЗНАЧЕНИЯ";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	row++;
			//	table.Cell(row, 1).Range.Text = "3.1";
			//	table.Cell(row, 2).Range.Text = "Количество свалок ТКО";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSpecialInfra.GetCountFromTypeCurrent(TSpecialType.Landfill).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSpecialInfra.GetCountFromTypePlanned(TSpecialType.Landfill).ToString();
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(2, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "3.2";
			//	table.Cell(row, 2).Range.Text = "Количество скотомогильников";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSpecialInfra.GetCountFromTypeCurrent(TSpecialType.CattleCemetery).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSpecialInfra.GetCountFromTypePlanned(TSpecialType.CattleCemetery).ToString();
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(2, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "3.3";
			//	table.Cell(row, 2).Range.Text = "Количество кладбищ";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSpecialInfra.GetCountFromTypeCurrent(TSpecialType.Cemetery).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSpecialInfra.GetCountFromTypePlanned(TSpecialType.Cemetery).ToString();
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(2, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по населению
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreatePopulution(ref Int32 row, Word.Table table, String number)
			//{
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "II.НАСЕЛЕНИЕ";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "2.1";
			//	table.Cell(row, 2).Range.Text = "Общая численность населения";
			//	table.Cell(row, 3).Range.Text = "чел.";
			//	table.Cell(row, 4).Range.Text = PopulationNumber.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = PopulationNumber.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "2.2";
			//	table.Cell(row, 2).Range.Text = "Средняя плотность населения";
			//	table.Cell(row, 3).Range.Text = "чел./га";
			//	table.Cell(row, 4).Range.Text = PopulationDensity.ToString("F0");
			//	table.Cell(row, 5).Range.Text = PopulationDensity.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "2.3";
			//	table.Cell(row, 2).Range.Text = "Численность населения моложе трудоспособного возраста";
			//	table.Cell(row, 3).Range.Text = "чел.";
			//	table.Cell(row, 4).Range.Text = PopulationUnderWorking.ToString("F0");
			//	table.Cell(row, 5).Range.Text = PopulationUnderWorking.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "2.4";
			//	table.Cell(row, 2).Range.Text = "Численность населения трудоспособного возраста";
			//	table.Cell(row, 3).Range.Text = "чел.";
			//	table.Cell(row, 4).Range.Text = PopulationWorking.ToString("F0");
			//	table.Cell(row, 5).Range.Text = PopulationWorking.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "2.5";
			//	table.Cell(row, 2).Range.Text = "Численность населения старше трудоспособного возраста";
			//	table.Cell(row, 3).Range.Text = "чел.";
			//	table.Cell(row, 4).Range.Text = PopulationOverWorking.ToString("F0");
			//	table.Cell(row, 5).Range.Text = PopulationOverWorking.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по транспортной инфраструктуре
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateTransport(ref Int32 row, Word.Table table, String number)
			//{
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "IV.ТРАНСПОРТНАЯ ИНФРАСТРУКТУРА";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	row++;
			//	table.Cell(row, 1).Range.Text = "4";
			//	table.Cell(row, 2).Range.Text = "Протяженность дорожной сети, в том числе";
			//	table.Cell(row, 3).Range.Text = "км";
			//	table.Cell(row, 4).Range.Text = mAllRoadInfra.TotalLength.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllRoadInfra.TotalLength.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "4.1";
			//	table.Cell(row, 2).Range.Text = "С асфальтобетонным покрытием";
			//	table.Cell(row, 3).Range.Text = "км";
			//	table.Cell(row, 4).Range.Text = mAllRoadInfra.GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.Asphalt).ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllRoadInfra.GetTotalLengthFromCoverageTypePlanned(TRoadCoverageType.Asphalt).ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "4.2";
			//	table.Cell(row, 2).Range.Text = "С щебеночным покрытием";
			//	table.Cell(row, 3).Range.Text = "км";
			//	table.Cell(row, 4).Range.Text = mAllRoadInfra.GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.CrushedStone).ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllRoadInfra.GetTotalLengthFromCoverageTypePlanned(TRoadCoverageType.CrushedStone).ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = "4.3";
			//	table.Cell(row, 2).Range.Text = "грунтовые дороги";
			//	table.Cell(row, 3).Range.Text = "км";
			//	table.Cell(row, 4).Range.Text = mAllRoadInfra.GetTotalLengthFromCoverageTypeCurrent(TRoadCoverageType.Ground).ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllRoadInfra.GetTotalLengthFromCoverageTypePlanned(TRoadCoverageType.Ground).ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по инженерной инфраструктуре
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateEngineering(ref Int32 row, Word.Table table, String number)
			//{
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "V. ИНЖЕНЕРНАЯ ИНФРАСТРУКТУРА";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	CreateWaterSupply(ref row, table, "5.1");
			//	CreatePowerSupply(ref row, table, "5.2");
			//	CreateGasSupply(ref row, table, "5.3");
			//	CreateHeatSupply(ref row, table, "5.4");
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по водоснабжению
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateWaterSupply(ref Int32 row, Word.Table table, String number)
			//{
			//	row++;
			//	table.Cell(row, 1).Range.Text = number;
			//	table.Cell(row, 2).Range.Text = "Водоснабжение";
			//	table.Cell(row, 3).Range.Text = "";
			//	table.Cell(row, 4).Range.Text = "";
			//	table.Cell(row, 5).Range.Text = "";
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".1";
			//	table.Cell(row, 2).Range.Text = "Протяжённость внутрепоселковых водопроводных сетей";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.WaterSupply.LengthVillage.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.WaterSupply.LengthVillage.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Протяжённость магистральных водопроводных сетей";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.WaterSupply.LengthTrunk.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.WaterSupply.LengthTrunk.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Общее количество источников водоснабжения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.WaterSupply.CountSource.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.WaterSupply.CountSource.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Общее водопотребление";
			//	table.Cell(row, 3).Range.Text = "тыс. м3/сут";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.WaterSupply.ConsumplationAll.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.WaterSupply.ConsumplationAll.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Среднесуточное водопотребление";
			//	table.Cell(row, 3).Range.Text = "л/сут на чел";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.WaterSupply.ConsumplationDay.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.WaterSupply.ConsumplationDay.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по электроснабжению
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreatePowerSupply(ref Int32 row, Word.Table table, String number)
			//{
			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number;
			//	table.Cell(row, 2).Range.Text = "Электроснабжение";
			//	table.Cell(row, 3).Range.Text = "";
			//	table.Cell(row, 4).Range.Text = "";
			//	table.Cell(row, 5).Range.Text = "";
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".1";
			//	table.Cell(row, 2).Range.Text = "Протяжённость линий электропередач низкого напряжения (до 10 кВ)";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.PowerSupply.LengthLow.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.PowerSupply.LengthLow.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Протяжённость линий электропередач среднего напряжения (10-35 кВ)";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.PowerSupply.LengthMiddle.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.PowerSupply.LengthMiddle.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Протяжённость линий электропередач высокого напряжения (100-1100 кВ)";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.PowerSupply.LengthHigh.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.PowerSupply.LengthHigh.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Кол-во подстанций";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.PowerSupply.CountSubstation.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.PowerSupply.CountSubstation.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Потребность в электроэнергии";
			//	table.Cell(row, 3).Range.Text = "млн. кВт. ч. /в год";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.PowerSupply.ConsumplationAll.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.PowerSupply.ConsumplationAll.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".6";
			//	table.Cell(row, 2).Range.Text = "Потребление электроэнергии";
			//	table.Cell(row, 3).Range.Text = "1 чел. в год (кВт. ч.)";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.PowerSupply.ConsumplationPerson.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.PowerSupply.ConsumplationPerson.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по газоснабжению
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateGasSupply(ref Int32 row, Word.Table table, String number)
			//{
			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number;
			//	table.Cell(row, 2).Range.Text = "Газоснабжение";
			//	table.Cell(row, 3).Range.Text = "";
			//	table.Cell(row, 4).Range.Text = "";
			//	table.Cell(row, 5).Range.Text = "";
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".1";
			//	table.Cell(row, 2).Range.Text = "Протяжённость газопровода низкого давления";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.GasSupply.LengthLow.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.GasSupply.LengthLow.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Протяжённость газопровода среднего давления";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.GasSupply.LengthMiddle.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.GasSupply.LengthMiddle.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Протяжённость газопровода высокого давления";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.GasSupply.LengthHigh.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.GasSupply.LengthHigh.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Количество газорегуляторных пунктов";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.GasSupply.CountStation.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.GasSupply.CountStation.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Газопотребление";
			//	table.Cell(row, 3).Range.Text = "млн. м3/год";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.GasSupply.Consumplation.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.GasSupply.Consumplation.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных по теплоснабжению
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateHeatSupply(ref Int32 row, Word.Table table, String number)
			//{
			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number;
			//	table.Cell(row, 2).Range.Text = "Теплоснабжение";
			//	table.Cell(row, 3).Range.Text = "";
			//	table.Cell(row, 4).Range.Text = "";
			//	table.Cell(row, 5).Range.Text = "";
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".1";
			//	table.Cell(row, 2).Range.Text = "Протяжённость сетей централизованного теплоснабжения";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.HeatSupply.Length.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.HeatSupply.Length.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Количество централизованных источников теплоснабжения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.HeatSupply.CountStationCenter.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.HeatSupply.CountStationCenter.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Количество автономных источников теплоснабжения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.HeatSupply.CountStationLocal.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.HeatSupply.CountStationLocal.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Потребление тепла";
			//	table.Cell(row, 3).Range.Text = "Гкал/год";
			//	table.Cell(row, 4).Range.Text = mAllEngineeringInfra.HeatSupply.Consumplation.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllEngineeringInfra.HeatSupply.Consumplation.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных о жилищной инфраструктуре
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateHousing(ref Int32 row, Word.Table table, String number)
			//{
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "VI. ЖИЛИЩНАЯ ИНФРАСТРУКТУРА";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	row++;
			//	table.Cell(row, 1).Range.Text = number + ".1";
			//	table.Cell(row, 2).Range.Text = "Общая площадь жилого фонда, в том числе:";
			//	table.Cell(row, 3).Range.Text = "м2";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.TotalArea.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.TotalAreaPlanned.ToString("F1");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2.1";
			//	table.Cell(row, 2).Range.Text = "Общая площадь индивидуальных жилых домов";
			//	table.Cell(row, 3).Range.Text = "м2";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.IndividualArea.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.GetTotalAreaFromHouseTypePlanned(THouseType.Individual).ToString("F1");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2.2";
			//	table.Cell(row, 2).Range.Text = "Количество индивидуальных жилых домов";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.IndividualCount.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.GetTotalCountFromHouseTypePlanned(THouseType.Individual).ToString("F0");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3.1";
			//	table.Cell(row, 2).Range.Text = "Общая площадь жилых домов блокированной застройки";
			//	table.Cell(row, 3).Range.Text = "м2";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.LockedArea.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.GetTotalAreaFromHouseTypePlanned(THouseType.Locked).ToString("F1");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3.2";
			//	table.Cell(row, 2).Range.Text = "Количество жилых домов блокированной застройки";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.LockedCount.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.GetTotalCountFromHouseTypePlanned(THouseType.Locked).ToString("F0");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4.1";
			//	table.Cell(row, 2).Range.Text = "Общая площадь многоквартирных жилых домов";
			//	table.Cell(row, 3).Range.Text = "м2";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.TenementArea.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.GetTotalAreaFromHouseTypePlanned(THouseType.Tenement).ToString("F1");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4.2";
			//	table.Cell(row, 2).Range.Text = "Количество многоквартирных жилых домов";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.TenementCount.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.GetTotalCountFromHouseTypePlanned(THouseType.Individual).ToString("F0");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Обеспеченность населения жилым фондом";
			//	table.Cell(row, 3).Range.Text = "кв.м./чел";
			//	table.Cell(row, 4).Range.Text = (mAllHousingInfra.TotalArea / PopulationNumber.Current).ToString("F2");
			//	table.Cell(row, 5).Range.Text = (mAllHousingInfra.TotalAreaPlanned / PopulationNumber.Projected).ToString("F2");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".6";
			//	table.Cell(row, 2).Range.Text = "Обеспеченность жилищного фонда водой";
			//	table.Cell(row, 3).Range.Text = "%";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.ProvidingWater.Current.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.ProvidingWater.Projected.ToString("F2");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".7";
			//	table.Cell(row, 2).Range.Text = "Обеспеченность жилищного фонда канализацией";
			//	table.Cell(row, 3).Range.Text = "%";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.ProvidingSewer.Current.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.ProvidingSewer.Projected.ToString("F2");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".8";
			//	table.Cell(row, 2).Range.Text = "Обеспеченность жилищного фонда газом";
			//	table.Cell(row, 3).Range.Text = "%";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.ProvidingGas.Current.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.ProvidingGas.Projected.ToString("F2");

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".9";
			//	table.Cell(row, 2).Range.Text = "Обеспеченность жилищного фонда теплом";
			//	table.Cell(row, 3).Range.Text = "%";
			//	table.Cell(row, 4).Range.Text = mAllHousingInfra.ProvidingWarm.Current.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mAllHousingInfra.ProvidingWarm.Projected.ToString("F2");
			//}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных о социальной инфраструктуре
			/// </summary>
			/// <param name="row">Текущий индекс строки</param>
			/// <param name="table">Таблица Word</param>
			/// <param name="number">Последовательный номер</param>
			//---------------------------------------------------------------------------------------------------------
			//public void CreateSocial(ref Int32 row, Word.Table table, String number)
			//{
			//	table.Rows.Add();
			//	row++;
			//	table.Cell(row, 1).Range.Text = "VII.СОЦИАЛЬНАЯ ИНФРАСТРУКТУРА";
			//	table.Cell(row, 1).SetDefaultStyle();

			//	table.Rows.Add();
			//	table.Cell(row, 1).MergeHorizontal(4);

			//	row++;
			//	table.Cell(row, 1).Range.Text = number + ".1";
			//	table.Cell(row, 2).Range.Text = "Административные учреждения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSocialInfra.GetCountFromTypeCurrent(TSocialType.Administrative).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSocialInfra.GetCountFromTypePlanned(TSocialType.Administrative).ToString();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Образовательные учреждения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSocialInfra.GetCountFromTypeCurrent(TSocialType.Educational).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSocialInfra.GetCountFromTypePlanned(TSocialType.Educational).ToString();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Учреждения здравоохранения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSocialInfra.GetCountFromTypeCurrent(TSocialType.Health).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSocialInfra.GetCountFromTypePlanned(TSocialType.Health).ToString();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Культурно-досуговые учреждения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSocialInfra.GetCountFromTypeCurrent(TSocialType.Cultural).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSocialInfra.GetCountFromTypePlanned(TSocialType.Cultural).ToString();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Спортивные учреждения и объекты";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSocialInfra.GetCountFromTypeCurrent(TSocialType.Sport).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSocialInfra.GetCountFromTypePlanned(TSocialType.Sport).ToString();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".6";
			//	table.Cell(row, 2).Range.Text = "Учреждения социальной сферы";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mAllSocialInfra.GetCountFromTypeCurrent(TSocialType.Social).ToString();
			//	table.Cell(row, 5).Range.Text = mAllSocialInfra.GetCountFromTypePlanned(TSocialType.Social).ToString();

			//}
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
				return (null);
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
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей
			/// </summary>
			/// <param name="parent">Родительский объект</param>
			//---------------------------------------------------------------------------------------------------------
			public override void OnUpdateLink(CUrbanPlanningItem parent)
			{
				//mLandInfrastructure.OnUpdateLink(this);
				//mTransfers.OnUpdateLink(this);
				//mEngineeringInfrastructure.OnUpdateLink(this);
				//mRoadInfrastructure.OnUpdateLink(this);
				//mVillages.OnUpdateLink(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка данных сельского поселения в файл
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Save()
			{
				if (String.IsNullOrEmpty(mFileName))
				{
					SaveAs();
				}
				else
				{
					Stream stream = new FileStream(Path.Combine(mPathFile, mFileName), FileMode.Create);
					XmlSerializer xml_serializer = new XmlSerializer(typeof(CUrbanPlanningItem));
					xml_serializer.Serialize(stream, this);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка данных сельского поселения в файл
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void SaveAs()
			{
				//Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
				//{
				//	DefaultExt = ".xml",
				//	Filter = "XML documents (.xml)|*.xml", // Filter files by extension
				//	FilterIndex = 1
				//};
				//if (dialog.ShowDialog() == true)
				//{
				//	using (Stream stream = dialog.OpenFile())
				//	{
				//		XmlSerializer xml_serializer = new XmlSerializer(typeof(CUrbanPlanningItem));
				//		xml_serializer.Serialize(stream, this);
				//		mFileName = Path.GetFileName(dialog.FileName);
				//		mPathFile = Path.GetDirectoryName(dialog.FileName);
				//	}
				//}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка данных СТП из файла
			/// </summary>
			/// <returns>СТП</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CScheme LoadFromFile()
			{
				//// Configure open file dialog box
				//Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
				//dlg.FileName = "Document"; // Default file name
				//dlg.DefaultExt = ".xml"; // Default file extension
				//dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension

				//// Show open file dialog box
				//Nullable<Boolean> result = dlg.ShowDialog();

				//// Process open file dialog box results
				CScheme scheme = null;
				//if (result == true)
				//{
				//	using (Stream stream = dlg.OpenFile())
				//	{
				//		XmlSerializer xml_serializer = new XmlSerializer(typeof(CUrbanPlanningItem));
				//		scheme = xml_serializer.Deserialize(stream) as CScheme;
				//	}

				//	if (scheme != null)
				//	{
				//		scheme.FileName = Path.GetFileName(dlg.FileName);
				//		scheme.PathFile = Path.GetDirectoryName(dlg.FileName);
				//		scheme.OnUpdateLink(null);
				//	}
				//}

				return (scheme);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конвертер типа <see cref="CScheme"/> для предоставления свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSchemeTypeConverter : TypeConverter
		{
			//-------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение возможности использовать определенный набор свойств
			/// </summary>
			/// <param name="context">Контекст</param>
			/// <returns>True</returns>
			//-------------------------------------------------------------------------------------------------------------
			public override Boolean GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение нужной коллекции свойств
			/// </summary>
			/// <param name="context">Контекст</param>
			/// <param name="value">Объект</param>
			/// <param name="attributes">Атрибуты</param>
			/// <returns>Сформированная коллекция свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value,
				Attribute[] attributes)
			{
				List<PropertyDescriptor> result = new List<PropertyDescriptor>();
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, true);

				// 1) Общие данные
				result.Add(pdc["Name"]);
				result.Add(pdc["ID"]);
				//result.Add(pdc["Grouping"]);

				// 2) Основные параметры
				//result.Add(pdc["Length"]);

				return (new PropertyDescriptorCollection(result.ToArray(), true));
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================