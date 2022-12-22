using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Common;

public class Utils
{
    private static CSharpObjectFormatter fmt = Microsoft.CodeAnalysis.CSharp.Scripting.Hosting.CSharpObjectFormatter.Instance;

    public static string getDebStr(object obj)
    {
        string str= fmt.FormatObject(obj);
        return str;
    }

    public static bool isDebOut { get; set; } = true;
    public static void DebOut(params object[] args)
    {
        if (!isDebOut) return;

        StringBuilder sb =new StringBuilder();
        foreach(var item in args)
        {
            var s0 = $"{fmt.FormatObject(item)}";
            if (s0.Length > 2)
            {
                sb.Append(s0.Substring(1,s0.Length-2));
            }
        }
        DebOut(sb.ToString(), null);
    }
    public static void DebOut(string msg, Exception? e)
    {
       
        var sExST = $"{(e!=null?$"{Environment.NewLine}{e.StackTrace}":string.Empty)}";

        Log.Debug($"{msg}{sExST}");
        Console.WriteLine($"{msg}{sExST}");
        Debug.WriteLine($"{msg}{sExST}");
       if(e!=null) Log.Error(e,e.Message);
    }
    public static string makeQueryInfomation(MethodBase? mb,HttpRequest httpRequest)
    {
        var sMethodName = mb!.Name;
        var attr_GraphQLName = (mb.GetCustomAttribute<GraphQLNameAttribute>())!.Name;
        var attr_GraphQLDescription = (mb.GetCustomAttribute<GraphQLDescriptionAttribute>())!.Description;
        return $"Query{sMethodName}【{httpRequest.Path} {attr_GraphQLName}({attr_GraphQLDescription}】";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public static string GetRequestId(
        string? prefix)
    {
        return $"{(string.IsNullOrEmpty(prefix) ? string.Empty : $"{prefix}_")}{Guid.NewGuid().ToString("N").ToUpper()}";
    }

    public static IConfiguration? Config { set; get; }

}


