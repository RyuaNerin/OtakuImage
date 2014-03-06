using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
namespace ComputerBeacon.Json
{
	internal static class Helper
	{
		static Type[] ValidTypes = new Type[] {typeof(JsonArray),typeof(JsonObject),
                        typeof(string),typeof(bool),typeof(byte),typeof(sbyte),
                        typeof(short),typeof(ushort),typeof(int),typeof(uint),typeof(long),typeof(ulong),
                        typeof(float),typeof(double),typeof(decimal)};
		internal static void CheckValidType(object Value)
		{
			if (Value != null)
			{
				for (int i = 0; i < ValidTypes.Length; i++) if (Value.GetType() == ValidTypes[i]) return;
				throw new FormatException("Invalid value type: " + Value.GetType().ToString());
			}
		}
		internal static void WriteString(StringBuilder sb, string s)
		{
			sb.Append('"');
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c == '"')
				{
					sb.Append("\\\"");
					continue;
				}
				if (c == '\\')
				{
					sb.Append("\\\\");
					continue;
				}
				if (c == '\n')
				{
					sb.Append("\\n");
					continue;
				}
				if (c == '\r')
				{
					sb.Append("\\r");
					continue;
				}
				if (c == '\t')
				{
					sb.Append("\\t");
					continue;
				}
				sb.Append(c);
			}
			sb.Append('"');
		}
	}
}
namespace ComputerBeacon.Json
{
	public class JsonArray : IJsonContainer, IList
	{
		internal List<object> _Objects;
		public JsonArray()
		{
			_Objects = new List<object>();
		}
		public JsonArray(string jsonString)
		{
			JsonArray ja = Parser.Parse(jsonString) as JsonArray;
			if (ja == null) throw new FormatException("JsonString represents JsonObject instead of JsonArray");
			this._Objects = ja._Objects;
		}
		void IJsonContainer.InternalAdd(string key, object value)
		{
			_Objects.Add(value);
		}
		bool IJsonContainer.IsArray { get { return true; } }
		public object this[int index]
		{
			get
			{
				return _Objects[index];
			}
			set
			{
				Helper.CheckValidType(value);
				_Objects[index] = value;
			}
		}
		public int Count { get { return _Objects.Count; } }
		bool IList.IsFixedSize { get { return false; } }
		bool IList.IsReadOnly { get { return false; } }
		bool ICollection.IsSynchronized { get { return false; } }
		object ICollection.SyncRoot { get { return this; } }
		void ICollection.CopyTo(Array array, int startIndex)
		{
			for (int i = 0; i < this._Objects.Count; i++)
			{
				array.SetValue(_Objects[i], i + startIndex);
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Objects.GetEnumerator();
		}
		public int Add(object item)
		{
			Helper.CheckValidType(item);
			_Objects.Add(item);
			return _Objects.Count - 1;
		}
		public void Clear()
		{
			_Objects.Clear();
		}
		public bool Contains(object item)
		{
			return _Objects.Contains(item);
		}
		public int IndexOf(object item)
		{
			return _Objects.IndexOf(item);
		}
		public void Insert(int index, object item)
		{
			Helper.CheckValidType(item);
			_Objects.Insert(index, item);
		}
		public void Remove(object item)
		{
			_Objects.Remove(item);
		}
		public void RemoveAt(int index)
		{
			_Objects.RemoveAt(index);
		}
		public override string ToString()
		{
			JsonStringLevel tsl = new JsonStringLevel();
			tsl.enumerator = _Objects.GetEnumerator();
			tsl.HasValue = false;
			return JsonStringGenerator.GetJsonString(tsl, string.Empty, string.Empty);
		}
		public string ToString(string newline, string indent)
		{
			JsonStringLevel tsl = new JsonStringLevel();
			tsl.enumerator = _Objects.GetEnumerator();
			tsl.HasValue = false;
			return JsonStringGenerator.GetJsonString(tsl, newline, indent);
		}
	}
}
namespace ComputerBeacon.Json
{
	interface IJsonContainer
	{
		bool IsArray { get; }
		void InternalAdd(string key, object value);
	}
}
namespace ComputerBeacon.Json
{
	public class JsonObject : IJsonContainer, IDictionary<string, object>
	{
		internal Dictionary<string, object> Entries;
		public JsonObject()
		{
			Entries = new Dictionary<string, object>();
		}
		public JsonObject(string jsonString)
		{
			JsonObject jo = Parser.Parse(jsonString) as JsonObject;
			if (jo == null) throw new FormatException("JsonString represents JsonArray instead of JsonObject");
			this.Entries = jo.Entries;
		}
		void IJsonContainer.InternalAdd(string key, object value)
		{
			Entries.Add(key, value);
		}
		bool IJsonContainer.IsArray { get { return false; } }
		public object this[string key]
		{
			get
			{
				if (Entries.ContainsKey(key)) return Entries[key];
				return null;
			}
			set
			{
				Helper.CheckValidType(value);
				Entries[key] = value;
			}
		}
		public int Count { get { return Entries.Count; } }
		public bool IsReadOnly { get { return false; } }
		public ICollection<string> Keys { get { return Entries.Keys; } }
		public ICollection<object> Values { get { return Entries.Values; } }
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			Helper.CheckValidType(item.Value);
			Add(item.Key, item.Value);
		}
		public void Add(string key, object value)
		{
			Helper.CheckValidType(value);
			Entries.Add(key, value);
		}
		public void Clear() { Entries.Clear(); }
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
		public bool Remove(string key) { return Entries.Remove(key); }
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			int i = 0;
			foreach (KeyValuePair<string, object> KVP in Entries)
			{
				array[arrayIndex + (i++)] = KVP;
			}
		}
		public bool Contains(KeyValuePair<string, object> item)
		{
			return Entries.ContainsKey(item.Key) && Entries[item.Key].Equals(item.Value);
		}
		public bool ContainsKey(string key)
		{
			return Entries.ContainsKey(key);
		}
		public bool TryGetValue(string key, out object value)
		{
			return Entries.TryGetValue(key, out value);
		}
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return Entries.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Entries.GetEnumerator();
		}
		public override string ToString()
		{
			JsonStringLevel tsl = new JsonStringLevel();
			tsl.enumerator = Entries.GetEnumerator();
			tsl.HasValue = false;
			return JsonStringGenerator.GetJsonString(tsl, string.Empty, string.Empty);
		}
		public string ToString(string newline, string indent)
		{
			JsonStringLevel tsl = new JsonStringLevel();
			tsl.enumerator = Entries.GetEnumerator();
			tsl.HasValue = false;
			return JsonStringGenerator.GetJsonString(tsl, newline, indent);
		}
	}
}
namespace ComputerBeacon.Json
{
	class JsonStringLevel
	{
		public IEnumerator enumerator;
		public bool HasValue;
	}
	class JsonStringGenerator
	{
		public static string GetJsonString(JsonStringLevel tsl, string newline, string indent)
		{
			StringBuilder sb = new StringBuilder();
			Stack<JsonStringLevel> stack = new Stack<JsonStringLevel>();
			if (tsl.enumerator is List<object>.Enumerator) sb.Append('[');
			else sb.Append('{');
			stack.Push(tsl);
			while (stack.Count > 0)
			{
				if (!stack.Peek().enumerator.MoveNext())
				{
					sb.Append(newline);
					for (int i = 1; i < stack.Count; i++)
					{
						sb.Append(indent);
					}
					if (stack.Pop().enumerator is List<object>.Enumerator) sb.Append(']');
					else sb.Append('}');
					continue;
				}
				if (stack.Peek().HasValue) sb.Append(',');
				sb.Append(newline);
				for (int i = 0; i < stack.Count; i++)
				{
					sb.Append(indent);
				}
				object data;
				if (stack.Peek().enumerator is Dictionary<string, object>.Enumerator)
				{
					Dictionary<string, object>.Enumerator e = (Dictionary<string, object>.Enumerator)stack.Peek().enumerator;
					Helper.WriteString(sb, e.Current.Key);
					sb.Append(':');
					data = e.Current.Value;
				}
				else
				{
					List<object>.Enumerator e = (List<object>.Enumerator)stack.Peek().enumerator;
					data = e.Current;
				}
				stack.Peek().HasValue = true;
				if (data is JsonObject)
				{
					sb.Append('{');
					JsonStringLevel level = new JsonStringLevel();
					level.enumerator = (data as JsonObject).Entries.GetEnumerator();
					level.HasValue = false;
					stack.Push(level);
					continue;
				}
				if (data is JsonArray)
				{
					sb.Append('[');
					JsonStringLevel level = new JsonStringLevel();
					level.enumerator = (data as JsonArray)._Objects.GetEnumerator();
					level.HasValue = false;
					stack.Push(level);
					continue;
				}
				if (data == null) sb.Append("null");
				else if (data is string) Helper.WriteString(sb, data as string);
				else sb.Append(data.ToString());
			}
			return sb.ToString();
		}
	}
}
namespace ComputerBeacon.Json
{
	public static class Parser
	{
		public static object Parse(string s)
		{
			Stack<IJsonContainer> stack = new Stack<IJsonContainer>();
			StringBuilder sb = new StringBuilder();
			string key = null;
			object root = null;
			bool aftercomma = false;
			short state = 0;
			int length = s.Length;
			char c;
			uint hexvalue;
			int i = 0;
			int strStart = -1;
			int strLength = 0;
			do
			{
				c = s[i];
				switch (state)
				{
					case 4:
						switch (c)
						{
							case '"':
								if (strLength > 0)
								{
									sb.Append(s, strStart, strLength);
									strLength = 0;
								}
								strStart = -1;
								if (!stack.Peek().IsArray && key == null)
								{
									if (sb.Length == 0) throw MakeException(s, i, "Key of value in JSON object cannot be empty string");
									state = 7;
								}
								else
								{
									stack.Peek().InternalAdd(key, sb.ToString());
									key = null;
									sb.Length = 0;
									state = 8;
								}
								continue;
							case '\\':
								if (strLength > 0)
								{
									sb.Append(s, strStart, strLength);
									strLength = 0;
								}
								strStart = -1;
								state = 5;
								continue;
							default:
								//sb.Append(c);
								//if (strStart == -1) strStart = i;
								++strLength;
								continue;
						}
					case 1:
						if (c == ' ' || c == '\n' || c == '\r' || c == '\t') continue;
						if (c == '"')
						{
							strStart = i + 1;
							state = 4; continue;
						}
						if (c == '{')
						{
							aftercomma = false;
							JsonObject jo = new JsonObject();
							stack.Peek().InternalAdd(key, jo);
							key = null;
							stack.Push(jo);
							state = 3; continue;
						}
						if (c == '[')
						{
							aftercomma = false;
							JsonArray ja = new JsonArray();
							stack.Peek().InternalAdd(key, ja);
							key = null;
							stack.Push(ja);
							continue;
						}
						if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '-')
						{
							sb.Append(c);
							state = 2; continue;
						}
						if (!aftercomma && c == ']')
						{
							if (!stack.Pop().IsArray) throw MakeException(s, i, "Invalid ']' character");
							if (stack.Count > 0)
							{
								state = 8; continue;
							}
							state = 9; continue;
						}
						throw MakeException(s, i, "Unknown value expression.");
					case 2:
						if (c == ' ' || c == '\n' || c == '\r' || c == '\t') continue;
						if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '.' || c == '+')
						{
							sb.Append(c);
							continue;
						}
						if (c == ',')
						{
							aftercomma = true;
							stack.Peek().InternalAdd(key, FormatJsonProperty(sb.ToString()));
							key = null;
							sb.Length = 0;
							if (stack.Peek().IsArray)
							{
								state = 1; continue;
							}
							else
							{
								state = 3; continue;
							}
						}
						if (c == ']')
						{
							if (!stack.Peek().IsArray) throw MakeException(s, i, "Invalid ']' character");
							stack.Pop().InternalAdd(null, FormatJsonProperty(sb.ToString()));
							sb.Length = 0;
							if (stack.Count > 0)
							{
								state = 8; continue;
							}
							state = 9; continue;
						}
						if (c == '}')
						{
							if (stack.Peek().IsArray) throw MakeException(s, i, "Invalid '}' character");
							stack.Pop().InternalAdd(key, FormatJsonProperty(sb.ToString()));
							key = null;
							sb.Length = 0;
							if (stack.Count > 0)
							{
								state = 8; continue;
							}
							state = 9; continue;
						}
						throw MakeException(s, i, "Invalid character in non-string value");
					case 3:
						switch (c)
						{
							case ' ':
							case '\n':
							case '\r':
							case '\t':
								continue;
							case '"':
								strStart = i + 1;
								state = 4;
								continue;
							case '}':
								if (aftercomma) goto default;
								if (stack.Pop().IsArray) throw MakeException(s, i, "Invalid '}' character");
								if (stack.Count == 0) state = 9;
								else state = 8;
								continue;
							default:
								throw MakeException(s, i, "Expected double quotation character to mark beginning of string");
						}
					case 5:
						switch (c)
						{
							case ' ':
							case '\n':
							case '\r':
							case '\t': continue;
							case '\\':
								sb.Append('\\');
								strStart = i + 1;
								state = 4; continue;
							case '/':
								sb.Append('/');
								strStart = i + 1;
								state = 4; continue;
							case '"':
								sb.Append('"');
								strStart = i + 1;
								state = 4; continue;
							case 'n':
								sb.Append('\n');
								strStart = i + 1;
								state = 4; continue;
							case 'r':
								sb.Append('\r');
								strStart = i + 1;
								state = 4; continue;
							case 't':
								sb.Append('\t');
								strStart = i + 1;
								state = 4; continue;
							case 'u':
								state = 6;
								continue;
							default:
								throw MakeException(s, i, "Unknown escaped character");
						}
					case 6:
						if (c == ' ' || c == '\n' || c == '\r' || c == '\t') continue;
						if (i + 3 >= length) throw new FormatException("Incomplete JSON string");
						hexvalue = (CharToHex(c) << 12) | (CharToHex(s[i + 1]) << 8) | (CharToHex(s[i + 2]) << 4) | CharToHex(s[i + 3]);
						sb.Append((char)hexvalue);
						i += 3;
						strStart = i + 1;
						state = 4;
						continue;
					case 7:
						switch (c)
						{
							case ' ':
							case '\n':
							case '\r':
							case '\t': continue;
							case ':':
								key = sb.ToString();
								sb.Length = 0;
								state = 1;
								continue;
							default:
								throw MakeException(s, i, "Expected colon(:) to seperate key and values in JSON object");
						}
					case 8:
						switch (c)
						{
							case ' ':
							case '\n':
							case '\r':
							case '\t':
								continue;
							case ',':
								aftercomma = true;
								state = 1;
								continue;
							case ']':
								if (!stack.Pop().IsArray) throw MakeException(s, i, "Invalid ']' character");
								if (stack.Count == 0) state = 9;
								continue;
							case '}':
								if (stack.Pop().IsArray) throw MakeException(s, i, "Invalid '}' character");
								if (stack.Count == 0) state = 9;
								continue;
							default:
								throw MakeException(s, i, "Expect comma or close bracket after value");
						}
					case 0:
						switch (c)
						{
							case ' ':
							case '\n':
							case '\r':
							case '\t':
								continue;
							case '[':
								JsonArray JA = new JsonArray();
								root = JA;
								stack.Push(JA);
								state = 1; continue;
							case '{':
								JsonObject JO = new JsonObject();
								root = JO;
								stack.Push(JO);
								state = 3; continue;
							default:
								throw MakeException(s, i, "Expect '{' or '[' to begin JSON string");
						}
					case 9:
						switch (c)
						{
							case ' ':
							case '\n':
							case '\r':
							case '\t':
								continue;
							default:
								throw MakeException(s, i, "Unexpected character(s) after termination of JSON string");
						}
				}
			} while (++i < length);
			if (state != 9) throw new FormatException("Incomplete JSON string");
			return root;
		}
		private static uint CharToHex(char c)
		{
			if (c >= '0' && c <= '9') return (uint)(c - '0');
			if (c >= 'a' && c <= 'f') return (uint)(c - 'a' + 10);
			if (c >= 'A' && c <= 'F') return (uint)(c - 'A' + 10);
			throw new FormatException(c + " is not a valid hex value");
		}
		private static object FormatJsonProperty(string jsonString)
		{
			int result;
			if (int.TryParse(jsonString, NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, null, out result)) return result;
			long result_long;
			if (long.TryParse(jsonString, NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, null, out result_long)) return result_long;
			double result_double;
			if (double.TryParse(jsonString, NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, null, out result_double)) return result_double;
			string lower = jsonString.ToLower();
			if (lower == "true") return true;
			if (lower == "false") return false;
			if (lower == "null") return null;
			throw new FormatException(string.Format("Unknown JSON value: \"{0}\"", jsonString));
		}
		private static FormatException MakeException(string errorString, int position, string message)
		{
			int start = position - 5;
			if (start < 0) start = 0;
			int length = errorString.Length - position;
			if (length > 5) length = 5;
			length += 5;
			StringBuilder sb = new StringBuilder(message);
			sb.Append(" at character position " + position + ", near ");
			Helper.WriteString(sb, errorString.Substring(start, length));
			return new FormatException(sb.ToString());
		}
	}
}