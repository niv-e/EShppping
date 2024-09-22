using Discount.API.Services;
using Discount.Grpc.Protos;
using Discount.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();

var app = builder.Build();
app.MigrateDatabase<Program>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<CouponService>();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(
        "Communication with gRPC endpoints must be mede through a gRPC client");
});

app.Run();
//Use for finding the API project when do integration tests
public partial class Program { };