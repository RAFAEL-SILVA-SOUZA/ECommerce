using ECommerce.Catalog.Extensions;
using ECommerce.Catalog.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.AddJsonFile("appsettings.json", optional: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
    config.AddEnvironmentVariables();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

builder.Services.AddCap(x =>
{
    var configuration = builder.Configuration;
    x.UseSqlServer(configuration.GetConnectionString("CatalogConnection"));
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
    var context = serviceScope.ServiceProvider.GetRequiredService<CatalogDBContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Catalog v1");
});
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
