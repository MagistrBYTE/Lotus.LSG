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
		public partial class LotusMunlndicatorTargetValuesClass : ComponentBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal protected String mID;
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
			public CMunicipalProgramIndicator Indicator { get; set; }

			/// <summary>
			/// До какого года будут отражены финансы
			/// </summary>
			[Parameter]
			public Int32 SubProgramId { get; set; }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные компонента предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusMunlndicatorTargetValuesClass()
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
			}
			#endregion
		}
	}
}
//=====================================================================================================================