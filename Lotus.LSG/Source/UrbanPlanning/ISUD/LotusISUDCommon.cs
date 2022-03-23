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
		//! \defgroup MunicipalityISUD ИСОГД
		//! Информационная система обеспечения градостроительной деятельности
		//! \ingroup Municipality
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Менеджер общих данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CCommonManager
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal ObservableCollection<CVillageSettlement> mVillageSettlements;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Все сельские поселения
			/// </summary>
			public ObservableCollection<CVillageSettlement> VillageSettlements
			{
				get { return (mVillageSettlements); }
				set { mVillageSettlements = value; }
			}
			#endregion

			#region ======================================= МЕТОДЫ СЕРИАЛИЗАЦИИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения данных сельских поселений
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void SaveToXmlVillage(String file_name)
			{
				XmlWriterSettings xws = new XmlWriterSettings();
				xws.Indent = true;

				String path = file_name + ".xml";
				XmlWriter writer = XmlWriter.Create(path, xws);
				writer.WriteStartElement("VillageSettlements");

				for (Int32 i = 0; i < mVillageSettlements.Count; i++)
				{
					writer.WriteStartElement("VillageSettlement");
					//mVillageSettlements[i].WriteToXml(writer);
					writer.WriteEndElement();
				}


				writer.WriteEndElement();
				writer.Close();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка данных сельских поселений
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void LoadFromXmlVillage(String file_name)
			{
				// 1) Создаем читателя
				StringReader stream_file = new StringReader(file_name);
				XmlReader reader = XmlReader.Create(stream_file);

				mVillageSettlements.Clear();

				// 2) Читаем данные
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.Name == "VillageSettlement")
					{
						CVillageSettlement village_settlement = new CVillageSettlement();
						//village_settlement.ReadFromXml(reader);
						mVillageSettlements.Add(village_settlement);
					}
				}

				reader.Close();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================