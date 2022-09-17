using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayCore.Application.DependencyContainer;
using PayCore.BusinessService.DependencyContainer;
using PayCore.Infrastructure.DependencyContainer;
using PayCore.Persistence.DependencyContainer;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();

app.Run();
