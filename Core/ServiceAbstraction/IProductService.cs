using E_Commerce.Web.DataTransfrerObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <returns>Collection of product DTOs</returns>
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a specific product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product DTO if found, otherwise null</returns>
        Task<ProductDTO> GetProductByIdAsync(int id);

        /// <summary>
        /// Retrieves all product types
        /// </summary>
        /// <returns>Collection of type DTOs</returns>
        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();

        /// <summary>
        /// Retrieves all product brands
        /// </summary>
        /// <returns>Collection of brand DTOs</returns>
        Task<IEnumerable<BrandDTo>> GetAllBrandsAsync();
    }
}
