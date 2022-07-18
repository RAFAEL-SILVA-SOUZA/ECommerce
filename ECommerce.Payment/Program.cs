using ECommerce.Payment.Domain.Services;

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
builder.Services.AddTransient<IPaymentService, PaymentService>();

builder.Services.AddCap(x =>
{
    var configuration = builder.Configuration;
    x.UseSqlServer(configuration.GetConnectionString("PaymentConnection"));
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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
