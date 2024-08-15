using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Conventions;
using Catalog.API.OpenApi;
using Catalog.Application.Handlers;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.RepositoryFactory;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new QueryStringApiVersionReader();
}).AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
    });

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(option => 
{
    option.OperationFilter<SwaggerDefaultValues>();
    option.UseInlineDefinitionsForEnums();

});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(configurations =>
    configurations.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ITypesRepository, TypesRepository>();
builder.Services.AddSingleton<DatabaseSeedingTask>();
builder.Services.AddSingleton<DatabaseSeedingTask>();
builder.Services.AddDataAccessor();
builder.Services.AddHealthChecks()
    .AddDbHealthCheck(builder.Configuration["DatabaseSettings:ConnectionString"]);
   
var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1)
    .HasApiVersion(2)
    .ReportApiVersions()
    .Build();
var databaseSeedingTask = app.Services.GetRequiredService<DatabaseSeedingTask>();
await databaseSeedingTask.Run();
// Configure the HTTP request pipeline.
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/hello", () => "Hellow world v1")
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(1);

app.MapGet("/hello", () => "Hellow world v2")
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(2);

app.MapGet("/", () => "Hellow world")
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(2);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var desc in descriptions)
        {
            var url = $"/swagger/{desc.GroupName}/swagger.json";
            var name = desc.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.UseDeveloperExceptionPage();
}

app.Run();

//Use for finding the API project when do integration tests
public partial class Program { };