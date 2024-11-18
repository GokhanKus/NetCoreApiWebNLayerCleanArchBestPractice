using App.Application.Extensions;
using App.Bus;
using App.Persistence.Extensions;
using App_API.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt().AddCachingExt();

builder.Services.AddRepositories(builder.Configuration).AddServices().AddBusExt(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();