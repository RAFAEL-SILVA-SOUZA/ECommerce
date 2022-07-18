using ECommerce.Order.Infra;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ECommerce.Order.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.AddJsonFile("appsettings.json", optional: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
    config.AddEnvironmentVariables();
});

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
    var configuration = builder.Configuration;
    x.UseSqlServer(configuration.GetConnectionString("OrderConnection"));
    x.UseRabbitMQ(o =>
    {
        o.HostName = configuration["RabbitMQ:Host"];
        o.Password = configuration["RabbitMQ:UserName"];
        o.UserName = configuration["RabbitMQ:Password"];
        o.Port = 5672;
    });
    x.UseDashboard();
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