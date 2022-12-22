
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Serilog.Core.Enrichers;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Common;

public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context,
        IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {

        try
        {
            // 出力：リクエストヘッダ
            Debug.WriteLine($"-headers- {context.Request.Method} ------------------------------------------------------------");
            context.Request.Headers.ToList().ForEach(o => Debug.WriteLine($"ReqestHeader: {o.Key}={string.Join(", ", o.Value.ToArray())}"));





            //context.Request.Headers.ToList().ForEach(o =>Utils.DebOut($"ReqestHeader:{o.Key} = {string.Join(", ", o.Value.ToArray())}"));



            context.Request.Headers.TryGetValue("user_name", out var user_name);
            context.Request.Headers.TryGetValue("y_request_id", out var y_request_id);
            context.Request.Headers.TryGetValue("y_trace_id", out var y_trace_id);

            if (!string.IsNullOrEmpty(user_name)) LogContext.PushProperty("user_name", user_name.First<string>());
            if (!string.IsNullOrEmpty(y_request_id)) LogContext.PushProperty("y_request_id", y_request_id.First<string>());
        
            if (!string.IsNullOrEmpty(y_trace_id)) LogContext.PushProperty("y_trace_id", y_trace_id.First<string>());
            LogContext.PushProperty("y_sever_datetime_local", DateTime.Now.ToString("yyyy/MM/dd HH:mm:sss"));

            //Debug.WriteLine($"-body--------------------------------------\n{getRequestBody(context)}\n-----------------------------------------------");

            return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                cancellationToken);
        }
        catch(Exception e)
        {
            Utils.DebOut($"Err:HttpRequestInterceptor#OnCreateAsync",e);
            throw;
        }



    }
    private async Task<string> getRequestBody(HttpContext context)
    {
        var body = string.Empty;
        using (var reader = new StreamReader(context.Request.Body))
        {

            var obj = await reader.ReadToEndAsync();
            body = JsonSerializer.Serialize(obj);
        }
        return body.ToString();
    }

}
