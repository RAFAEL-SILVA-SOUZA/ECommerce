using ECommerce.ApiGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.SetBasePath(env.ContentRootPath)
        .AddJsonFile("ocelot.json", false)
        .AddJsonFile($"ocelot.{env.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddMvc();

var app = builder.Build(); 

app.UseErrorHandlingMiddleware();

app.UseStaticFiles();

app.UseSwaggerForOcelotUI();

app.UseRouting();
app.UseOcelot().Wait();


app.Run();

