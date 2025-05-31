using AutoMapper;
using DomainLayer.Models;
using E_Commerce.Web.DataTransfrerObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.MappingProfiles
{
    public class ProductProfile :Profile // becuase reach for map 
    {

        public ProductProfile()
        {
CreateMap<Product,ProductDTO>().ForMember(dist=>dist.BrandName,options=>options.MapFrom(src=>src.ProductBrand.Name))
.ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name));
            CreateMap<ProductType, TypeDTo>();
            CreateMap<ProductBrand, BrandDTo>();  

           // mape form product for proeuct dto
           //CreateMap<ProductProfile,t>()
        }

    }
}
