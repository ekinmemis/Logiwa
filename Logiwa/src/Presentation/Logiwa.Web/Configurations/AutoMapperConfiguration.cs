using AutoMapper;

using Logiwa.Core.Domain.Catalog;
using Logiwa.Web.Models.Category;
using Logiwa.Web.Models.Product;

namespace Logiwa.Web.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<CategoryModel, Category>().IgnoreAllVirtual();

                cfg.CreateMap<Product, ProductModel>();
                cfg.CreateMap<ProductModel, Product>().IgnoreAllVirtual();
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
