using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Conexão com o MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";

// Add services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do MassTransit com RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

// Configuração do Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5104); // HTTP
    options.ListenAnyIP(7012, listenOptions => listenOptions.UseHttps()); // HTTPS
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.API v1");
        c.RoutePrefix = "swagger";
    });
}

// app.UseHttpsRedirection(); // HTTPS opcional
app.UseAuthorization();
app.MapControllers();

app.Run();
