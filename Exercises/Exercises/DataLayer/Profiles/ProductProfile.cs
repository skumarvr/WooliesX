using AutoMapper;

namespace Exercises.DataLayer.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<DataLayer.DbModels.Product, BusinessLayer.Entities.Product>();
        }
    }
}
