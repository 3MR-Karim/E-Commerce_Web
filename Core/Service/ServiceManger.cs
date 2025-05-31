using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManger(IUnitOfWork unitOfWork,IMapper mapper) : IServiceManger
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork ,mapper)) ;
        public IProductService ProductService => _lazyProductService.Value;
    }
}
