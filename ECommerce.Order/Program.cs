using ECommerce.Order.Extensions;
using System.Text.Json.Serialization;
using MediatR;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.
        Add(new JsonStringEnumConverter());

    options.JsonSerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.RegisterServices();
builder.Services.AddCap(x =>
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigurationManager configuration = builder.Configuration;
    x.UseSqlServer(configuration.GetConnectionString("OrderConnection"));
    x.UseRabbitMQ(o =>
    {
        o.HostName = "localhost";
        o.Password = "guest";
        o.UserName = "guest";
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use(async (ctx, _next) =>
{
    try
    {
        await _next(ctx);
        
    }
    catch (Exception ex)
    {
        ctx.Response.StatusCode = 500;
        ctx.Response.ContentType = "application/json";
        await ctx.Response.WriteAsync(JsonConvert.SerializeObject(new { Error = ex.Message }));
    }
});

app.Run();


public partial class Program { };