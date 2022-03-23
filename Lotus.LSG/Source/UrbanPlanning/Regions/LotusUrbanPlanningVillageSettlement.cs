//=====================================================================================================================
// Решение: LotusPlatform
// Проект: LotusClientTemplate
// Раздел: Информационная система обеспечения градостроительной деятельности
// Автор: MagistrBYTE
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusISUDElement.cs
*		Элемент документа ИСОГД.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		//-----------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Сельское поселение
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CVillageSettlement : CUrbanPlanningItem, IArea
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsArea = new PropertyChangedEventArgs(nameof(Area));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Площадь
			internal Double mArea;

			// Земли по категориям
			internal CLandInfrastructure mLandInfrastructure;

			// Все объекты перевода
			internal CTransferInfrastructure mTransfers;

			// Инженерная инфраструктура
			internal CEngineeringInfrastructure mEngineeringInfrastructure;

			// Межпоселковые дороги
			internal CRoadInfrastructure mRoadInfrastructure;

			// Населенные пункты
			internal CVillageInfrastructure mVillages;

			// Все элементы
			internal ObservableCollection<CUrbanPlanningItem> mItems;

			// Связанное имя файла
			internal String mFileName;
			internal String mPathFile;

			// Сводные данные
			internal CSpecialInfrastructure mAllSpecialInfra;
			internal CRoadInfrastructure mAllRoadInfra;
			internal CHousingInfrastructure mAllHousingInfra;
			internal CSocialInfrastructure mAllSocialInfra;

			// Служебные данные
			//internal Word.Range mCurrentRange;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОБЩИЕ ДАННЫЕ
			//
			/// <summary>
			/// Площадь сельского поселения
			/// </summary>
			[DisplayName("Площадь")]
			[Description("Площадь сельского поселения, га")]
			[Category("Территория")]
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
			/// Все земли по категориям
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CLandInfrastructure LandInfrastructure
			{
				get { return (mLandInfrastructure); }
				set { mLandInfrastructure = value; }
			}

			/// <summary>
			/// Все объекты перевода
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CTransferInfrastructure LandTransfers
			{
				get { return (mTransfers); }
				set { mTransfers = value; }
			}

			/// <summary>
			/// Инженерная инфраструктура
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CEngineeringInfrastructure EngineeringInfrastructure
			{
				get { return (mEngineeringInfrastructure); }
				set { mEngineeringInfrastructure = value; }
			}

			/// <summary>
			/// Межпоселковые дороги
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CRoadInfrastructure RoadInfrastructure
			{
				get { return (mRoadInfrastructure); }
				set { mRoadInfrastructure = value; }
			}

			/// <summary>
			/// Населенные пункты
			/// </summary>
			[Browsable(false)]
			[XmlElement]
			public CVillageInfrastructure Villages
			{
				get { return (mVillages); }
				set { mVillages = value; }
			}

			/// <summary>
			/// Список всех элементов
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public ObservableCollection<CUrbanPlanningItem> Items
			{
				get
				{
					if (mItems == null)
					{
						mItems = new ObservableCollection<CUrbanPlanningItem>();
						//mItems.Add(mLandInfrastructure);
						mItems.Add(mTransfers);
						mItems.Add(mEngineeringInfrastructure);
						mItems.Add(mRoadInfrastructure);
						mItems.Add(mVillages);
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
					for (Int32 i = 0; i < mVillages.Villages.Count; i++)
					{
						value += mVillages.Villages[i].PopulationNumber;
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
					for (Int32 i = 0; i < mVillages.Villages.Count; i++)
					{
						count_population += mVillages.Villages[i].PopulationNumber.Current;
						count_area += mVillages.Villages[i].Area;
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
					for (Int32 i = 0; i < mVillages.Villages.Count; i++)
					{
						value += mVillages.Villages[i].PopulationUnderWorking;
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
					for (Int32 i = 0; i < mVillages.Villages.Count; i++)
					{
						value += mVillages.Villages[i].PopulationWorking;
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
					for (Int32 i = 0; i < mVillages.Villages.Count; i++)
					{
						value += mVillages.Villages[i].PopulationOverWorking;
					}

					return (value);
				}
			}

			//
			// СВОДНЫЕ ДАННЫЕ
			//
			/// <summary>
			/// Сводные данные
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CSpecialInfrastructure AllSpecialInfra
			{
				get { return (mAllSpecialInfra); }
			}

			/// <summary>
			/// Сводные данные
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CRoadInfrastructure AllRoadInfra
			{
				get { return (mAllRoadInfra); }
			}

			/// <summary>
			/// Сводные данные
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CHousingInfrastructure AllHousingInfra
			{
				get { return (mAllHousingInfra); }
			}

			/// <summary>
			/// Сводные данные
			/// </summary>
			[Browsable(false)]
			[XmlIgnore]
			public CSocialInfrastructure AllSocialInfra
			{
				get { return (mAllSocialInfra); }
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
			public CVillageSettlement()
				: base()
			{
				//mLandInfrastructure = new CLandInfrastructure();
				//mLandInfrastructure.SetParentArea(this);
				//mTransfers = new CTransferInfrastructure();
				//mEngineeringInfrastructure = new CEngineeringInfrastructure();
				//mRoadInfrastructure = new CRoadInfrastructure(TRoadPlaceType.Between);
				//mRoadInfrastructure.Name = "Межпослековые дороги";
				//mVillages = new CVillageInfrastructure();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public CVillageSettlement(String name)
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
				mAllSpecialInfra = new CSpecialInfrastructure();
				mAllRoadInfra = new CRoadInfrastructure();
				mAllHousingInfra = new CHousingInfrastructure();
				mAllSocialInfra = new CSocialInfrastructure();

				for (Int32 i = 0; i < Villages.Villages.Count; i++)
				{
					mAllSpecialInfra.Union(Villages.Villages[i].SpecialInfrastructure);
					mAllRoadInfra.Union(Villages.Villages[i].Roads);
					mAllHousingInfra.Union(Villages.Villages[i].Housing);
					mAllSocialInfra.Union(Villages.Villages[i].Social);
				}

				mAllRoadInfra.Union(mRoadInfrastructure);
				mAllHousingInfra.ComputePercentProviding();
			}
			#endregion

			#region ======================================= МЕТОДЫ WORD ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание документов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateWordDocument()
			{
				//Word.Application app = new Word.Application();
				//app.Visible = true;

				//Word.Document doc = app.Documents.Add();
				//doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4;
				//doc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

				//Object start = 0;
				//Object end = 0;
				//mCurrentRange = doc.Range(ref start, ref end);

				//GetAllDataToExport();

				//CreateTechnicalEconomicTable(doc);
			}

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

			//	//CreateLand(ref row, table, LandInfrastructure.AgriculturalLand, "1.1");
			//	//CreateLand(ref row, table, LandInfrastructure.SettlementsLands, "1.2");
			//	//CreateLand(ref row, table, LandInfrastructure.IndustrialLand, "1.3");
			//	//CreateLand(ref row, table, LandInfrastructure.LandsForest, "1.4");
			//	//CreateLand(ref row, table, LandInfrastructure.LandsWater, "1.5");
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
			//	table.Cell(row, 5).Range.Text = (land.Area + mTransfers.GetChangedLand(land.Category)).ToString("F1");
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
			//		table.Cell(row, 5).Range.Text = (land.LandElements[i].Area + mTransfers.GetChangedLand(land.LandElements[i])).ToString("F1");

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
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.WaterSupply.LengthVillage.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.WaterSupply.LengthVillage.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Протяжённость магистральных водопроводных сетей";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.WaterSupply.LengthTrunk.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.WaterSupply.LengthTrunk.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Общее количество источников водоснабжения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.WaterSupply.CountSource.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.WaterSupply.CountSource.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Общее водопотребление";
			//	table.Cell(row, 3).Range.Text = "тыс. м3/сут";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.WaterSupply.ConsumplationAll.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.WaterSupply.ConsumplationAll.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Среднесуточное водопотребление";
			//	table.Cell(row, 3).Range.Text = "л/сут на чел";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.WaterSupply.ConsumplationDay.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.WaterSupply.ConsumplationDay.ToString("F2");
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
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.PowerSupply.LengthLow.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.PowerSupply.LengthLow.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Протяжённость линий электропередач среднего напряжения (10-35 кВ)";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.PowerSupply.LengthMiddle.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.PowerSupply.LengthMiddle.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Протяжённость линий электропередач высокого напряжения (100-1100 кВ)";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.PowerSupply.LengthHigh.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.PowerSupply.LengthHigh.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Кол-во подстанций";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.PowerSupply.CountSubstation.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.PowerSupply.CountSubstation.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Потребность в электроэнергии";
			//	table.Cell(row, 3).Range.Text = "млн. кВт. ч. /в год";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.PowerSupply.ConsumplationAll.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.PowerSupply.ConsumplationAll.ToString("F2");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".6";
			//	table.Cell(row, 2).Range.Text = "Потребление электроэнергии";
			//	table.Cell(row, 3).Range.Text = "1 чел. в год (кВт. ч.)";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.PowerSupply.ConsumplationPerson.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.PowerSupply.ConsumplationPerson.ToString("F2");
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
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.GasSupply.LengthLow.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.GasSupply.LengthLow.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Протяжённость газопровода среднего давления";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.GasSupply.LengthMiddle.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.GasSupply.LengthMiddle.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Протяжённость газопровода высокого давления";
			//	table.Cell(row, 3).Range.Text = "м";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.GasSupply.LengthHigh.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.GasSupply.LengthHigh.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Количество газорегуляторных пунктов";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.GasSupply.CountStation.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.GasSupply.CountStation.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".5";
			//	table.Cell(row, 2).Range.Text = "Газопотребление";
			//	table.Cell(row, 3).Range.Text = "млн. м3/год";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.GasSupply.Consumplation.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.GasSupply.Consumplation.ToString("F2");
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
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.HeatSupply.Length.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.HeatSupply.Length.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".2";
			//	table.Cell(row, 2).Range.Text = "Количество централизованных источников теплоснабжения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.HeatSupply.CountStationCenter.Current.ToString("F1");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.HeatSupply.CountStationCenter.Projected.ToString("F1");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".3";
			//	table.Cell(row, 2).Range.Text = "Количество автономных источников теплоснабжения";
			//	table.Cell(row, 3).Range.Text = "шт.";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.HeatSupply.CountStationLocal.Current.ToString("F0");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.HeatSupply.CountStationLocal.Projected.ToString("F0");
			//	for (Int32 i = 1; i <= 5; i++) table.Cell(row, i).SetDefaultStyle();

			//	row++;
			//	table.Rows.Add();
			//	table.Cell(row, 1).Range.Text = number + ".4";
			//	table.Cell(row, 2).Range.Text = "Потребление тепла";
			//	table.Cell(row, 3).Range.Text = "Гкал/год";
			//	table.Cell(row, 4).Range.Text = mEngineeringInfrastructure.HeatSupply.Consumplation.ToString("F2");
			//	table.Cell(row, 5).Range.Text = mEngineeringInfrastructure.HeatSupply.Consumplation.ToString("F2");
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
			/// Загрузка данных сельского поселения из файла
			/// </summary>
			/// <returns>Сельское поселение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CVillageSettlement LoadFromFile()
			{
				//// Configure open file dialog box
				//Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
				//dlg.FileName = "Document"; // Default file name
				//dlg.DefaultExt = ".xml"; // Default file extension
				//dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension

				//// Show open file dialog box
				//Nullable<Boolean> result = dlg.ShowDialog();

				// Process open file dialog box results
				CVillageSettlement village_settlement = null;
				//if (result == true)
				//{
				//	using (Stream stream = dlg.OpenFile())
				//	{
				//		XmlSerializer xml_serializer = new XmlSerializer(typeof(CUrbanPlanningItem));
				//		village_settlement = xml_serializer.Deserialize(stream) as CVillageSettlement;
				//	}

				//	if (village_settlement != null)
				//	{
				//		village_settlement.FileName = Path.GetFileName(dlg.FileName);
				//		village_settlement.PathFile = Path.GetDirectoryName(dlg.FileName);
				//		village_settlement.OnUpdateLink(null);
				//	}
				//}

				return (village_settlement);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Список сельских поселений
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CVillageSettlementInfrastructure : CUrbanPlanningItem
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal ObservableCollection<CVillageSettlement> mVillageSettlements;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Список сельских послелений
			/// </summary>
			[Browsable(false)]
			[XmlArray]
			public ObservableCollection<CVillageSettlement> VillageSettlements
			{
				get { return (mVillageSettlements); }
				set { mVillageSettlements = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CVillageSettlementInfrastructure()
			{
				mVillageSettlements = new ObservableCollection<CVillageSettlement>();
				mName = "Сельские поселения";
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
				return (mVillageSettlements);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание и добавление созданного элемента в список дочерних элементов
			/// </summary>
			/// <remarks>
			/// Происходит создание элемента указанного типа и добавление его в список дочерних элементов
			/// </remarks>
			/// <returns>Структурный элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public override CUrbanPlanningItem AddChildNewElement()
			{
				CVillageSettlement village_settlement = new CVillageSettlement("Сельское  поселение");
				mVillageSettlements.Add(village_settlement);
				return (village_settlement);
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
				CVillageSettlement village_settlement = element as CVillageSettlement;
				if (village_settlement != null)
				{
					mVillageSettlements.Remove(village_settlement);
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
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей
			/// </summary>
			/// <param name="parent">Родительский объект</param>
			//---------------------------------------------------------------------------------------------------------
			public override void OnUpdateLink(CUrbanPlanningItem parent)
			{
				for (Int32 i = 0; i < mVillageSettlements.Count; i++)
				{
					mVillageSettlements[i].OnUpdateLink(parent);
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================