using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.DataSeeding
{
    public class DataSeeding(storeDbContext _storeDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                // Check for pending migrations
                var pending = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (pending.Any())
                {
                    await _storeDbContext.Database.MigrateAsync();
                }

                // Seed ProductBrands
                if (!_storeDbContext.Set<ProductBrand>().Any())
                {
                    var productbrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productbrandData);

                    if (productBrands is not null && productBrands.Any())
                    {
                        await _storeDbContext.Set<ProductBrand>().AddRangeAsync(productBrands);
                    }
                }

                // Seed ProductTypes
                if (!_storeDbContext.Set<ProductType>().Any())
                {
                    var productTypeData = File.ReadAllText("../Infrastructure/Persistence/Data/DataSeed/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);

                    if (productTypes is not null && productTypes.Any())
                    {
                        await _storeDbContext.Set<ProductType>().AddRangeAsync(productTypes);
                    }
                }

                // Seed Products
                if (!_storeDbContext.Set<Product>().Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Persistence/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                        await _storeDbContext.Set<Product>().AddRangeAsync(products);
                    }
                }

                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                throw;
            }
        }
    }
}