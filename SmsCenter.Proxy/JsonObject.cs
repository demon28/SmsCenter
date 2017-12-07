using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.SmsCenter.Client
{
    internal class JsonObject
    {
        private const string _NULL = "null";
        private const string _UNDEFINED = "undefined";
        private Dictionary<string, object> _jsonDic;
        /// <summary>
        /// JSON解析对象
        /// </summary>
        /// <param name="keyvalueDic"></param>
        protected JsonObject(Dictionary<string, object> keyvalueDic)
        {
            this._jsonDic = keyvalueDic;
        }
        /// <summary>
        /// 解析JSON字符串
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JsonObject Parse(string json)
        {
            var dic = ParseJson(json);
            return new JsonObject(dic);
        }
        /// <summary>
        /// 取属性是否忽略大小写
        /// </summary>
        public static bool PropertyNameEqualityIgnoreCase { get; set; }
        private string[] _keys;
        /// <summary>
        /// JSON数据的键集合
        /// </summary>
        public string[] Keys
        {
            get
            {
                if (_keys == null || _keys.Length <= 0)
                {
                    _keys = new string[0];
                    if (_jsonDic == null)
                    {
                        return _keys;
                    }
                    _keys = new string[_jsonDic.Keys.Count];
                    _jsonDic.Keys.CopyTo(_keys, 0);
                }
                return _keys;
            }
        }
        /// <summary>
        /// 使用键名称获取JSON值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (this._jsonDic.Keys.Contains(key, new JsonPropertyNameComparer()))
                {
                    return this._jsonDic[key];
                }
                return null;
            }
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            object obj = this[key];
            return obj != null ? obj.ToString() : null;
        }
        /// <summary>
        /// 获取指定key的集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject[] GetArray(string key)
        {
            string s = GetString(key);
            var list = ParseJsonArray(s);
            return list.ToArray();
        }
        /// <summary>
        /// 获取指定key的json对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject GetObject(string key)
        {
            string s = GetString(key);
            var obj = JsonObject.Parse(s);
            return obj;
        }
        /// <summary>
        /// 获取指定key的int值
        /// </summary>
        /// <param name="key">Json键名</param>
        /// <returns></returns>
        public int GetInt(string key)
        {
            object value = this[key];
            return Convert.ToInt32(value);
        }
        /// <summary>
        /// 获取指定值的bool值
        /// </summary>
        /// <param name="key">Json键名</param>
        /// <returns></returns>
        public bool GetBoolean(string key)
        {
            object value = this[key];
            return Convert.ToBoolean(value);
        }
        /// <summary>
        /// 获取指定key的浮点数值
        /// </summary>
        /// <param name="key">Json键名</param>
        /// <returns></returns>
        public decimal GetDecimal(string key)
        {
            object value = this[key];
            return Convert.ToDecimal(value);
        }
        /// <summary>
        /// 获取指定key的datetime值
        /// </summary>
        /// <param name="key">Json键名</param>
        /// <returns></returns>
        public DateTime GetDateTime(string key)
        {
            object value = this[key];
            return Convert.ToDateTime(value);
        }
        /// <summary>
        /// Json属性名比较器
        /// </summary>
        private class JsonPropertyNameComparer : IEqualityComparer<string>
        {

            public bool Equals(string x, string y)
            {
                StringComparison sc = JsonObject.PropertyNameEqualityIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                return string.IsNullOrEmpty(x) ? false : x.Equals(y, sc);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }
        private static Dictionary<string, object> ParseJson(string json)
        {
            var resDic = new Dictionary<string, object>();//读取结果
            Stack<char> tagStack = new Stack<char>();//标签栈
            StringReader reader = new StringReader(json);//字符串读取器
            StringBuilder propertyName = new StringBuilder();//Json属性名
            StringBuilder propertyValue = new StringBuilder();//Json属性值
            int position = 0;//索引位置
            int currentChar;//当前字符
            char lastTag = '\0';//上一个标签
            while ((currentChar = reader.Read()) > -1)
            {
                char word = (char)currentChar;
                //Debug.Assert(propertyValue.ToString() != "ALL");
                if (word == '{')
                {
                    if (position != 0)
                    {
                        tagStack.Push(word);
                        propertyValue.Append(word);
                    }
                    lastTag = '{';
                }
                else if (word == '}')
                {
                    if (tagStack.Count == 0)//结束
                    {
                        string value = propertyValue.ToString();
                        if (lastTag != '\'' && lastTag != '\"' && (value == _NULL || value == _UNDEFINED))
                        {
                            value = null;
                        }
                        resDic.Add(propertyName.ToString(), value);
                        propertyName.Clear();
                        propertyValue.Clear();

                    }
                    else
                    {
                        if (tagStack.Peek() != '{')
                        {
                            tagStack.Push(word);
                            propertyValue.Append(word);
                        }
                        else
                        {
                            tagStack.Pop();
                            propertyValue.Append(word);
                        }
                    }
                    lastTag = '}';
                }
                else if (word == ',')
                {
                    if (tagStack.Count == 0)
                    {
                        string value = propertyValue.ToString();
                        if (lastTag != '\'' && lastTag != '\"' && (value == _NULL || value == _UNDEFINED))
                        {
                            value = null;
                        }
                        resDic.Add(propertyName.ToString(), value);

                        propertyName.Clear();
                        propertyValue.Clear();
                    }
                    else
                    {
                        propertyValue.Append(word);
                    }
                    lastTag = ',';
                }
                else if (word == '\'' || word == '\"')
                {
                    if (tagStack.Count > 0)
                    {
                        if (tagStack.Peek() != word)
                        {
                            tagStack.Push(word);
                        }
                        else
                        {
                            tagStack.Pop();
                        }
                        if (tagStack.Count > 0)
                        {
                            propertyValue.Append(word);
                        }
                    }
                    else
                    {
                        tagStack.Push(word);
                    }
                    lastTag = word;
                }
                else if (word == '[')
                {
                    lastTag = '[';
                    tagStack.Push(word);
                    propertyValue.Append(word);
                }
                else if (word == ']')
                {

                    if (tagStack.Peek() != '[')
                    {
                        tagStack.Push(word);
                        propertyValue.Append(word);
                    }
                    else
                    {
                        tagStack.Pop();
                        propertyValue.Append(word);
                    }
                    lastTag = ']';
                }
                else if (word == ':')
                {

                    if (tagStack.Count == 0)
                    {
                        propertyName.Append(propertyValue.ToString());

                        propertyValue.Clear();

                    }
                    else
                    {
                        propertyValue.Append(word);
                    }
                    lastTag = ':';
                }
                else
                {

                    char lastestTag = tagStack.Count > 0 ? tagStack.Peek() : '\0';//上一个标签
                    if (word == 0x20 && lastestTag != '\'' && lastestTag != '\"')
                    {
                        //丢弃空格
                    }
                    else
                    {
                        propertyValue.Append(word);
                    }
                }
                position++;
            }
            return resDic;
        }
        private static List<JsonObject> ParseJsonArray(string json)
        {
            var resDic = new List<JsonObject>();
            StringReader reader = new StringReader(json);
            Stack<char> tagStack = new Stack<char>();
            StringBuilder temp = new StringBuilder();
            List<string> jsonArrayItem = new List<string>();
            char firstChar = (char)reader.Read();
            if (firstChar != '[')
            {
                throw new ApplicationException("不是标准的Json数据格式");
            }
            int i;
            while ((i = reader.Read()) > -1)
            {
                char c = (char)i;
                if (c == '{')
                {
                    tagStack.Push(c);
                    temp.Append(c);
                }
                else if (c == '}')
                {
                    char lastTag = tagStack.Peek();
                    if (lastTag == '{')
                    {
                        tagStack.Pop();
                        temp.Append(c);
                        if (tagStack.Count == 0)
                        {
                            jsonArrayItem.Add(temp.ToString());

                            temp.Clear();

                        }
                    }
                }
                else if (c == ',')
                {
                    if (tagStack.Count > 0)
                    {
                        temp.Append(c);
                    }
                }
                else
                {
                    temp.Append(c);
                }
            }
            foreach (string item in jsonArrayItem)
            {
                var dic = ParseJson(item);
                var dataItem = new JsonObject(dic);
                resDic.Add(dataItem);
            }
            return resDic;
        }
    }
}
