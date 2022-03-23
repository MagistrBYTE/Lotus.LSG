//=====================================================================================================================
// Проект: LotusLocalSelfGovernment
// Раздел: Модуль градостроительства
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLSGUrbanPlanningCommon.cs
*		Общие типы и структуры данных подсистем градостроительного проектирования.
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
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace LSG
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MunicipalityPlanBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип объекта градостроительства
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TStatusUrban>))]
		public enum TStatusUrban
		{
			/// <summary>
			/// Существующий
			/// </summary>
			[Description("Существующий")]
			Existing,

			/// <summary>
			/// Планируемый
			/// </summary>
			[Description("Планируемый")]
			Planned,

			/// <summary>
			/// Ликвидируемый
			/// </summary>
			[Description("Ликвидируемый")]
			Abolished,
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для поддержки площадного объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface IArea
		{
			/// <summary>
			/// Площадь объекта
			/// </summary>
			Double Area { get; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для поддержки требуемого площадного объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface IAreaRequired
		{
			/// <summary>
			/// Площадь объекта
			/// </summary>
			Double RequiredArea { get; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для поддержки площадного объекта содержащий текущие значение и планируемое
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface IAreaProjected : IArea
		{
			/// <summary>
			/// Планируемая площадь
			/// </summary>
			Double AreaProjected { get; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для поддержки объекта который имеет географические координаты
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILocation
		{
			/// <summary>
			/// Географическая широта
			/// </summary>
			Double Latitude { get; set; }

			/// <summary>
			/// Географическая долгота
			/// </summary>
			Double Longitude { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Целый тип содержащий текущие значение и планируемое
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public struct TValueInt : IComparer<TValueInt>, IComparable<TValueInt>, IEqualityComparer<TValueInt>,
			IEquatable<TValueInt>, ICloneable
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			public readonly static TValueInt Zero = new TValueInt(0);
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal Int32 mCurrent;
			internal Int32 mProjected;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Текущие значение
			/// </summary>
			public Int32 Current
			{
				get { return (mCurrent); }
				set
				{
					mCurrent = value;
				}
			}

			/// <summary>
			/// Проектируемое значение
			/// </summary>
			public Int32 Projected
			{
				get { return (mProjected); }
				set
				{
					mProjected = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="current">Значение</param>
			//---------------------------------------------------------------------------------------------------------
			public TValueInt(Int32 current)
			{
				mCurrent = current;
				mProjected = current;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="current">Значение</param>
			/// <param name="projected">Проектируемое значение</param>
			//---------------------------------------------------------------------------------------------------------
			public TValueInt(Int32 current, Int32 projected)
			{
				mCurrent = current;
				mProjected = projected;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(System.Object obj)
			{
				if (obj != null)
				{
					if (GetType() == obj.GetType())
					{
						TValueInt value = (TValueInt)obj;
						return (Equals(value));
					}
				}
				return (base.Equals(obj));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="x">Первый объект</param>
			/// <param name="y">Второй объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(TValueInt x, TValueInt y)
			{
				return (x.mCurrent == y.mCurrent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(TValueInt other)
			{
				return (mCurrent == other.mCurrent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="x">Первый объект</param>
			/// <param name="y">Второй объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 Compare(TValueInt x, TValueInt y)
			{
				return (x.CompareTo(y));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(TValueInt other)
			{
				return (mCurrent.CompareTo(other.mCurrent));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода объекта
			/// </summary>
			/// <returns>Хеш-код объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return (mCurrent.GetHashCode() ^ base.GetHashCode());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода указанного объекта
			/// </summary>
			/// <param name="obj">Объект</param>
			/// <returns>Хеш-код объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetHashCode(TValueInt obj)
			{
				return (obj.GetHashCode());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				return (MemberwiseClone());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (mCurrent.ToString());
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение значений
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Сумма значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValueInt operator +(TValueInt left, TValueInt right)
			{
				return (new TValueInt(left.Current + right.Current, left.Projected + right.Projected));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание значений
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Разность значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValueInt operator -(TValueInt left, TValueInt right)
			{
				return (new TValueInt(left.Current - right.Current, left.Projected - right.Projected));
			}
			
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение значений на равенство
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Статус равенства значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(TValueInt left, TValueInt right)
			{
				return (left.Current == right.Current && left.Projected == right.Projected);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение значений на неравенство
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Статус неравенства значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(TValueInt left, TValueInt right)
			{
				return (left.Current != right.Current || left.Projected != right.Projected);
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в тип
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator Int32(TValueInt value)
			{
				return (value.mCurrent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в тип
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator TValueInt(Int32 value)
			{
				return (new TValueInt(value));
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вещественный тип содержащий текущие значение и планируемое
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public struct TValueReal : IComparer<TValueReal>, IComparable<TValueReal>, IEqualityComparer<TValueReal>,
			IEquatable<TValueReal>, ICloneable
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			public readonly static TValueReal Zero = new TValueReal(0);
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			internal Double mCurrent;
			internal Double mProjected;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Текущие значение
			/// </summary>
			public Double Current
			{
				get { return (mCurrent); }
				set
				{
					mCurrent = value;
				}
			}

			/// <summary>
			/// Проектируемое значение
			/// </summary>
			public Double Projected
			{
				get { return (mProjected); }
				set
				{
					mProjected = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="current">Значение</param>
			//---------------------------------------------------------------------------------------------------------
			public TValueReal(Double current)
			{
				mCurrent = current;
				mProjected = current;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="current">Значение</param>
			/// <param name="projected">Проектируемое значение</param>
			//---------------------------------------------------------------------------------------------------------
			public TValueReal(Double current, Double projected)
			{
				mCurrent = current;
				mProjected = projected;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(System.Object obj)
			{
				if (obj != null)
				{
					if (GetType() == obj.GetType())
					{
						TValueReal value = (TValueReal)obj;
						return (Equals(value));
					}
				}
				return (base.Equals(obj));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="x">Первый объект</param>
			/// <param name="y">Второй объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(TValueReal x, TValueReal y)
			{
				return (x.mCurrent == y.mCurrent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(TValueReal other)
			{
				return (mCurrent == other.mCurrent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="x">Первый объект</param>
			/// <param name="y">Второй объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 Compare(TValueReal x, TValueReal y)
			{
				return (x.CompareTo(y));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(TValueReal other)
			{
				return (mCurrent.CompareTo(other.mCurrent));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода объекта
			/// </summary>
			/// <returns>Хеш-код объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return (mCurrent.GetHashCode() ^ base.GetHashCode());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода указанного объекта
			/// </summary>
			/// <param name="obj">Объект</param>
			/// <returns>Хеш-код объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetHashCode(TValueReal obj)
			{
				return (obj.GetHashCode());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				return (MemberwiseClone());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (mCurrent.ToString());
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение значений
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Сумма значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValueReal operator +(TValueReal left, TValueReal right)
			{
				return (new TValueReal(left.Current + right.Current, left.Projected + right.Projected));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание значений
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Разность значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValueReal operator -(TValueReal left, TValueReal right)
			{
				return (new TValueReal(left.Current - right.Current, left.Projected - right.Projected));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение значения на скаляр
			/// </summary>
			/// <param name="vector">Значение</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValueReal operator *(TValueReal vector, Double scalar)
			{
				return (new TValueReal(vector.Current * scalar, vector.Projected * scalar));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Деление значения на скаляр
			/// </summary>
			/// <param name="vector">Значение</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValueReal operator /(TValueReal vector, Double scalar)
			{
				scalar = 1 / scalar;
				return (new TValueReal(vector.Current * scalar, vector.Projected * scalar));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение значений на равенство
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Статус равенства значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(TValueReal left, TValueReal right)
			{
				return (left.Current == right.Current && left.Projected == right.Projected);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение значений на неравенство
			/// </summary>
			/// <param name="left">Первое значение</param>
			/// <param name="right">Второе значение</param>
			/// <returns>Статус неравенства значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(TValueReal left, TValueReal right)
			{
				return (left.Current != right.Current || left.Projected != right.Projected);
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в тип
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator Double(TValueReal value)
			{
				return (value.mCurrent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в тип
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator TValueReal(Double value)
			{
				return (new TValueReal(value));
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для хранения данных о местоположении объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[TypeConverter(typeof(CAddressConverter))]
		public class CAddress : ICloneable, INotifyPropertyChanged
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsSettlement = new PropertyChangedEventArgs(nameof(Settlement));
			protected static PropertyChangedEventArgs PropertyArgsStreet = new PropertyChangedEventArgs(nameof(Street));
			protected static PropertyChangedEventArgs PropertyArgsNumber = new PropertyChangedEventArgs(nameof(Number));
			protected static PropertyChangedEventArgs PropertyArgsApartament = new PropertyChangedEventArgs(nameof(Apartament));
			protected static PropertyChangedEventArgs PropertyArgsIsLocation = new PropertyChangedEventArgs(nameof(IsLocation));
			protected static PropertyChangedEventArgs PropertyArgsCadastralNumber = new PropertyChangedEventArgs(nameof(CadastralNumber));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal String mSettlement;
			internal String mStreet;
			internal String mNumber;
			internal String mApartament;
			internal Boolean mIsLocation;
			internal String mCadastralNumber;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Населённый пункт
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public String Settlement
			{
				get { return (mSettlement); }
				set
				{
					mSettlement = value;
					NotifyPropertyChanged(PropertyArgsSettlement);
				}
			}

			/// <summary>
			/// Улица
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public String Street
			{
				get { return (mStreet); }
				set
				{
					mStreet = value;
					NotifyPropertyChanged(PropertyArgsStreet);
				}
			}

			/// <summary>
			/// Номер объекта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public String Number
			{
				get { return (mNumber); }
				set
				{
					mNumber = value;
					NotifyPropertyChanged(PropertyArgsNumber);
				}
			}

			/// <summary>
			/// Номер квартиры
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public String Apartament
			{
				get { return (mApartament); }
				set
				{
					mApartament = value;
					NotifyPropertyChanged(PropertyArgsApartament);
				}
			}

			/// <summary>
			/// Статус местоположения, а не адреса объекта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public Boolean IsLocation
			{
				get { return (mIsLocation); }
				set
				{
					mIsLocation = value;
					NotifyPropertyChanged(PropertyArgsIsLocation);
				}
			}

			/// <summary>
			/// Кадастровый номер объекта
			/// </summary>
			[Browsable(false)]
			[XmlAttribute]
			public String CadastralNumber
			{
				get { return (mCadastralNumber); }
				set
				{
					mCadastralNumber = value;
					NotifyPropertyChanged(PropertyArgsCadastralNumber);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CAddress()
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return (MemberwiseClone());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CAddress CloneAddress()
			{
				return (MemberwiseClone() as CAddress);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление адреса</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				if(mIsLocation)
				{
					return (mSettlement);
				}
				else
				{
					return (mSettlement + ", " + mStreet + ", " + mNumber);
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyPropertyChanged =============================
			/// <summary>
			/// Событие срабатывает ПОСЛЕ изменения свойства
			/// </summary>
			public event PropertyChangedEventHandler PropertyChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(String property_name = "")
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(PropertyChangedEventArgs args)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, args);
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конвертер типа <see cref="CAddress"/> для предоставления свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CAddressConverter : TypeConverter
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение возможности конвертации в определённый тип
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="dest_type">Целевой тип</param>
			/// <returns>Статус возможности</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean CanConvertTo(ITypeDescriptorContext context, Type dest_type)
			{
				return (dest_type == typeof(String));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение возможности конвертации из определённого типа
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="source_type">Тип источник</param>
			/// <returns>Статус возможности</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type source_type)
			{
				return (source_type == typeof(String));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация в определённый тип
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="culture">Культура</param>
			/// <param name="value">Значение</param>
			/// <param name="dest_type">Целевой тип</param>
			/// <returns>Значение целевого типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type dest_type)
			{
				return (value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация из определённого типа
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="culture">Культура</param>
			/// <param name="value">Значение целевого типа</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
			{
				CAddress address = new CAddress();
				address.Settlement = value.ToString();
				return (address);
			}
			#endregion

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение возможности использовать определенный набор свойств
			/// </summary>
			/// <param name="context">Контекст</param>
			/// <returns>True</returns>
			//---------------------------------------------------------------------------------------------------------
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
				result.Add(pdc["Settlement"]);
				result.Add(pdc["Street"]);

				return (new PropertyDescriptorCollection(result.ToArray(), true));
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================