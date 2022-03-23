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
//=====================================================================================================================
namespace Lotus.Web
{
	namespace Components
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Компонент для отображения текста
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public partial class LotusDisplayName : ComponentBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mID;
			protected internal String mText;
			protected internal Int32 mOffset;
			protected internal Boolean mIsSmallText;
			protected internal Boolean mIsBadge;
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsDone;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Основной идентификатор HTML элемента
			/// </summary>
			[Parameter]
			public String ID
			{
				get { return (mID); }
				set
				{
					mID = value;
				}
			}

			/// <summary>
			/// Основной текст
			/// </summary>
			[Parameter]
			public String Text
			{
				get { return (mText); }
				set
				{
					mText = value;
				}
			}

			/// <summary>
			/// Смещение текста в пробелах
			/// </summary>
			[Parameter]
			public Int32 Offset
			{
				get { return (mOffset); }
				set
				{
					mOffset = value;
				}
			}

			/// <summary>
			/// Статус малого текста
			/// </summary>
			[Parameter]
			public Boolean IsSmallText
			{
				get { return (mIsSmallText); }
				set
				{
					mIsSmallText = value;
				}
			}

			/// <summary>
			/// Отображение дополнительного значка для объекта который не учитывается в расчетах
			/// </summary>
			[Parameter]
			public Boolean IsBadge
			{
				get { return (mIsBadge); }
				set
				{
					mIsBadge = value;
				}
			}

			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			[Parameter]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
				}
			}

			/// <summary>
			/// Статус завершенного объекта
			/// </summary>
			[Parameter]
			public Boolean IsDone
			{
				get { return (mIsDone); }
				set
				{
					mIsDone = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные компонента предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusDisplayName()
			{
				mID = "cubex_id_" + Guid.NewGuid().ToString();
			}
			#endregion
		}
	}
}
//=====================================================================================================================