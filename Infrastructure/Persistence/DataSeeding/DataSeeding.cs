using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence.DataSeeding
{
    public class DataSeeding(storeDbContext _storeDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                // this FuncTions do 1 one just in productojn (can proeopblity update Database you need beferor do seeding makeSure migraotinos Apply OK
                // Before Do Seeding You Make Sure all migration Apply  check db has nay databasse pending need object from db content 
                // ) or devlopetim in developet update DONE 

                var pending = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (pending.Any())
                {

                    await _storeDbContext.Database.MigrateAsync(); // do apply for all migraotin that if not applied BRO

                }

                // when enter data enter the data not person depend OK 
                // enter brand and type then product becuase depaend PK for everyone
                // everyone call function insert from start noe can do if has not data in database form aby
                if (!_storeDbContext.Set<ProductBrand>().Any())
                {
                    //var productbrandData = File.ReadAllText("D:\\Aliaa WebApi\\project\\E-Commerce.Web\\Infrastructure\\Persistence\\Data\\DataSeed\\brands.json");
                    //D:\Aliaa WebApi\project\E - Commerce.Web\Infrastructure\Persistence\Data\DataSeed\brands.json  that not needbecause first for web work can change becuase read from server OK 
                    // bro can better you the how read out executinAseempt in infratsutre in perstiens in dataseend
                    //var productbrandData = File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var productbrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // convert data for C# object [producte brand] deserialized OK 
                    // <gerenric what transofmr >
                    var productBrands = await JsonSerializer.Deserialize<List<ProductBrand>>(productbrandData);

                    if (productBrands is not null && productBrands.Any())
                    {
                        _storeDbContext.productBrands.AddRange(productBrands);
                        // not save chagne here becuase need add things other for three  
                    }
                }
                if (!_storeDbContext.ProductTypes.Any())
                {
                    //var productbrandData = File.ReadAllText("D:\\Aliaa WebApi\\project\\E-Commerce.Web\\Infrastructure\\Persistence\\Data\\DataSeed\\brands.json");
                    //D:\Aliaa WebApi\project\E - Commerce.Web\Infrastructure\Persistence\Data\DataSeed\brands.json  that not needbecause first for web work can change becuase read from server OK 
                    // bro can better you the how read out executinAseempt in infratsutre in perstiens in dataseend
                    var productbrandData = File.ReadAllText("../Infrastructure/Persistence/Data/DataSeed/products.json");
                    // convert data for C# object [producte brand] deserialized OK 
                    // <gerenric what transofmr >
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(productbrandData);

                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(ProductTypes);
                        // not save chagne here becuase need add things other for three  
                    }
                }
                // تحقق إن جدول المنتجات Products فاضي (يعني مفيهوش ب يانات أصلاً)
                if (!_storeDbContext.Products.Any())
                {
                    // قراءة ملف الـ JSON اللي فيه بيانات المنتجات – لازم يكون المسار صحيح نسبي للمشروع
                    var productsData = File.ReadAllText("../Infrastructure/Persistence/Data/DataSeed/products.json");

                    // تحويل البيانات من JSON إلى كائنات C# – ولازم النوع يكون Product مش ProductType
                    // الخطأ كان هنا: كنت عامل Deserialize على النوع الغلط
                    var Products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // لو فعلاً البيانات اتقرأت صح وفيها عناصر
                    if (Products is not null && Products.Any())
                    {
                        // أضف المنتجات إلى قاعدة البيانات (بس لسه ما حفظناش التغييرات)
                        _storeDbContext.Products.AddRange(Products);
                    }
                }

                _storeDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                // TODO
            }
        }
    }
}
