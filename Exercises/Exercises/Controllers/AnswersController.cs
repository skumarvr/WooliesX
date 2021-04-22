using AutoMapper;
using Exercises.BusinessLayer.Entities;
using Exercises.BusinessLayer.RepositoryContracts;
using Exercises.BusinessLayer.Services;
using Exercises.BusinessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Exercises.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly ILogger<AnswersController> _logger;
        private readonly ProductService productService;
        
        public AnswersController(ILogger<AnswersController> logger, IProductRepository _productRepository, IConfigurationProvider config)
        {
            _logger = logger;
            productService = new ProductService(_productRepository, config);
        }

        [HttpGet]
        [Route("User")]
        public UserTokenResponse GetUserToken()
        {
            return new UserTokenResponse();
        }

        [HttpGet]
        [Route("Sort")]
        public async Task<ProductsResponse[]> GetProducts([FromQuery]SortOptions sortOption)
        {
            var prodsResp = await productService.GetProducts(sortOption);
            return prodsResp.ToArray();
        }
    }
}
