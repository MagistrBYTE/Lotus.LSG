//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.LSG;
//=====================================================================================================================
namespace Lotus.Web
{
	namespace Components
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Компонент для отображения списка мероприятий по одному целевому показателю
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public partial class LotusViewActivityClass : ComponentBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal protected String mID;
			internal protected List<Boolean> mProcessingActivities;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Список мероприятий
			/// </summary>
			[Parameter]
			public List<CMunicipalProgramActivity> Activities { get; set; }

			/// <summary>
			/// Идентификатор целевого показателя мероприятия которого будут отражены
			/// </summary>
			[Parameter]
			public Int32 IndicatorId { get; set; }

			/// <summary>
			/// До какого года будут отражены финансы
			/// </summary>
			[Parameter]
			public Int32 YearToFinance { get; set; } = 2023;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные компонента предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusViewActivityClass()
			{
				mID = "cubex_id_" + Guid.NewGuid().ToString();
			}
			#endregion

			#region ======================================= ПЕРЕГРУЖЕННЫЕ МЕТОДЫ КОМПОНЕНТА ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Метод, вызываемый, когда компонент получил параметры от своего родителя в дереве отрисовки, 
			/// а входящие значения были назначены свойствам
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected override void OnParametersSet()
			{
				base.OnParametersSet();

				if (Activities != null)
				{
					mProcessingActivities = new List<Boolean>();
					for (Int32 i = 0; i < Activities.Count; i++)
					{
						mProcessingActivities.Add(false);
					}
				}
			}
			#endregion
		}
	}
}
//=====================================================================================================================