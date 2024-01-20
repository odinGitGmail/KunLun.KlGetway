using Cola.ColaMiddleware.ColaSwagger;
using Cola.ColaMiddleware.ColaVersioning;
using Cola.ColaNacos;
using Cola.ColaWebApi;
using Cola.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

var config = builder.Configuration;
builder.Host
    .ConfigureLogging((hostingContext, build) =>
    {
        //该方法需要引入Microsoft.Extensions.Logging名称空间
        build.AddFilter("System", LogLevel.Error); //过滤掉系统默认的一些日志
        build.AddFilter("Microsoft", LogLevel.Error); //过滤掉系统默认的一些日志
    });
builder.Services.AddColaCore();
builder.Services.AddColaHttpClient(config);
var environmentName = builder.Environment.EnvironmentName;
// builder.Configuration.AddNacosV2Configuration(config.GetSection($"NacosConfig-{environmentName}"))
// builder.Services.AddNacosV2Config(config.GetSection($"ColaNacos:a"));
// builder.Services.AddNacosV2Config(config.GetSection($"ColaNacos:nacosServer"));
builder.Services.AddColaNacos(config, new List<string>() { "a", "nacosServer" });
// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddColaVersioning(config);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddColaSwagger(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseColaSwagger(new Dictionary<string, string>()
    {
        {"/swagger/v1/swagger.json", "WebApi V1"}
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();