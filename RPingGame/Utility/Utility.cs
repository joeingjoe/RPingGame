using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
#nullable disable

namespace RPingGame.Utility
{
	public static partial class Utility
	{
		public static T FromJson<T> (string json)
		{
			return JsonConvert.DeserializeObject<T> (""+json);
		}
		public static dynamic FromJson(string json)
		{
			return JsonConvert.DeserializeObject("" + json);
		}
		#region object
		public static string Left(this object obj,int length)
		{
			if (length > 0)
			{
				string text = "" + obj;
				return text.Length > length ? text.Substring(0, length) : text;
			}
			else
				return "";
		}
		public static string Right(this object obj,int length)
		{
			string text = "" + obj;
			return text.Length > length ? text.Substring(text.Length - length) : text;
		}
		public static bool IsEmpty(this object obj)
		{
			return ("" + obj).Trim() == "";
		}
		public static int Int(this object obj)
		{
			return decimal.TryParse(""+obj,out decimal ret)?(int)ret:0;
		}
		public static string Int(this object obj,string format)
		{
			return Int(obj).ToString(format);
		}
		public static decimal Decimal(this object obj)
		{
			return decimal.TryParse("" + obj, out decimal ret) ? ret : 0;
		}
		public static string Decimal(this object obj,string format)
		{
			return Decimal(obj).ToString(format);
		}
		public static DateTime? Date(this object obj)
		{
			return DateTime.TryParse("" + obj, out DateTime ret) ? ret : null;
		}
		public static string Date(this object obj,string format)
		{
			return DateTime.TryParse(""+obj,out DateTime ret)?ret.ToString(format):"";
		}
		public static string MD5(this object obj)
		{
			using(var md5 = System.Security.Cryptography.MD5.Create())
			{
				var bytes = Encoding.UTF8.GetBytes("" + obj);
				return Convert.ToBase64String(bytes);
			}
		}
		public static string Json(this object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
		public static string JavaScriptString(this object obj)
		{
			return HttpUtility.JavaScriptStringEncode("" + obj);
		}
		public static string UrlString(this object obj)
		{
			return HttpUtility.UrlEncode("" + obj);
		}
		#endregion
	}
}
