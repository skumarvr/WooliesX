using AutoMapper;

namespace Exercises.BusinessLayer.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, ViewModels.ProductsResponse>();
        }
    }
}
