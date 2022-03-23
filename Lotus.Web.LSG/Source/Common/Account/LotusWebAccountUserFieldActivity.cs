//=====================================================================================================================
// ������: Lotus.Web
// ������: ����� ������
// ���������: ���������� ��������
// �����: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusWebAccountUserFieldActivity.cs
*		����� ��� ����������� ����� ������������ ������������.
*/
//---------------------------------------------------------------------------------------------------------------------
// ������: 1.0.0.0
// ��������� ��������� �� 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus.Web
{
	namespace Account
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup WebCommonAccount
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// ����� ��� ����������� ����� ������������ ������������
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CFieldActivity : CNameableId
		{
			#region ======================================= ������ ����������� ������� ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ���������������� ������ ��� ���� <see cref="CFieldActivity"/>
			/// </summary>
			/// <param name="model_builder">��������� ��� ���������� �������</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				// ����������� ��� �������
				var model = model_builder.Entity<CFieldActivity>();
				model.ToTable("AspUserFieldActivity");      // ��� �������
				model.HasKey(vs => vs.Id);                  // ��������� ����
				model.HasIndex(vs => vs.Id).IsUnique();     // ��� ���������� ������������������ ������ � ���� ������ ����������� �������. ������������ ��������
				model.Ignore(vs => vs.InspectorObjectName); // ���������� ��������� ��������
				model.Ignore(vs => vs.InspectorTypeName);   // ���������� ��������� ��������

				// ����������� ��� ��������
				var property_name = model.Property(vs => vs.Name);
				property_name.HasColumnName("names");       // ������������� � ������ �������
				property_name.HasMaxLength(140);            // ������������ ����� ����
				property_name.IsRequired();                 // ���� ����������� ������ ����� ��������

				// ����������� ��� ��������
				var property_id = model.Property(vs => vs.Id);
				property_id.HasColumnName("id");            // ������������� � ������ �������

				// ����������� ��� ��������
				var property_sname = model.Property(vs => vs.ShortName);
				property_sname.HasColumnName("sname");         // ������������� � ������ �������
				property_sname.HasMaxLength(40);               // ������������ ����� ����

				// ������
				model.HasData(
					new CFieldActivity() { Id = 1, Name = "�������-������������ ���������", ShortName = "���" },
					new CFieldActivity() { Id = 2, Name = "���������� ��������������", ShortName = "���������" },
					new CFieldActivity() { Id = 3, Name = "�������� ������������", ShortName = "������" },
					new CFieldActivity() { Id = 4, Name = "�����������", ShortName = "�����������" }
					);
			}
			#endregion

			#region ======================================= �������� ==================================================
			/// <summary>
			/// ������� ������������ ����� ������������
			/// </summary>
			public String ShortName { get; set; }

			/// <summary>
			/// ��� ������������
			/// </summary>
			public List<CUser> Users { get; set; }
			#endregion

			#region ======================================= ������������ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ����������� �� ��������� �������������� ������ ������ ������������������ ����������
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CFieldActivity()
				: base()
			{
				Users = new List<CUser>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ����������� �������������� ������ ������ ���������� �����������
			/// </summary>
			/// <param name="field_activity">����� ������������</param>
			//---------------------------------------------------------------------------------------------------------
			public CFieldActivity(String field_activity)
				: base(field_activity)
			{
				Users = new List<CUser>();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================