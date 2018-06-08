using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TigerPaws.DTOs;
using TigerPaws.Models;

namespace TigerPaws.App_Start
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Genre, GenreDto>();

            CreateMap<ProductDto, Product>().ForMember(p => p.Id, opt => opt.Ignore());
           
                

        }

    }
}