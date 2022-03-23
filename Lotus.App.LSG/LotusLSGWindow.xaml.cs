//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Markup;
//---------------------------------------------------------------------------------------------------------------------
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;
using AvalonDock.Layout;
//---------------------------------------------------------------------------------------------------------------------
using Fluent;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Windows;
using Lotus.LSG;
//=====================================================================================================================
namespace Lotus
{
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Логика взаимодействия для LotusLSGWindow.xaml
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public partial class LotusLSGWindow : RibbonWindow, INotifyPropertyChanged
	{
		#region =========================================== СТАТИЧЕСКИЕ ДАННЫЕ ========================================
		protected static PropertyChangedEventArgs PropertyArgsDocument = new PropertyChangedEventArgs(nameof(Document));
		#endregion

		#region =========================================== ОПРЕДЕЛЕНИЕ СВОЙСТВ ЗАВИСИМОСТИ ===========================
		#endregion

		#region =========================================== ДАННЫЕ ====================================================
		private ILotusDocument mDocument;
		//private CRepositoryDispatcher mRepositoryDispatcher;
		#endregion

		#region =========================================== СВОЙСТВА ==================================================
		/// <summary>
		/// Текущий документ для работы
		/// </summary>
		public ILotusDocument Document
		{
			get { return (mDocument); }
			set
			{
				mDocument = value;
				NotifyPropertyChanged(PropertyArgsDocument);
			}
		}
		#endregion

		#region =========================================== КОНСТРУКТОРЫ ==============================================
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public LotusLSGWindow()
		{
			this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
			InitializeComponent();
		}
		#endregion

		#region =========================================== ОБРАБОТЧИКИ СОБЫТИЙ - ГЛАВНОЕ ОКНО ========================
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Загрузка основного окна и готовность к представлению
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnMainWindowLoaded(Object sender, RoutedEventArgs args)
		{
			// Создаём менеджер репозиториев
			//mRepositoryDispatcher = new CRepositoryDispatcher();
			//mRepositoryDispatcher.LoadAllRepositories(Path.Combine(XApplicationManager.GetPathDirectoryData(), "Handbook"), true);

			// Присваиваем иконки панелям
			//layoutAnchorableTreeModel.IconSource = XResources.Fatcow_folders_explorer_32.ToBitmapSource();
			//layoutAnchorableInspectorProperties.IconSource = XResources.NuoveXT_document_properties_16.ToBitmapSource();
			//layoutAnchorableLogger.IconSource = XResources.Oxygen_utilities_log_viewer_16.ToBitmapSource();

			// Устанавливаем глобальные данные по элементам управления
			//XWindowManager.ExploreModel = treeModel;
			//XWindowManager.PropertyInspector = inspectorProperties;
			//XWindowManager.RepositoryDispatcher = mRepositoryDispatcher;
			//XLogger.Logger = logger;

			// Устанавливаем презентаторы данных
			//treeModel.TextPresenter = flowDoc;
			//treeModel.TablePresenter = dataPresent;
			//treeModel.IsNotifySelectedInspector = true;

			// Присваиваем команды
			CommandBindings.Add(new CommandBinding(XCommandManager.FileNew, OnFileNew));
			CommandBindings.Add(new CommandBinding(XCommandManager.FileOpen, OnFileOpen));
			CommandBindings.Add(new CommandBinding(XCommandManager.FileSave, OnFileSave));
			CommandBindings.Add(new CommandBinding(XCommandManager.FileSaveAs, OnFileSaveAs));
			CommandBindings.Add(new CommandBinding(XCommandManager.FilePrint, OnFilePrint));
			CommandBindings.Add(new CommandBinding(XCommandManager.FileExport, OnFileExport));
			CommandBindings.Add(new CommandBinding(XCommandManager.FileClose, OnFileClose));
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Закрытие основного окна
		/// </summary>
		/// <remarks>
		/// Применяется при закрытие другим способом
		/// </remarks>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnMainWindowClose(Object sender, RoutedEventArgs args)
		{
			Close();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Закрытие основного окна
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnMainWindowClosing(Object sender, CancelEventArgs args)
		{
		}
		#endregion

		#region =========================================== ОБРАБОТЧИКИ СОБЫТИЙ - ФАЙЛ ================================
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Создание файла
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFileNew(Object sender, ExecutedRoutedEventArgs args)
		{
			if (args.Parameter == null)
			{
				// treeModel.ItemsSource = CFinancingEntityManager.GetTestInstance();
				// btreeModel.ItemTemplateSelector = CFinancingEntityDataSelector.Instance;

				//Document = entityManager;

				//CMunicipalProgram program = CMunicipalProgram.GetTestInstance();
				//treeModel.ItemsSource = program;
				//treeModel.ItemTemplateSelector = CMunicipalProgramDataSelector.Instance;
				//Document = program;
			}
			else
			{
				switch (args.Parameter.ToString())
				{
					case "Program":
						{

						}
						break;
					default:
						break;
				}
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Открытие файла
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFileOpen(Object sender, ExecutedRoutedEventArgs args)
		{
			if (args.Parameter == null)
			{
				if (Document != null)
				{
					Document.LoadDocument();
				}
				else
				{
					//Document = XExtensionDocument.LoadDocument();
					//if(Document != null)
					//{
					//	CMunicipalProgram program = Document as CMunicipalProgram;
					//	treeModel.ItemsSource = program;
					//	treeModel.ItemTemplateSelector = CMunicipalProgramDataSelector.Instance;

					//	program.GroupingIndicatorsBy(nameof(CMunicipalProgramIndicator.SubProgramName),
					//		nameof(CMunicipalProgramIndicator.Desc));

					//	program.GroupingActivitiesBy(nameof(CMunicipalProgramActivities.SubProgramName),
					//		nameof(CMunicipalProgramActivities.YearExecution), nameof(CMunicipalProgramActivities.Group));
					//}
				}
			}
			else
			{
				switch (args.Parameter.ToString())
				{
					default:
						{

						}
						break;
				}
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Сохранение файла
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFileSave(Object sender, RoutedEventArgs args)
		{
			if(mDocument != null)
			{
				mDocument.SaveDocument();
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Сохраннее файла под другим имением
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFileSaveAs(Object sender, RoutedEventArgs args)
		{
			if (mDocument != null)
			{
				mDocument.SaveAsDocument();
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Печать файла
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFilePrint(Object sender, RoutedEventArgs args)
		{

		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Экспорт файла
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFileExport(Object sender, RoutedEventArgs args)
		{

		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Закрытие файла
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnFileClose(Object sender, RoutedEventArgs args)
		{
			//treeModel.ItemsSource = null;
			//treeModel.ItemTemplateSelector = null;
			//inspectorProperties.SelectedObject = null;
			Document = null;
		}
		#endregion

		#region =========================================== ОБРАБОТЧИКИ СОБЫТИЙ - РЕДАКТИРОВАНИЕ ======================
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Быстрое сохранение
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditSave(Object sender, RoutedEventArgs args)
		{
			//XManager.SaveAllProject();
			//XManager.SaveAllDocument();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Копирование в буфер обмена
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditCopy(Object sender, RoutedEventArgs args)
		{
			//XManager.Editor.Copy();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вырезать объект в буфер обмена
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditCut(Object sender, RoutedEventArgs args)
		{

		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вставка из буфера обмена
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditPaste(Object sender, RoutedEventArgs args)
		{
			//XManager.Editor.Paste();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Отмена последнего действия
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditUndo(Object sender, RoutedEventArgs args)
		{
			//XManager.MementoManager.Undo();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Повтор отменённого действия
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditRedo(Object sender, RoutedEventArgs args)
		{
			//XManager.MementoManager.Redo();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Общие обновление
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnEditRefresh(Object sender, RoutedEventArgs args)
		{

		}
		#endregion

		#region =========================================== ОБРАБОТЧИКИ СОБЫТИЙ - СПРАВОЧНИКИ =========================
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Просмотр всех доступных справочников
		/// </summary>
		/// <param name="sender">Источник события</param>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		private void OnButtonHandbookViewClick(Object sender, RoutedEventArgs args)
		{
			//LotusRepositoryViewer handbookViewer = new LotusRepositoryViewer();
			//handbookViewer.RepositoryDispatcher = mRepositoryDispatcher;
			//handbookViewer.Show();

			//CRepositoryExcel database = new CRepositoryExcel();
			//database.Connect(XApplicationManager.GetPathFileData("Handbook\\Система мероприятий.xlsx"), true);

			//CRepositoryDatabaseMySql database = new CRepositoryDatabaseMySql();
			//String connect = "server=localhost;user=root;database=localselfgovernment;password=1111;";

			String connect = "server=mysql-114474.srv.hoster.ru;user=srv114474_deco;database=srv114474_goverment;password=Deco123pas;";

			//database.Connect(connect, true);

			////LotusRepositoryDatabaseViewer repositoryDatabaseViewer = new LotusRepositoryDatabaseViewer();
			////repositoryDatabaseViewer.Repository = database;
			////repositoryDatabaseViewer.ShowDialog();

			//if (database.DataSet != null)
			//{
			//	dataGrid.ItemsSource = database.DataSet.Tables[0].DefaultView;
			//	dataGrid.IsShowFilterColumn = true;
			//}
		}
		#endregion

		#region =========================================== ДАННЫЕ INotifyPropertyChanged =============================
		/// <summary>
		/// Событие срабатывает ПОСЛЕ изменения свойства
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вспомогательный метод для нотификации изменений свойства.
		/// </summary>
		/// <param name="property_name">Имя свойства</param>
		//-------------------------------------------------------------------------------------------------------------
		public void NotifyPropertyChanged(String property_name = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property_name));
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вспомогательный метод для нотификации изменений свойства.
		/// </summary>
		/// <param name="args">Аргументы события</param>
		//-------------------------------------------------------------------------------------------------------------
		public void NotifyPropertyChanged(PropertyChangedEventArgs args)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, args);
			}
		}
		#endregion

		private void LotusTextBoxSearch_TextChanged(Object sender, TextChangedEventArgs e)
		{
			//queryEnumControl.QueryItem.SourceItems = new List<System.Object>(XEnum.GetDescriptions(typeof(TPropertyType)));
		}

		private void Button_Click(Object sender, RoutedEventArgs e)
		{
			//dataGrid.IsShowFilterColumn = !dataGrid.IsShowFilterColumn;
		}
	}
}
//=====================================================================================================================