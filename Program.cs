using Cola.ColaMiddleware.ColaSwagger;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureLogging((hostingContext, build) =>
    {
        //该方法需要引入Microsoft.Extensions.Logging名称空间
        build.AddFilter("System", LogLevel.Error); //过滤掉系统默认的一些日志
        build.AddFilter("Microsoft", LogLevel.Error); //过滤掉系统默认的一些日志
    })
    .ConfigureAppConfiguration((context, build) =>
    {
        var environmentName = builder.Environment.EnvironmentName;
        build.AddNacosV2Configuration(build.Build().GetSection($"NacosConfig-{environmentName}"));
    });

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json");
var config = builder.Configuration;
builder.Services.AddControllers();


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