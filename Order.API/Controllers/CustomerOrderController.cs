using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.Data;
using Order.API.DTOs;
using Order.API.Entities;
using Order.API.Events;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerOrderController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CustomerOrderController(OrderDbContext context, IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderCreateDto dto)
    {
        // DTO → Entidade
        var order = _mapper.Map<CustomerOrder>(dto);

        // Salva no MySQL
        _context.CustomerOrders.Add(order);
        await _context.SaveChangesAsync();

        // Entidade → Evento
        var orderEvent = _mapper.Map<OrderCreatedEvent>(order);
        orderEvent.OrderId = order.Id;

        // Publica no RabbitMQ
        await _publishEndpoint.Publish(orderEvent);

        return Ok(new { message = "Pedido criado com sucesso!", order.Id });
    } 

}
