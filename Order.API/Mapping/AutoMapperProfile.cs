using AutoMapper;
using Order.API.DTOs;
using Order.API.Entities;
using Order.API.Events;

namespace Order.API.Mapping;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<OrderCreateDto, CustomerOrder>();
        CreateMap<CustomerOrder, OrderCreatedEvent>();
    }

}
