
using Serilog;


using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Exceptions;
using System.Diagnostics;
using Serilog.Formatting.Elasticsearch;
using Serilog.Enrichers.Span;
using Path = System.IO.Path;

namespace Common;

public static class YayoiConfigSerilog
{

    public static void SetupLoggerConfig(string[] args)
    {

        // @see https://qiita.com/soi/items/e74918a924c02e3a3097
        string template = "| {Timestamp:HH:mm:ss.fff} | {Level:u4} | {Message:j} | {NewLine}{Exception}";

        string logFilePathHead = $"{Utils.Config!["DIR_LOG"]}{Path.DirectorySeparatorChar}L";
       

        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        Log.Logger = new LoggerConfiguration()
                        .Enrich.WithSpan()                                                      //Serilog.Enrichers.Span Serilogに出力されない例外の詳細およびカスタムプロパティをログに記録


                        .Enrich.WithThreadId()                                                  //Serilog.Enrichers.Thread
                        .Enrich.WithThreadName().Enrich.WithProperty("ThreadName", "__")        //Serilog.Enrichers.Thread


                        .Enrich.WithProcessId().Enrich.WithProcessName()                        //Serilog.Enrichers.Process

                        .Enrich.WithMachineName()                                               //Serilog.Enrichers.Environment
                        .Enrich.WithEnvironmentUserName()                                       //Serilog.Enrichers.Environment

                        .Enrich.WithAssemblyName()                                              //Serilog.Enrichers.AssemblyName
                        .Enrich.WithAssemblyVersion()                                           //Serilog.Enrichers.AssemblyName
                        .Enrich.WithMemoryUsage()                                               //Serilog.Enrichers.Memory




                        .Enrich.WithExceptionDetails()
                        .MinimumLevel.Information()
                        //.MinimumLevel.Verbose()
                        //.MinimumLevel.Debug()
                        //.WriteTo.Console(new ExceptionAsObjectJsonFormatter(renderMessage: true))   //ExceptionAsObjectJsonFormatter Serilog.Formatting.Elasticsearch
                        .WriteTo.Debug(outputTemplate: template)
                        //.WriteTo.Debug(new ExceptionAsObjectJsonFormatter(renderMessage: true))
                        .WriteTo.File($"{logFilePathHead}.txt", LogEventLevel.Debug, outputTemplate: template, rollingInterval: RollingInterval.Day)
                        .WriteTo.File(new CompactJsonFormatter(), $"{logFilePathHead}_comapct.json", LogEventLevel.Information, rollingInterval: RollingInterval.Day)

                        // Enrich.FromLogContext を追加して Datadog プロパティを表示
                        .Enrich.FromLogContext()        // リクエストヘッダのカスタム変数をLogContext.PushProperty()するのに必要
                                                        //Serilog.Sinks.File
                                                        //Serilog.Extensions.Logging
                        .MinimumLevel.Debug()

                        .CreateLogger()
                        ;
    }
}
