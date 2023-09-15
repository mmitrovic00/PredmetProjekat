using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;
using WebProjekat.Models;

namespace WebProjekat.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            //Kazemo mu da mapira Subject na SubjectDto i obrnuto
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
