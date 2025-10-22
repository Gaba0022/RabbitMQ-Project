using MassTransit;
using Payment.API.Events;

namespace Payment.API.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var order = context.Message;

            Console.WriteLine($"💰 Processando pagamento do pedido {order.OrderId} - Cliente: {order.CustomerName} - Total: {order.Total}");

            // Simular processamento de pagamento
            await Task.Delay(2000);

            Console.WriteLine($"✅ Pagamento concluído para pedido {order.OrderId}");
        }
    }
}
