using App.Application.Extensions;
using App.Persistence.Extensions;
using App_API.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt().AddCachingExt();

builder.Services.AddRepositories(builder.Configuration).AddServices();

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();