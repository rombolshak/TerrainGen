using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TerrainGen.Serialization;

namespace TerrainGen
{
    /// <summary>
    /// Представляет какой-либо <b>один</b> параметр сцены
    /// </summary>
    public class SimpleSetting
    {
        /// <summary>
        /// Получает или задает имя параметра
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Получает или задает тип параметра
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Получает или задает само значение параметра
        /// </summary>
        /// <remarks>
        /// При задании значения параметра необходимо вручную обновить и значение типа, если
        /// оно отличается от того, что было. Вообще же, необходимости изменять и передавать
        /// куда-либо дальше объект этого класса не должно возникнуть
        /// </remarks>
        public object Obj { get; set; }
    }

    /// <summary>
    /// Представляет класс настроек приложения, описывающих различные параметры
    /// генерации и отображения сцены
    /// </summary>
    /// <remarks>
    /// Следующий код показывает основные приемы работы с данным классом
    /// <code lang="C#"><![CDATA[Settings set = new Settings();
    /// set["name"] = "Alex";
    /// set["age"] = 19;
    /// set["phones"] = new string[] {"123-45-67", "987-65-43"};
    /// 
    /// Console.WriteLine(set.Exists("qweqwe")); // false
    /// Console.WriteLine(set.Exists("name")); // true
    /// Console.WriteLine(set.GetTypeByName("age").ToString()); // System.Int32
    /// Console.WriteLine( ((string[]) set.Get("phones"))[0] ); // 123-45-67
    /// Console.WriteLine(set["phones"][0]); // 123-45-67
    /// Console.WriteLine("{0}, {1} years, work phone {3}", set["name"], set["age"] + 1,
    /// set["phones][1]); // Alex, 20 years, work phone 987-65-43
    /// ]]></code>
    /// </remarks>
    public class Settings : IEnumerable<SimpleSetting>
    {
        private Dictionary<string, Tuple<Type, object>> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.Settings"/> class.
        /// </summary>
        public Settings() 
        {
            _settings = new Dictionary<string, Tuple<Type, object>>();
        }

        /// <summary>
        /// Проверяет, существует ли в текущем экземпляре объекта параметр с заданным именем
        /// </summary>
        /// <remarks>
        /// Перед вызовом любых других функций, принимающих имя параметра рекомендуется
        /// вызывать этот метод во избежание возниконовения исключений
        /// </remarks>
        /// <param name="name">Имя проверяемого параметра</param>
        /// <returns>
        /// <see langword="true"/>, если параметр найден, и <see langword="false"/> в
        /// противном случае
        /// </returns>
        public bool Exists(string name)
        {
            return _settings.ContainsKey(name);
        }

        /// <summary>
        /// Возвращает тип объекта с указанным именем
        /// </summary>
        /// <param name="name">Имя параметра, чей тип необходимо узнать</param>
        /// <returns>
        /// Тип объекта, если таковой найден, <see langword="null"/> в противном случае
        /// </returns>
        public Type GetTypeByName(string name)
        {
            return _settings.ContainsKey(name) ? _settings[name].Item1 : null;
        }

        /// <summary>
        /// Возвращает значение параметра с указанным именем
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <returns>
        /// <see langword="null"/>, если значение не найдено
        /// </returns>
        public object Get(string name)
        {
            return _settings.ContainsKey(name) ? _settings[name].Item2 : null;
        }

        /// <summary>
        /// Заносит новое значение либо обновляет старое
        /// </summary>
        /// <remarks>
        /// Для простоты использования рекомендуется пользоваться индексатором, в таком
        /// случае не нужно заботиться о типах значений
        /// </remarks>
        /// <param name="name">Имя параметра</param>
        /// <param name="type">Тип значения</param>
        /// <param name="obj">Значение параметра</param>
        public void Set(string name, Type type, object obj)
        {
            _settings[name] = new Tuple<Type, object>(type, obj);
        }

        /// <summary>
        /// Получает или задает значение параметра по заданному имени
        /// </summary>
        /// <remarks>
        /// Следующий код показывает основные приемы работы с данным классом
        /// <code lang="C#"><![CDATA[Settings set = new Settings();
        /// set["name"] = "Alex";
        /// set["age"] = 19;
        /// set["phones"] = new string[] {"123-45-67", "987-65-43"};
        /// 
        /// Console.WriteLine(set.Exists("qweqwe")); // false
        /// Console.WriteLine(set.Exists("name")); // true
        /// Console.WriteLine(set.GetTypeByName("age").ToString()); // System.Int32
        /// Console.WriteLine( ((string[]) set.Get("phones"))[0] ); // 123-45-67
        /// Console.WriteLine(set["phones"][0]); // 123-45-67
        /// Console.WriteLine("{0}, {1} years, work phone {3}", set["name"], set["age"] + 1,
        /// set["phones][1]); // Alex, 20 years, work phone 987-65-43
        /// ]]></code>
        /// </remarks>
        /// <param name="name">Имя параметра</param>
        public dynamic this[string name]
        {
            get 
            {
                return Get(name); 
            }
            set 
            {
                Set(name, value.GetType(), value);
            }
        }

        /// <summary>
        /// Получает имена всех параметров, занесенных в объект на текущий момент
        /// </summary>
        public IEnumerable<string> Names
        {
            get { return _settings.Keys; }
        }

        /// <summary>
        /// Производит слияние текущих настроек с какими-либо другими
        /// </summary>
        /// <param name="sets">Новые настройки</param>
        /// <returns>
        /// Настройки с обновленными значениями и добавленными новыми
        /// </returns>
        public Settings MergeWith(params Settings[] sets)
        {
            return Merge(this, sets);
        }

        /// <summary>
        /// Сливает несколько объектов настроек в один
        /// </summary>
        /// <param name="set1">Начальные настройки</param>
        /// <param name="sets">Настройки, с которыми производится слияние</param>
        public static Settings Merge(Settings set1, params Settings[] sets)
        {
            var result = set1;
            foreach (var set in sets)
                foreach (var name in set.Names)
                {
                    if (set1.GetTypeByName(name) == typeof (Settings))
                        ((Settings) (set1[name])).MergeWith((Settings) set[name]);
                    result[name] = set[name];
                }
            return result;
        }

        /// <summary>
        /// Производит сериализацию либо десериализацию настроек
        /// </summary>
        /// <remarks>
        /// Несмотря на то, что метод принимает объект, реализующий <see
        /// cref="T:TerrainGen.Serialization.ISerializer"/>, фактически требуется
        /// наследование класса <see cref="T:TerrainGen.Serialization.SerializeWriter"/> для
        /// сериализации или <see cref="T:TerrainGen.Serialization.SerializeReader"/> для
        /// десериализации. Предполагается, что методы <see
        /// cref="M:TerrainGen.Serialization.ISerializer.Open"/> и <see
        /// cref="M:TerrainGen.Serialization.ISerializer.Close"/> будут вызваны кодом,
        /// вызвавшим и этот метод
        /// </remarks>
        /// <param name="serializer">Сериализатор, определяющий, в каком формате и куда
        /// записывать/считывать настройки</param>
        /// <returns>
        /// <see langword="true"/>, если операция завершилась успешно, иначе <see
        /// langword="false"/>
        /// </returns>
        public bool Serialize(ISerializer serializer)
        {
            if (serializer.GetType().BaseType == typeof(SerializeWriter))
                return Serialize(this, (SerializeWriter) serializer);
            try
            {
                var tmpSettings = DeserializeObject(typeof(Settings), (SerializeReader)serializer, this);
                this.MergeWith(tmpSettings);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /// <summary>
        /// Пытается получить объект указанного типа из указанного десериализатора
        /// </summary>
        /// <param name="t">Тип объекта, который нужно получить</param>
        /// <param name="reader">Десериализатор</param>
        /// <param name="settingsForTypeCheck">Если тип объекта -- Settings, то необходимо
        /// указать, откуда брать информацию о типах. Значение по-умолчанию null.</param>
        /// <returns>
        /// Объект указанного типа, если удалось его получить
        /// </returns>
        /// <exception cref="InvalidDataException">Объект указанного типа получить не
        /// удалось в силу различных причин</exception>
        private dynamic DeserializeObject(Type t, SerializeReader reader, Settings settingsForTypeCheck = null)
        {
            if (t == typeof(int))
                {
                    var i = 0;
                    if (reader.InOut(ref i))
                        return i;
                    throw new InvalidDataException("Не удалось прочитать число Int32");
                }
                if (t == typeof(double))
                {
                    var d = 0d;
                    if (reader.InOut(ref d))
                        return d;
                    throw new InvalidDataException("Не удалось прочитать число типа Double");
                }
                if (t == typeof(float))
                {
                    var f = 0f;
                    if (reader.InOut(ref f))
                        return f;
                    throw new InvalidDataException("Не удалось прочитать число типа Single");
                }
                if (t == typeof(string))
                {
                    var s = "";
                    if (reader.InOut(ref s))
                        return s;
                    throw new InvalidDataException("Не удалось прочитать строку");
                }
                if (t == typeof(bool))
                {
                    var b = false;
                    if (reader.InOut(ref b))
                        return b;
                    throw new InvalidDataException("Не удалось прочитать значение типа bool");
                }

                if (t.IsArray)
                {
                    if (!reader.InOut(SpecialChars.ArrayStart)) throw new InvalidDataException("Ожидался признак начала массива");
                    var arr = new ArrayList();
                    while (reader.TestCurrentSpecialChar() != SpecialChars.ArrayEnd)
                    {
                        arr.Add(DeserializeObject(t.GetElementType(), reader));
                        if (!reader.InOut(SpecialChars.Delimiter, false)) break;
                        reader.InOut(SpecialChars.Delimiter);
                    }
                    if (!reader.InOut(SpecialChars.ArrayEnd)) throw new InvalidDataException("Ожидался конец массива либо разделитель");
                    var res = Array.CreateInstance(t.GetElementType(), arr.Count);
                    arr.CopyTo(res);
                    return res;
                }

                if (t == typeof(Color))
                {
                    return DeserializeColor(reader);
                }
                if (t == typeof(ColorDescription))
                {
                    return DeserializeColorDescription(reader);
                }
                if (t == typeof(Settings))
                {
                    var settings = new Settings();
                    if (!reader.InOut(SpecialChars.ObjectStart)) throw new InvalidDataException("Ожидался маркер начала объекта");

                    while (reader.TestCurrentSpecialChar() != SpecialChars.ObjectEnd)
                    {
                        var name = "";
                        if (!reader.InOut(ref name)) throw new InvalidDataException("Ожидалось имя параметра");
                        var type = typeof(string);
                        if (settingsForTypeCheck.Exists(name)) type = settingsForTypeCheck.GetTypeByName(name);
                        settings[name] = DeserializeObject(type, reader,
                                                           (Settings) (type == typeof (Settings) ? settingsForTypeCheck[name] : null));
                        if (!reader.InOut(SpecialChars.Delimiter, false)) break;
                        reader.InOut(SpecialChars.Delimiter);
                    }

                    if (!reader.InOut(SpecialChars.ObjectEnd)) throw new InvalidDataException("Ожидался маркер конца объекта");
                    return settings;
                }
            return null;
        }

        private static ColorDescription DeserializeColorDescription(SerializeReader reader)
        {
            var comment = "";
            var level = 0;
            var color = new Color(0, 0, 0);
            var param = "";
            if (!reader.InOut(SpecialChars.ObjectStart)) throw new InvalidDataException("Ожидалось начало секции");
            for (int i = 0; i < 3; ++i)
            {
                if (!reader.InOut(ref param)) throw new InvalidDataException("Не обнаружено имя параметра");
                switch (param)
                {
                    case "comment":
                        if (!reader.InOut(ref comment))
                            throw new InvalidDataException("Невозможно прочитать комментарий");
                        break;
                    case "level":
                        if (!reader.InOut(ref level))
                            throw new InvalidDataException("Невозможно прочитать значение уровня цвета");
                        break;
                    case "color":
                        color = DeserializeColor(reader);
                        break;
                    default:
                        throw new InvalidDataException("Неожиданное значение в объекте описания цвета");
                }
                if (i != 2)
                    if (!reader.InOut(SpecialChars.Delimiter)) throw new InvalidDataException("Ожидался разделитель");
            }
            if (!reader.InOut(SpecialChars.ObjectEnd)) throw new InvalidDataException("Ожидался конец объекта");
            return new ColorDescription {Color = color, Comment = comment, Level = level};
        }

        private static Color DeserializeColor(SerializeReader reader)
        {
            int r = 0, g = 0, b = 0, a = 0;
            if (!reader.InOut(SpecialChars.ObjectStart)) throw new InvalidDataException("Ожидалось начало объекта");
            var colorName = "";
            for (var i = 0; i < 4; ++i)
            {
                if (!reader.InOut(ref colorName)) throw new InvalidDataException("Ожидалось имя цветовой компоненты");
                var colorPart = 0;
                if (!reader.InOut(ref colorPart)) throw new InvalidDataException("Ожидалось значение цветовой компоненты");
                switch (colorName)
                {
                    case "r":
                        r = colorPart;
                        break;
                    case "g":
                        g = colorPart;
                        break;
                    case "b":
                        b = colorPart;
                        break;
                    case "a":
                        a = colorPart;
                        break;
                    default:
                        throw new InvalidDataException("Некорректно задан цвет. Отсутствует одна из компонент");
                }
                if (i != 3)
                    if (!reader.InOut(SpecialChars.Delimiter)) throw new InvalidDataException("Отсутствует разделитель между компонентами цвета");
            }
            if (!reader.InOut(SpecialChars.ObjectEnd)) throw new InvalidDataException("Не закрыта секция цвета");
            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Сериализует указанный объект в указанный сериализатор
        /// </summary>
        /// <param name="o">Объект для сериализации</param>
        /// <param name="writer">Сериализатор</param>
        /// <returns>
        /// <see langword="true"/> при успехе
        /// </returns>
        private static bool Serialize(object o, SerializeWriter writer)
        {
            var t = o.GetType();

            if (t == typeof(int))
            {
                var i = (int) o;
                return writer.InOut(ref i);
            }
            if (t == typeof(double))
            {
                var d = (double) o;
                return writer.InOut(ref d);
            }
            if (t == typeof(float))
            {
                var f = (float) o;
                return writer.InOut(ref f);
            }
            if (t == typeof(string))
            {
                var s = (string) o;
                return writer.InOut(ref s);
            }
            if (t == typeof(bool))
            {
                var b = (bool) o;
                return writer.InOut(ref b);
            }

            if (t.IsArray)
            {
                if (!writer.InOut(SpecialChars.ArrayStart)) return false;
                var first = true;
                foreach (var value in (Array)o)
                {
                    if (!first)
                        writer.InOut(SpecialChars.Delimiter);
                    else first = false;
                    if (!Serialize(value, writer)) return false;
                }
                return writer.InOut(SpecialChars.ArrayEnd);
            }

            if (t == typeof(Color))
            {
                var c = (Color)o;
                int r = c.R, g = c.G, b = c.B, a = c.A;
                if (!writer.InOut(SpecialChars.ObjectStart)) return false;
                writer.WriteName("r");
                if (!writer.InOut(ref r)) return false;
                writer.InOut(SpecialChars.Delimiter);
                writer.WriteName("g");
                if (!writer.InOut(ref g)) return false;
                writer.InOut(SpecialChars.Delimiter);
                writer.WriteName("b");
                if (!writer.InOut(ref b)) return false;
                writer.InOut(SpecialChars.Delimiter);
                writer.WriteName("a");
                if (!writer.InOut(ref a)) return false;
                return writer.InOut(SpecialChars.ObjectEnd);
            }
            if (t == typeof(ColorDescription))
            {
                var cd = (ColorDescription)o;
                var comment = cd.Comment;
                var level = cd.Level;
                if (!writer.InOut(SpecialChars.ObjectStart)) return false;
                writer.WriteName("comment");
                writer.InOut(ref comment);
                writer.InOut(SpecialChars.Delimiter);
                writer.WriteName("color");
                Serialize(cd.Color, writer);
                writer.InOut(SpecialChars.Delimiter);
                writer.WriteName("level");
                writer.InOut(ref level);
                return writer.InOut(SpecialChars.ObjectEnd);
            }
            if (t == typeof(Settings))
            {
                var settings = (Settings)o;
                if (!writer.InOut(SpecialChars.ObjectStart)) return false;

                var first = true;
                foreach (var name in settings.Names)
                {
                    if (!first)
                        writer.InOut(SpecialChars.Delimiter);
                    else first = false;
                    writer.WriteName(name);
                    if (!Serialize(settings[name], writer)) return false;
                }

                return writer.InOut(SpecialChars.ObjectEnd);
            }
            return false;
        }

        #region Implementation of IEnumerable

        public IEnumerator<SimpleSetting> GetEnumerator()
        {
            return _settings.Keys.Select(name => new SimpleSetting
                                                     {
                                                         Name = name,
                                                         Obj = _settings[name].Item2,
                                                         Type = _settings[name].Item1
                                                     }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
