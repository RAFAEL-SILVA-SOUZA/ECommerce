using ECommerce.Order.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ECommerce.Order.Infra;

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
builder.Services.RegisterServices();
builder.Services.AddCap(x =>
{
    var builder = WebApplication.CreateBuilder(args);

    x.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnection"));
    x.UseRabbitMQ(o =>
    {
        o.HostName = "rabbitmq";
        o.Password = "guest";
        o.UserName = "guest";
        o.Port = 5672;
    });
});


var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Order v1");
});
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();