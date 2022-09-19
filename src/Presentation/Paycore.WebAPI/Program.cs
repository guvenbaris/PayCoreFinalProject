using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Paycore.WebAPI.Middlewares;
using PayCore.Application.DependencyContainer;
using PayCore.BusinessService.DependencyContainer;
using PayCore.Infrastructure.DependencyContainer;
using PayCore.Persistence.DependencyContainer;
using Serilog;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(config)
  .Enrich.FromLogContext()
  .Enrich.WithMachineName()
  .Enrich.WithThreadName()
  .Enrich.WithThreadId()
  .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Custom Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddPersistenceServices();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomeExceptionMiddle();

app.MapControllers();

app.Run();
