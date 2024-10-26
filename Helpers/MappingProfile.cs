﻿using AutoMapper;
using Shopify.API.DTOs;
using Shopify.API.Models;

namespace Shopify.API.Helpers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Category , CategoryDTO> ().ReverseMap();
            CreateMap<Category, CategoryDetailsDTO>().ReverseMap();

            CreateMap<Product , ProductDTO> ().ReverseMap();
            CreateMap<ProductDTO , ProductDetailsDTO>().ReverseMap()
                .ForMember(opt => opt.Photos , 
                src => src.Ignore());
        }
    }
}
