using AutoMapper;
using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.InternalModels
{
    public class InternamMapperProfile : Profile
    {
        public InternamMapperProfile()
        {
            CreateMap<ObservableCartItem, CartItemDto>();
            CreateMap<CartItemDto, ObservableCartItem>();
        }
    }
}
