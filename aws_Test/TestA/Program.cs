using Microsoft.EntityFrameworkCore;

using Common;
using Data;
using Mutations;

using Queries;

using System.Reflection;

using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using Services;

using Interface;

Console.WriteLine("--start Program.cs---");

var listType_Services =
Assembly.GetExecutingAssembly().GetTypes()
    .Where(_ => _.IsClass && _.IsPublic && _.IsClass && _.IsAssignableFrom(_) && _.Namespace == "Services").ToList<Type>()
;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {

            options.RequireHttpsMetadata = false;


            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    return Task.CompletedTask;

                },

                OnMessageReceived = context =>
                {
                    return Task.CompletedTask;

                },

                OnAuthenticationFailed = context =>
                {
                    return Task.CompletedTask;

                },
                OnTokenValidated = context =>
                {
                    return Task.CompletedTask;
                }
            };

            options.TokenValidationParameters =

                new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,

                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AuthUtils.validIssuer,
                    ValidAudience = AuthUtils.validAudience,

                    IssuerSigningKey = new SymmetricSecurityKey(AuthUtils.symmetricSecurityKeyBase),

                    //ClockSkew = TimeSpan.Zero

                }
                ;


        })
;

// JWT発行用サービス
builder.Services.AddScoped<AuthService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
    builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowAnyOrigin();
    });

});




ConfigurationManager configuration = builder.Configuration;
Utils.Config = configuration;

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DEV_DOCKER")))
{
    var configFile = @"appSettings.DevDocker.json";

    var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: configFile)
    ;
    Utils.Config = configBuilder.Build();

    Utils.DebOut($"DevDocker :[{configFile}]");

}

YayoiConfigSerilog.SetupLoggerConfig(args);
builder.Host.UseSerilog();
Log.Information("ログテスト");

/*##
var _connectionString = Utils.Config["ConnectionStrings:DbConectionString"];
Utils.DebOut($"_connectionString>>>:[{_connectionString}]");
builder.Services.AddDbContextPool<AppDbContext>(opt =>
    {
        opt.EnableSensitiveDataLogging();
        opt.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }
    
) ;
Utils.DebOut($"DIR_LOG:[{Utils.Config["DIR_LOG"]}]"); 
*/


builder.Services.AddMemoryCache();

Assembly.GetExecutingAssembly().GetTypes()
    .Where(_ => _.IsClass && _.IsPublic && _.IsClass && _.IsAssignableFrom(_) && _.Namespace == "Services").ToList<Type>()
    .ForEach(x => builder.Services.AddScoped(x));

builder.Services.AddScoped<IRequestIdService, RequestIdService>();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()     //これを入れないと[Authorize]が効かない

    //##.AddApolloFederation()

    .AddQueryType<Query>()
    .AddMutationType<Mutation>()


    .AddHttpRequestInterceptor<HttpRequestInterceptor>()

    .AddFiltering()
    .AddSorting()


;
builder.Services.AddHttpContextAccessor();



var app = builder.Build();


app.UseRouting();
app.UseCors();
app.UseAuthentication();

app.MapGraphQL();
//app.MapGraphQL(PathString.FromUriComponent("/v1/graphql"));



app.Run();





/*

query request_id($prefix: String) {
  req10:request_id(prefix: $prefix)
}


{
  "prefix":"pp_"
}



tail -f /work/log/L20221122.txt
*/








