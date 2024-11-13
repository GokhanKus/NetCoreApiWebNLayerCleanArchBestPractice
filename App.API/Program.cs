using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
	options.Filters.Add<FluentValidationFilter>();
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration).AddServices();

var app = builder.Build();

app.UseExceptionHandler(x => { });

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
	
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();