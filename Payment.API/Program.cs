using MassTransit;
using Payment.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Configuração do RabbitMQ e MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitHost = builder.Configuration["RabbitMQ__Host"] ?? "localhost";

        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context); // Cria filas automaticamente
    });
});

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura Kestrel para HTTP e HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5140); // HTTP
    options.ListenAnyIP(7050, listenOptions => listenOptions.UseHttps()); // HTTPS
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment.API v1");
        c.RoutePrefix = "swagger";
    });
}

// app.UseHttpsRedirection(); // HTTPS opcional
app.UseAuthorization();
app.MapControllers();

app.Run();
