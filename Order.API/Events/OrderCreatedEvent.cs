namespace Order.API.Events;

// Evento enviado para RabbitMQ quando um pedido é criado
public class OrderCreatedEvent
{
    public int OrderId { get; set; }    
    public string CustomerName { get; set; } = string.Empty;
    public decimal Total { get; set; }
}
