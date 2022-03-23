//=====================================================================================================================
// ������: Lotus.Web
// ������: ����� ������
// ���������: ���������� ��������
// �����: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusWebAccountUser.cs
*		����� ��� ����������� ������������.
*/
//---------------------------------------------------------------------------------------------------------------------
// ������: 1.0.0.0
// ��������� ��������� �� 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus.Web
{
	namespace Account
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup WebCommonAccount ���������� ��������
		//! \ingroup WebCommon
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// ����� ��� ����������� ������������
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUser : IdentityUser
		{
			#region ======================================= ������ ����������� ������� ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ���������������� ������ ��� ���� <see cref="CUser"/>
			/// </summary>
			/// <param name="model_builder">��������� ��� ���������� �������</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder model_builder)
			{
				model_builder.Entity<CUser>()
					.HasMany(user => user.FieldActivities)
					.WithMany(field => field.Users)
					.UsingEntity(j => j.ToTable("AspUserFieldActivities"));
			}
			#endregion

			#region ======================================= ������ ====================================================
			protected internal List<String> mRoleNames;
			protected internal List<String> mFieldActivityNames;
			#endregion

			#region ======================================= �������� ==================================================
			/// <summary>
			/// ��� ������������
			/// </summary>
			[DataType(DataType.Text)]
			[MaxLength(30)]
			public String Name { get; set; }

			/// <summary>
			/// ������� ������������
			/// </summary>
			[DataType(DataType.Text)]
			[MaxLength(30)]
			public String Surname { get; set; }

			/// <summary>
			/// �������� ������������
			/// </summary>
			public String? Patronymic { get; set; }

			/// <summary>
			/// ������ ��� (���)
			/// </summary>
			[NotMapped]
			public String FullName 
			{
				get { return ($"{Surname} {Name} {Patronymic}"); }
			}

			/// <summary>
			/// ������� ��� (�������, ��������)
			/// </summary>
			[NotMapped]
			public String ShortName
			{
				get 
				{
					if(Patronymic != null)
                    {
						return ($"{Surname} {Name[0]}. {Patronymic[0]}.");
					}
					else
                    {
						return ($"{Surname} {Name[0]}.");
					}
				}
			}

			/// <summary>
			/// ������������� ���������
			/// </summary>
			public Int64? PostId { get; set; }

			/// <summary>
			/// ���������
			/// </summary>
			[ForeignKey(nameof(PostId))]
			public CPost Post { get; set; }

			/// <summary>
			/// ������������ ���������
			/// </summary>
			[NotMapped]
			public String PostShortName
			{
				get 
				{
					if(Post == null)
					{
						return ("��� ���������");
					}
					else
					{
						return (Post.ShortName);
					}
				}
			}

			/// <summary>
			/// C���� ������������ ������������
			/// </summary>
			public List<CFieldActivity> FieldActivities { get; set; }

			/// <summary>
			/// ������ ���� ���� ������������ ������������
			/// </summary>
			[NotMapped]
			public IReadOnlyList<String> FieldActivityNames
			{
				get
				{
					return (mFieldActivityNames);
				}
				set
				{
					mFieldActivityNames.Clear();
					if (value != null && value.Count > 0)
					{
						mFieldActivityNames.AddRange(value);
					}
				}
			}

			/// <summary>
			/// ����� ���� ����� ������������
			/// </summary>
			[NotMapped]
			public String FieldActivityNameText
			{
				get
				{
					return (mFieldActivityNames.ToTextString("��� ����", ',', true));
				}
				set
				{
				}
			}

			/// <summary>
			/// ������ ���� ����� ������������
			/// </summary>
			[NotMapped]
			public IReadOnlyList<String> RoleNames
            {
                get
                {
                    return (mRoleNames);
                }
                set
                {
                    mRoleNames.Clear();
                    if (value != null && value.Count > 0)
                    {
                        mRoleNames.AddRange(value);
                    }
                }
            }

            /// <summary>
            /// ����� ���� ����� ������������
            /// </summary>
            [NotMapped]
			public String RoleNameText
			{
				get
				{
					return (mRoleNames.ToTextString("��� �����", ',', true));
				}
				set
                {
                }
			}
			#endregion

			#region ======================================= ������������ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ����������� �� ��������� �������������� ������ ������ ������������������ ����������
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUser()
				: base()
			{
				FieldActivities = new List<CFieldActivity>();
				mFieldActivityNames = new List<String>();
				mRoleNames = new List<String>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ����������� �������������� ������ ������ ���������� �����������
			/// </summary>
			/// <param name="user_name">��� ������������</param>
			//---------------------------------------------------------------------------------------------------------
			public CUser(String user_name)
				: base(user_name)
			{
				FieldActivities = new List<CFieldActivity>();
				mFieldActivityNames = new List<String>();
				mRoleNames = new List<String>();
			}
			#endregion

			#region ======================================= ����� ������ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// �������� ���� ����� ������������
			/// </summary>
			/// <param name="user_managers">�������� �������������</param>
			/// <returns>������ ���������</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task LoadAllRolesName(UserManager<CUser> user_managers)
			{
                mRoleNames.Clear();
                var roles = await user_managers.GetRolesAsync(this);
                if (roles != null)
                {
                    mRoleNames.AddRange(roles);
                }
            }

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ���������� ���� ���� ������������ ������������
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpadeFieldActivityName()
			{
				mFieldActivityNames.Clear();
				for (Int32 i = 0; i < FieldActivities.Count; i++)
				{
					mFieldActivityNames.Add(FieldActivities[i].ShortName);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ���������� ���� ������������ ������������
			/// </summary>
			/// <param name="field_activities">����� ������������</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpadeFieldActivityEntity(List<CFieldActivity> field_activities)
			{
				FieldActivities.Clear();
                for (Int32 i = 0; i < mFieldActivityNames.Count; i++)
                {
                    String fan = mFieldActivityNames[i];
                    CFieldActivity field_activity = field_activities.Find((field) =>
                    {
                        return (field.ShortName == fan);
                    });
					if(field_activity != null)
                    {
						FieldActivities.Add(field_activity);
					}
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