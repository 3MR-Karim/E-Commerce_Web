
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.DataSeeding;


using Persistence.Repository;
using Persistence;


namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<storeDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("storeDbContext"));

            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //builder.Services.AddScoped<Iprod, ProductService>(); // مهم جدًا
            //builder.Services.AddAutoMapper(X=>X.AddProfile(new ProductProfile)); // لازم النوع الصح
            //builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly); // لازم النوع الصح
            //builder.Services.AddAutoMapper(typeof(services,).Assembly); // لازم النوع الصح
            //builder.Services.AddAutoMapper(typeof(services,).Assembly); // لازم النوع الصح
            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly); // لازم النوع الصح
                                                                                //builder.Services.addAutm

            //builder.Services.AddScoped<IServicemanger, ServiceManger>;
            var app = builder.Build();
         using  var scoope=  app.Services.CreateScope();

           
          var objectDataSeeding=  scoope.ServiceProvider.GetRequiredService<IDataSeeding>();// take typer service and return servcie
                                                                                            //return serive that sked form data Seeding  

            objectDataSeeding.DataSeedAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
