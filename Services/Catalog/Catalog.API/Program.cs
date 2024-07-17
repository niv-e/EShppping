using Asp.Versioning;
using Asp.Versioning.Conventions;
using Catalog.API.OpenApi;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen( option => option.OperationFilter<SwaggerDefaultValues>());
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

builder.Services.AddHealthChecks()
    .AddMongoDb(
        builder.Configuration["DatabaseSettings:ConnectionString"]!,
        "Catalog MongoDB health Check",
        HealthStatus.Degraded);

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1)
    .HasApiVersion(2)
    .ReportApiVersions()
    .Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/hello", () => "Hellow world v1")
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(1);

app.MapGet("/hello", () => "Hellow world v2")
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
 