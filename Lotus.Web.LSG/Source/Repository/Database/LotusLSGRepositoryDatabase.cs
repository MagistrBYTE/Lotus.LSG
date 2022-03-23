//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль репозитория
// Подраздел: Подсистема базы данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGRepositoryDatabase.cs
*		Контекст базы данных представляющий собой все данные.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
//---------------------------------------------------------------------------------------------------------------------
#if USE_EFC
using Microsoft.EntityFrameworkCore;
#endif
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityRepositoryDatabase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Контекст базы данных представляющий собой все данные
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CRepositoryDatabase : DbContext
		{
            #region ======================================= СВОЙСТВА ==================================================
            //
            // ТЕРРИТОРИАЛЬНО-АДРЕСНОЕ ХОЗЯЙСТВО
            //
            /// <summary>
            /// Список сельских поселений
            /// </summary>
            public DbSet<CAddressVillageSettlement> AddressVillageSettlements
            {
                get; set;
            }

            /// <summary>
            /// Список населенных пунктов
            /// </summary>
            public DbSet<CAddressVillage> AddressVillages
            {
                get; set;
            }

            /// <summary>
            /// Список улиц
            /// </summary>
            public DbSet<CAddressStreet> AddressStreets
            {
                get; set;
            }

            /// <summary>
            /// Список адресов
            /// </summary>
            public DbSet<CAddressElement> AddressElements
            {
                get; set;
            }

            //
            // СУБЪЕКТЫ ГРАЖДАНСКИХ ПРАВООТНОШЕНИЙ
            //
            /// <summary>
            /// Общий список субъектов гражданских правоотношений
            /// </summary>
            public DbSet<CSubjectCivil> Subjects
            {
                get; set;
            }

            /// <summary>
            /// Список физических лиц
            /// </summary>
            public DbSet<CIndividualPerson> SubjectPersons
            {
                get; set;
            }

            /// <summary>
            /// Список юридических лиц
            /// </summary>
            public DbSet<CLegalEntity> SubjectEntities
            {
                get; set;
            }

            /// <summary>
            /// Список органов власти
            /// </summary>
            public DbSet<CPublicAuthority> SubjectAuthorities
            {
                get; set;
            }

            //
            // КОНТРАКТЫ И АКТЫ
            //
            /// <summary>
            /// Список всех контрактов
            /// </summary>
            public DbSet<CContract> Contracts
            {
                get; set;
            }

            /// <summary>
            /// Список всех актов выполненных работ
            /// </summary>
            public DbSet<CCertificateCompletion> CertificateCompletions
            {
                get; set;
            }

            //
            // МУНИЦИПАЛЬНЫЕ ПРОГРАММЫ И ЭЛЕМЕНТЫ
            //
            /// <summary>
            /// Список муниципальных программ
            /// </summary>
            public DbSet<CMunicipalProgram> MunicipalPrograms
            {
                get; set;
            }

            /// <summary>
            /// Список муниципальных подпрограмм
            /// </summary>
            public DbSet<CMunicipalSubProgram> MunicipalSubPrograms
            {
                get; set;
            }

            /// <summary>
            /// Список индикаторов муниципальных программ и подпрограмм
            /// </summary>
            public DbSet<CMunicipalProgramIndicator> MunicipalIndicators
            {
                get; set;
            }

            /// <summary>
            /// Список мероприятий муниципальных программ и подпрограмм
            /// </summary>
            public DbSet<CMunicipalProgramActivity> MunicipalActivities
            {
                get; set;
            }
            #endregion

            #region ======================================= КОНСТРУКТОРЫ ==============================================
            //---------------------------------------------------------------------------------------------------------
            /// <summary>
            /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
            /// </summary>
            //---------------------------------------------------------------------------------------------------------
            public CRepositoryDatabase()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="options">Параметры конфигурации</param>
			//---------------------------------------------------------------------------------------------------------
			public CRepositoryDatabase(DbContextOptions<CRepositoryDatabase> options)
				: base(options)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование моделей
			/// </summary>
			/// <param name="model_builder">Интерфейс для построения моделей</param>
			//---------------------------------------------------------------------------------------------------------
			protected override void OnModelCreating(ModelBuilder model_builder)
			{
                // ТЕРРИТОРИАЛЬНО-АДРЕСНОЕ ХОЗЯЙСТВО
                CAddressVillageSettlement.ModelCreating(model_builder);
                CAddressVillage.ModelCreating(model_builder);
                CAddressStreet.ModelCreating(model_builder);
                CAddressElement.ModelCreating(model_builder);

                // СУБЪЕКТЫ ГРАЖДАНСКИХ ПРАВООТНОШЕНИЙ
                CSubjectCivil.ModelCreating(model_builder);
                CIndividualPerson.ModelCreating(model_builder);
                CLegalEntityBase.ModelCreating(model_builder);
                CLegalEntity.ModelCreating(model_builder);
                CPublicAuthority.ModelCreating(model_builder);

                // КОНТРАКТЫ и АКТЫ
                CContract.ModelCreating(model_builder);
                CCertificateCompletion.ModelCreating(model_builder);

                // МУНИЦИПАЛЬНЫЕ ПРОГРАММЫ И ЭЛЕМЕНТЫ
                CMunicipalProgram.ModelCreating(model_builder);
                CMunicipalSubProgram.ModelCreating(model_builder);
                CMunicipalProgramIndicator.ModelCreating(model_builder);
                CMunicipalProgramActivity.ModelCreating(model_builder);
            }
            #endregion
        }
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
