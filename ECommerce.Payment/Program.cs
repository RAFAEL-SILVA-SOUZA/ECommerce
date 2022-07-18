using ECommerce.Payment.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPaymentService, PaymentService>();

builder.Services.AddCap(x =>
{
    var builder = WebApplication.CreateBuilder(args);
    x.UseSqlServer(builder.Configuration.GetConnectionString("PaymentConnection"));
    x.UseRabbitMQ(o =>
    {
        o.HostName = "rabbitmq";
        o.Password = "guest";
        o.UserName = "guest";
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
