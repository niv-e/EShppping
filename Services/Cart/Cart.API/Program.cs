using Asp.Versioning;
using Asp.Versioning.Conventions;
using Cart.API.OpenApi;
using Cart.Application.Handlers;
using Cart.Core.Repository;
using Cart.Infrastructure;
using Cart.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
});


builder.Services.AddDataAccessor(builder.Configuration["CacheSettings:ConnectionString"]);
builder.Services.AddMediatR(configurations =>
    configurations.RegisterServicesFromAssembly(typeof(CreateShoppingCartCommandHandler).Assembly));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddHealthChecks()
    .AddDbHealthCheck(builder.Configuration["CacheSettings:ConnectionString"]);
var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1)
    .ReportApiVersions()
    .Build();

// Configure the HTTP request pipeline.
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

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
